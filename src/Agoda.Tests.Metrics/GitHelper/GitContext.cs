namespace Agoda.Tests.Metrics.GitHelper
{
    public class GitContext
    {
        /// <summary>
        /// Full URL to the repository
        /// </summary>
        public string RepositoryUrl { get; set; }

        /// <summary>
        /// Name of the repository
        /// </summary>
        public string RepositoryName { get; set; }

        /// <summary>
        /// Name of the current branch
        /// </summary>
        public string BranchName { get; set; }
    }
}
