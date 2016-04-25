﻿namespace Virgil.SDK.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a confirmed identity information.
    /// </summary>
    public class IdentityConfirmedInfo
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("type")]
        public IdentityType Type { get; set; }

        /// <summary>
        /// Gets or sets the validation token.
        /// </summary>
        [JsonProperty("validation_token")]
        public string ValidationToken { get; set; }
    }
}