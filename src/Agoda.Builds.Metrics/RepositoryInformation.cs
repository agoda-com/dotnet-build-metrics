using LibGit2Sharp;
using System;
using System.Linq;
namespace Agoda.Builds.Metrics
{
    public class RepositoryInformation : IDisposable
    {
        private bool _disposed;
        private readonly Repository _repo;
        public static RepositoryInformation GetRepositoryInformation(string path)
        {
                return new RepositoryInformation(path);
        }

        public static bool isValidPath(string path)
        {
            if (Repository.IsValid(path))
            {
                return true;
            }
            return false;
        }
        private RepositoryInformation(string path)
        {
            _repo = new Repository(path);
        }
        public string BranchName => _repo.Head.FriendlyName;

        public string Origin => _repo.Network.Remotes.FirstOrDefault()?.Url;

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _repo.Dispose();
        }
    }
}