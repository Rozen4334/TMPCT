using System.Text.Json.Serialization;

namespace TMPCT.API.Models
{
    /// <summary>
    ///     Represents a translatable language.
    /// </summary>
    public class Language
    {
        /// <summary>
        ///     The language code.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = "";

        /// <summary>
        ///     The language name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
