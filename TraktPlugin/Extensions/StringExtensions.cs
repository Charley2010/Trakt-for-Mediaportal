﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TraktPlugin.Extensions
{
    public static class StringExtensions
    {
        public static string ToSlug(this string phrase)
        {
            if (phrase == null) return string.Empty;

            var s = phrase.RemoveDiacritics().ToLower();
            s = Regex.Replace(s, @"[^a-z0-9\s-]", string.Empty);        // remove invalid characters
            s = Regex.Replace(s, @"\s+", " ").Trim();                   // single space
            s = s.Substring(0, s.Length <= 45 ? s.Length : 45).Trim();  // cut and trim
            s = Regex.Replace(s, @"\s", "-");                           // insert hyphens
            return s.ToLower(); 
        }

        public static string RemoveDiacritics(this string text)
        {
            if (text == null) return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static bool IsNumber(this string number)
        {
            double retValue;
            return double.TryParse(number, out retValue);
        }

        public static int ToInt(this string number)
        {
            int i = 0;
            int.TryParse(number, out i);
            return i;
        }

        public static string ToLogString(this string text)
        {
            return string.IsNullOrEmpty(text) ? "<empty>" : text;
        }

        public static string ToLogString(this int? number)
        {
            return number.HasValue ? number.ToString() : "<empty>";
        }

        /// <summary>
        /// Strips the title of a year if it exists
        /// </summary>        
        public static string StripYear(this string title, int? year)
        {
            if (year == null)
                return title;
            
            // e.g. ShowTitle (2000) ==> ShowTitle
            return title.Replace(string.Format(" ({0})", year), string.Empty);
        }

        public static string ToCountryName(this string twoLetterCode)
        {
            // check length of code is two letters
            if (string.IsNullOrEmpty(twoLetterCode))
                return null;

            RegionInfo regionInfo = null;
            try
            {
                regionInfo = new RegionInfo(twoLetterCode);
            }
            catch (ArgumentException)
            {
                TraktLogger.Debug("Failed to convert two letter country code to country name. Two Letter Code = '{0}'", twoLetterCode);
                return twoLetterCode;
            }
            return regionInfo.DisplayName;
        }

        public static string StripHTML(this string htmlString)
        {
            if (string.IsNullOrEmpty(htmlString)) return string.Empty;

            string pattern = @"<(.|\n)*?>";
            return Regex.Replace(htmlString, pattern, string.Empty);
        }

        public static string RemapHighOrderChars(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // hack to remap high order unicode characters with a low order equivalents
            // for now, this allows better usage of clipping. This can be removed, once the skin engine can properly render unicode without falling back to sprites
            // as unicode is more widely used, this will hit us more with existing font rendering only allowing cached font textures with clipping
            
            input = input.Replace(((char)8211).ToString(), "--");  //	–
            input = input.Replace(((char)8212).ToString(), "---");  //	—
            input = input.Replace((char)8216, '\''); //	‘
            input = input.Replace((char)8217, '\''); //	’
            input = input.Replace((char)8220, '"');  //	“
            input = input.Replace((char)8221, '"');  //	”
            input = input.Replace((char)8223, '"');  // ‟
            input = input.Replace((char)8226, '*');  //	•
            input = input.Replace(((char)8230).ToString(), "...");  // …
            input = input.Replace(((char)8482).ToString(), string.Empty);  // ™
            
            return input;
        }

        public static string SurroundWithDoubleQuotes(this string text)
        {
            return SurroundWith(text, "\"");
        }

        public static string SurroundWith(this string text, string ends)
        {
            return ends + text + ends;
        }

        public static int? ToNullableInt32(this string text)
        {
            int i;
            if (Int32.TryParse(text, out i)) return i;
            return null;
        }

        public static string ToNullIfEmpty(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            if (text.Trim() == string.Empty)
                return null;

            return text.Trim();
        }

        public static string ToPrettyTime(this TimeSpan span)
        {
            if (span.TotalDays >= 1)
            {
                return string.Format("{0} day{1}, {2} hour{3} and {4} minute{5}", span.Days, span.Days > 1 ? "s" : "", span.Hours, span.Hours != 1 ? "s" : "", span.Minutes, span.Minutes != 1 ? "s" : "");
            }
            else if (span.TotalHours >= 1)
            {
                return string.Format("{0} hour{1}, {2} minute{3} and {4} second{5}", span.Hours, span.Hours > 1 ? "s" : "", span.Minutes, span.Minutes != 1 ? "s" : "", span.Seconds, span.Seconds != 1 ? "s" : "");
            }
            else if (span.TotalMinutes >= 1)
            {
                return string.Format("{0} minute{1} and {2} second{3}", span.Minutes, span.Minutes > 1 ? "s" : "", span.Seconds, span.Seconds != 1 ? "s" : "");
            }
            else
            {
                return string.Format("{0} seconds", Math.Round(span.TotalSeconds, 3));
            }
        }
    }
}
