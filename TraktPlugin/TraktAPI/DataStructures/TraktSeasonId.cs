﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TraktPlugin.TraktAPI.DataStructures
{
    [DataContract]
    public class TraktSeasonId
    {
        [DataMember(Name = "trakt")]
        public int? Trakt { get; set; }

        [DataMember(Name = "tmdb")]
        public int? Tmdb { get; set; }

        [DataMember(Name = "tvdb")]
        public int? Tvdb { get; set; }

        [DataMember(Name = "tvrage")]
        public int? TvRage { get; set; }
    }
}
