using System.ComponentModel;
using System.Diagnostics;

namespace Agoda.Tests.Metrics.GitHelper
{
    public static class GitContextReader
    {
        public static GitContext GetGitContext()
        {
            string url = RunCommand("config --get remote.origin.url");
            return new GitContext
            {
                RepositoryUrl = RunCommand("config --get remote.origin.url"),
                RepositoryName = GetRepositoryNameFromUrl(url),
                BranchName = RunCommand("rev-parse --abbrev-ref HEAD")
            };
        }

        static string RunCommand(string args)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                (
                    fileName: Environment.OSVersion.Platform == PlatformID.Win32NT ? "git.exe" : "git"
                )
                {
                    UseShellExecute = false,
                    WorkingDirectory = Environment.CurrentDirectory,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    Arguments = args
                }
            };

            try
            {
                process.Start();
            }
            catch (Win32Exception ex)
            {
                return string.Empty;
            }

            return process.StandardOutput.ReadLine();
        }

        static string GetRepositoryNameFromUrl(string url)
        {
            var repositoryName = url.Substring(url.LastIndexOf('/') + 1);

            return repositoryName.EndsWith(".git")
                ? repositoryName.Substring(0, repositoryName.LastIndexOf('.'))
                : repositoryName;
        }
    }
}
