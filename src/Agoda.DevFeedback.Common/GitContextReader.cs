using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Agoda.DevFeedback.Common
{
    public class GitContextNotFoundException : Exception
    {
        public GitContextNotFoundException(string message) : base(message)
        {
        }
    }
    
    public static class GitContextReader
    {
        public static GitContext GetGitContext()
        {
            string url = RunCommand("config --get remote.origin.url");
            string branch = RunCommand("rev-parse --abbrev-ref HEAD");

            if (string.IsNullOrEmpty(url))
            {
                throw new GitContextNotFoundException("unable to get git remote url");
            }

            if (string.IsNullOrEmpty(branch))
            {
                throw new GitContextNotFoundException("unable to get git branch");
            }

            return new GitContext
            {
                RepositoryUrl = url,
                RepositoryName = GetRepositoryNameFromUrl(url),
                BranchName = branch
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
            catch(Win32Exception)
            {
                throw new GitContextNotFoundException("git executable not found");
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
