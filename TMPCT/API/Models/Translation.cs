using System.Text.Json.Serialization;

namespace TMPCT.API.Models
{
    /// <summary>
    ///     Represents a succesful translation.
    /// </summary>
    public class Translation
    {
        /// <summary>
        ///     The translated text.
        /// </summary>
        [JsonPropertyName("translatedText")]
        public string TranslatedText { get; set; } = "";

        /// <summary>
        ///     Checks if the translation was successful compared to the input value.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool TranslationSuccessful(TranslationRequest request)
            => !(request.Text is not null && request.Text.Equals(TranslatedText, StringComparison.InvariantCultureIgnoreCase));
    }
}
