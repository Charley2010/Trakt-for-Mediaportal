﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TraktPlugin.TraktAPI.DataStructures
{
    [DataContract]
    public class TraktListDetail : TraktList
    {
        [DataMember(Name = "updated_at")]
        public string UpdatedAt { get; set; }

        [DataMember(Name = "item_count")]
        public int ItemCount { get; set; }

        [DataMember(Name = "comment_count")]
        public int Comments { get; set; }

        [DataMember(Name = "likes")]
        public int Likes { get; set; }

        [DataMember(Name = "ids")]
        public TraktId Ids { get; set; }
    }
}
