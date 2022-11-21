using CSF;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMPCT
{
    /// <summary>
    ///     Runs a command from source.
    /// </summary>
    public class CommandProcessor
    {
        const string _portCommand = "@ECHO OFF && for /F \"tokens=1,2\" %i in ('tasklist /FI \"IMAGENAME eq Terraria.exe\" /fo table /nh') do (for /F \"tokens=1,2\" %a in ('netstat -ano ^| find \"%j\" ^| find \"ESTABLISHED\"') do (for /F \"delims=: tokens=2\" %c in ('echo %b') do (echo Local Port: \"%c\")))";

        private readonly ProcessStartInfo _procInfo;

        public CommandProcessor(string command, Action<ProcessStartInfo> startInfo)
            : this(command)
        {
            startInfo(_procInfo);
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CommandProcessor(string? command = null)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            command ??= _portCommand;

            var procInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            _procInfo = procInfo;
        }

        private Process _process;
        /// <summary>
        ///     Starts the process.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken token = default)
        {
            await Task.CompletedTask;

            var proc = Process.Start(_procInfo)!;

            proc.OutputDataReceived += OutputReceived;

            proc.ErrorDataReceived += ErrorReceived;

            _process = proc;
        }

        private string? _output = null;
        private void OutputReceived(object sender, DataReceivedEventArgs e)
        {
            _output = e.Data;
        }

        private bool _error = false;
        private void ErrorReceived(object sender, DataReceivedEventArgs e)
        {
            _error = true;
        }

        /// <summary>
        ///     Awaits the result of the process and returns the output value.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<string> AwaitResultAsync(CancellationToken token = default)
        {
            _process.BeginErrorReadLine();
            _process.BeginOutputReadLine();

            while (!token.IsCancellationRequested || _output is null)
            {
                await Task.Delay(1000, token);
            }

            _process.WaitForExit();

            if (_error)
                throw new InvalidOperationException();

            return _output;
        }
    }
}
