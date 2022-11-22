using TMPCT.API;
using TMPCT.Commands;
using TMPCT.Commands.Readers;
using TMPCT.Configuration;
using TMPCT.Conn;
using TMPCT.Exceptions;

// start build
var collection = new ServiceCollection();

collection.AddLogging(configure => configure.AddSimpleConsole());
collection.AddHttpClient<ITranslateClient, TranslateClient>(configure => configure.BaseAddress = new Uri(Config.Settings.ApiUrl));
collection.AddSingleton(new CommandConfiguration()
{
    TypeReaders = new TypeReaderProvider()
        .Include<Uri>(new UriReader())
});
collection.AddSingleton<CommandFramework>();
// end build

// start setup
using var services = collection.BuildServiceProvider();

var framework = services.GetRequiredService<CommandFramework>();

framework.CommandExecuted += (context, result) =>
{
    if (!result.IsSuccess)
    {
        AnsiConsole.MarkupLineInterpolated($"[red]An unexpected error occurred:[/] [grey]{result.ErrorMessage}[/]");
        if (result.Exception is not null)
            AnsiConsole.WriteException(result.Exception);
    }
    return Task.CompletedTask;
};
// end setup

// start exec
while (true)
{
    var token = new CancellationTokenSource();

    // resolve local port & proc name
    if (TcpConnectionInfo.TryGetLocalPort(Config.Settings.ProcessName, out var localGamePort) is 0)
    {
        var procName = AnsiConsole.Ask<string>("[red]Process failed to resolve.[/] [grey]Please provide process name:[/]");

        if (TcpConnectionInfo.TryGetLocalPort(Config.Settings.ProcessName, out localGamePort) is 0)
            throw new ProcessUnavailableException("The Terraria process is not live or not connected to a multiplayer server.");

        Config.Settings.ProcessName = procName;
    }
    Config.Settings.ProcessPort = localGamePort;

    // start command execution
    while (!token.IsCancellationRequested)
    {
        var command = AnsiConsole.Ask<string>("[grey]Command: '[/][orange1]help[/][grey]' for more info[/]");

        var context = new LocalCommandContext(command, token);
        await framework.ExecuteCommandAsync(context, services);
    }
}
// end exec