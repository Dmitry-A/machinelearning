using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AzureML
{
    internal class AzAuth
    {
        public static async Task<string> GetAccessToken()
        {
            (bool cliSucceeded, string cliToken) = await TryGetAzCliToken();
            if (cliSucceeded) return cliToken;

            //(bool powershellSucceeded, string psToken) = await TryGetAzPowerShellToken();
            //if (powershellSucceeded) return psToken;

            throw new Exception($"Unable to connect to Azure. Make sure you have the `az` CLI or `Az.Accounts` PowerShell module installed and logged in and try again");
        }

        private static async Task<(bool succeeded, string token)> TryGetAzCliToken()
        {
            try
            {
                return (true, await RunAzCliCommand("account get-access-token --query \"accessToken\" --output json"));
            }
            catch (Exception)
            {
                return (false, null);
            }
        }

        private static async Task<string> RunAzCliCommand(string param)
        {
            if (!CommandExists("az"))
            {
                throw new Exception("az CLI not found");
            }
            var az = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? new Executable("cmd", $"/c az {param}")
                : new Executable("az", param);

            var stdout = new StringBuilder();
            var stderr = new StringBuilder();
            var exitCode = await az.RunAsync(o => stdout.AppendLine(o), e => stderr.AppendLine(e));
            if (exitCode == 0)
            {
                return stdout.ToString().Trim(' ', '\n', '\r', '"');
            }
            else
            {
                throw new Exception("Error running Az CLI command");
            }
        }

        public static bool CommandExists(string command)
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? CheckExitCode("where", command)
            : CheckExitCode("/bin/bash", $"-c \"command -v {command}\"");

        private static bool CheckExitCode(string fileName, string args, int expectedExitCode = 0)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = args,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            var process = System.Diagnostics.Process.Start(processStartInfo);
            process?.WaitForExit();
            return process?.ExitCode == 0;
        }
    }
}