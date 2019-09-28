﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.Models
{
    public class Language
    {
        [JsonProperty(PropertyName = "iso639_1")]
        public string Iso6391 { get; set; }

        [JsonProperty(PropertyName = "iso639_2")]
        public string Iso6392 { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "nativeName")]
        public string NativeName { get; set; }
    }
}
