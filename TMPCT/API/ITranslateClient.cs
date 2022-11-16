using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPCT.API.Models;

namespace TMPCT.API
{
    /// <summary>
    ///     Represents a client that manages translation.
    /// </summary>
    public interface ITranslateClient
    {
        /// <summary>
        ///     Gets all supported languages in the current API version.
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyCollection<Language>> GetSupportedLanguagesAsync();

        /// <summary>
        ///     Translates the given input to a translated string.
        /// </summary>
        /// <param name="action">The post to translate.</param>
        /// <returns></returns>
        public Task<Translation> TranslateAsync(Action<TranslationRequest> action);
    }
}
