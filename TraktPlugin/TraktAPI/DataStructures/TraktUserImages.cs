﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TraktPlugin.TraktAPI.DataStructures
{
    [DataContract]
    public class TraktUserImages
    {
        [DataMember(Name = "avatar")]
        public TraktImage Avatar { get; set; }
    }
}
