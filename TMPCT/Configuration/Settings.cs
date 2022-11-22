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

namespace TMPCT.Configuration
{
    internal enum ReadState
    {
        Deserializing,

        Ready
    }

    public record class Settings
    {
        [JsonIgnore]
        internal ReadState ReadState { get; set; } = ReadState.Deserializing;

        private string _apiUrl;
        /// <summary>
        ///     The API url used to connect to the translate API.
        /// </summary>
        [JsonPropertyName("api-url")]
        public string ApiUrl
        {
            get
                => _apiUrl;
            set
            {
                if (ReadState is ReadState.Ready)
                    Save();
                _apiUrl = value;
            }
        }

        private string _processName;
        /// <summary>
        ///     The process name.
        /// </summary>
        [JsonPropertyName("proc-name")]
        public string ProcessName
        {
            get
                => _processName;
            set
            {
                if (ReadState is ReadState.Ready)
                    Save();
                _processName = value;
            }
        }

        private ushort _processPort;
        /// <summary>
        ///     The process port.
        /// </summary>
        [JsonPropertyName("proc-port")]
        public ushort ProcessPort
        {
            get
                => _processPort;
            set
            {
                if (ReadState is ReadState.Ready)
                    Save();
                _processPort = value;
            }
        }

        public Settings()
        {
            _apiUrl = "";
            _processName = "Terraria";
            _processPort = 0;
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
