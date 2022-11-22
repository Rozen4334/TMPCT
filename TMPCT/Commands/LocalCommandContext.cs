using CSF.Spectre;

namespace TMPCT.Commands
{
    public class LocalCommandContext : SpectreCommandContext
    {
        /// <summary>
        ///     The cancellation token responsible for restarting.
        /// </summary>
        public CancellationTokenSource Token { get; }

        public LocalCommandContext(string rawInput, CancellationTokenSource token)
            : base(rawInput)
        {
            Token = token;
        }
    }
}
