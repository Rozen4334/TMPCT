using CSF.Spectre;
using TMPCT.Configuration;

namespace TMPCT.Commands
{
    public class ConfigModule : SpectreModuleBase<LocalCommandContext>
    {
        [Command("set-process")]
        public void SetProcess([Remainder] string name)
        {
            Config.Settings.ProcessName = name;

            Success("Succesfully set process name.");
        }

        [Command("set-port")]
        public void SetPort(ushort port)
        {
            Config.Settings.ProcessPort = port;

            Success("Succesfully set process port.");
        }

        [Command("set-apiurl")]
        public void SetApiUrl([Remainder] Uri uri)
        {
            Config.Settings.ApiUrl = uri.ToString();

            Success("Succesfully set translation API url.");
        }
    }
}
