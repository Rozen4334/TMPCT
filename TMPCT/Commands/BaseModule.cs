using CSF.Spectre;

namespace TMPCT.Commands
{
    public class BaseModule : SpectreModuleBase<LocalCommandContext>
    {
        [Command("quit", "q", "exit")]
        public void Exit()
        {
            Respond("[green]Quitting application.[/]");

            Environment.Exit(0);
        }

        [Command("restart", "r")]
        public void Restart()
        {
            Context.Token.Cancel();

            Respond("[green]Restarting application.[/]");
        }
    }
}
