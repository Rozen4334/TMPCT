using System.Text.Json.Serialization;

namespace TMPCT.API.Models
{
    /// <summary>
    ///     Represents a translation request.
    /// </summary>
    public class TranslationRequest
    {
        /// <summary>
        ///     The translation query.
        /// </summary>
        [JsonPropertyName("q")]
        public string Text { get; set; } = "";

        /// <summary>
        ///     The source language.
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = "";

        /// <summary>
        ///     The target language.
        /// </summary>
        [JsonPropertyName("target")]
        public string Target { get; set; } = "";

        /// <summary>
        ///     The API key.
        /// </summary>
        [JsonPropertyName("api_key")]
        public string ApiKey { get; set; } = "";
    }
}
