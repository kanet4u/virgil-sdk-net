﻿namespace Virgil.PKI.Dtos
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class PkiUserData
    {
        [JsonProperty("id")]
        public PkiIdBundle Id { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("is_confirmed")]
        public bool Confirmed { get; set; }

        [JsonProperty("signs")]
        public List<PkiSign> Signs { get; set; }
    }
}