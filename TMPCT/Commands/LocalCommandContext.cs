using CSF.Spectre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMPCT.Commands
{
    public class LocalCommandContext : SpectreCommandContext
    {
        /// <summary>
        ///     The cancellation token responsible for further execution.
        /// </summary>
        public CancellationTokenSource Token { get; }

        public LocalCommandContext(string rawInput, CancellationTokenSource token) 
            : base(rawInput)
        {
            Token = token;
        }
    }
}
