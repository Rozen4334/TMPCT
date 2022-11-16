using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using TMPCT.API.Models;

namespace TMPCT.API
{
    public class TranslateClient : ITranslateClient
    {
        private readonly HttpClient _httpClient;

        public TranslateClient(HttpClient client)
            => _httpClient = client;

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<Language>> GetSupportedLanguagesAsync()
            => JsonSerializer.Deserialize<List<Language>>(await _httpClient.GetStringAsync("/languages"))
            ?? new();

        /// <inheritdoc/>
        public async Task<Translation> TranslateAsync(Action<TranslationRequest> action)
        {
            var post = new TranslationRequest();
            action(post);

            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "q", post.Text },
                { "source", post.Source.ToString() },
                { "target", post.Target.ToString() },
                { "api_key", post.ApiKey }
            });

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/translate")
            {
                Content = content
            });

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<Translation>(await response.Content.ReadAsStringAsync());
                return result
                    ?? new()
                    {
                        TranslatedText = string.Empty
                    };
            }

            return new()
            {
                TranslatedText = string.Empty
            };
        }
    }
}
