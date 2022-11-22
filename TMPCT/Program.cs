using TMPCT;
using TMPCT.API;
using TMPCT.Commands;

// start build
var collection = new ServiceCollection();

collection.AddLogging(configure => configure.AddSimpleConsole());
collection.AddHttpClient<ITranslateClient, TranslateClient>(configure => configure.BaseAddress = new Uri(Configuration.Settings.ApiUrl ?? ""));
collection.AddSingleton<CommandFramework>();
// end build

// start setup
using var services = collection.BuildServiceProvider();

var token = new CancellationTokenSource();

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
while (!token.IsCancellationRequested)
{
    var command = AnsiConsole.Ask<string>("[grey]Command: '[/][orange1]help[/][grey]' for more info[/]");

    var context = new LocalCommandContext(command, token);
    await framework.ExecuteCommandAsync(context, services);
}
// end exec