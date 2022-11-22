using System.Text.Json;

namespace TMPCT.Configuration
{
    public static class Config
    {
        private static string? _confRoot;

        public static string BasePath { get; }

        public static JsonSerializerOptions Options { get; }

        public static Settings Settings { get; }

        static Config()
        {
            BasePath = "files";

            if (!Path.Exists(BasePath))
                Directory.CreateDirectory(BasePath);

            Settings = Read();
            Options = new JsonSerializerOptions()
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
                WriteIndented = true
            };
        }

        public static string GetPath()
           => _confRoot ??= Path.Combine(BasePath, "config.json");

        public static Settings Read()
            => ReadAsync().GetAwaiter().GetResult();

        public static async Task<Settings> ReadAsync(CancellationToken token = default)
        {
            var path = GetPath();

            if (!Path.Exists(path))
                return await ReadInnerAsync(path, token);

            var content = await File.ReadAllTextAsync(path, token);

            if (content is null)
                throw new ArgumentNullException(nameof(content), $"Internal file content in '{path}' was found null. \n\rPlease delete the file to let the application regenerate it.");

            var obj = JsonSerializer.Deserialize<Settings>(content, Options);

            if (obj is null)
                throw new JsonException($"Json structure in '{path}' was found null. \n\rPlease delete the file to let the application regenerate it.");

            obj.ReadState = ReadState.Ready;

            return obj;
        }

        private static async Task<Settings> ReadInnerAsync(string? path = null, CancellationToken token = default)
        {
            var obj = new Settings();

            obj.ReadState = ReadState.Ready;

            await obj.SaveAsync(path, token);

            return obj;
        }
    }
}
