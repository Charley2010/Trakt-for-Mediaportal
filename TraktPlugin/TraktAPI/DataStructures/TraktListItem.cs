﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TraktPlugin.TraktAPI.DataStructures
{
    [DataContract]
    public class TraktListItem
    {
        [DataMember(Name = "listed_at")]
        public string ListedAt { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "movie")]
        public TraktMovie Movie { get; set; }

        [DataMember(Name = "show")]
        public TraktShowSummary Show { get; set; }

        [DataMember(Name = "season")]
        public TraktSeason Season { get; set; }

        [DataMember(Name = "episode")]
        public TraktEpisode Episode { get; set; }

        [DataMember(Name = "person")]
        public TraktPerson Person { get; set; }
    }
}
