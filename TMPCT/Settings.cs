using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TMPCT
{
    public record class Settings
    {
        [JsonPropertyName("api-url")]
        public string ApiUrl { get; set; }

        [JsonPropertyName("proc-name")]
        public string ProcessName { get; set; }

        public Settings()
        {
            ApiUrl = string.Empty;
            ProcessName = string.Empty;
        }

        public void Save(string? path = null)
            => SaveAsync(path).GetAwaiter().GetResult();

        public async Task SaveAsync(string? path = null, CancellationToken token = default)
        {
            path ??= Configuration.GetPath();

            var serialized = JsonSerializer.Serialize(this, Configuration.Options);

            await File.WriteAllTextAsync(path, serialized, token);
        }
    }
}
