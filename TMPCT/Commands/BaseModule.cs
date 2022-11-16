using CSF.Spectre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMPCT.Commands
{
    public class BaseModule : SpectreModuleBase<LocalCommandContext>
    {
        [Command("quit")]
        public void Exit()
        {
            Context.Token.Cancel();

            Respond("[green]Quitting application.[/]");
        }
    }
}
