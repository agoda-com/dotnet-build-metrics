using System.Xml;
using System.Xml.Serialization;
using Agoda.DevFeedback.Common;
using Agoda.Tests.Metrics;

namespace Agoda.Tests.Metrics.xUnit
{
    /// <summary>
    /// Allow test results to be built up as tests progress
    /// </summary>
    internal class TestResultsBuilder
    {
        //--- Default endpoint to use
        private const string DefaultIngestorEndpoint = "http://localhost/api/testmetrics";

        ///--- Used for synchronization
        private static object BuilderLock = new object();

        /// <summary>
        /// True if we are inside a CI pipeline
        /// </summary>
        public bool InsideCI { get; private set; }

        /// <summary>
        /// The endpoint to use for publishing
        /// </summary>
        public string IngestorEndpoint { get; private set; }

        /// <summary>
        /// Information about the host machine
        /// </summary>
        public HostContext HostContext { get; private set; }

        /// <summary>
        /// Information about the Git environment for the project
        /// </summary>
        public GitContext GitContext { get; private set; }

        /// <summary>
        /// The collection of suites
        /// </summary>
        private List<TestCase> _testResults = new();

        /// <summary>
        /// We only publish if we are not in CI and we have a GIT repo
        /// </summary>
        public bool WillPublishingHappen => !(
            InsideCI                                          // We are in a CI environment
            || string.IsNullOrEmpty(GitContext.RepositoryUrl) // or there is no attached Git repository
            || string.IsNullOrWhiteSpace(IngestorEndpoint)    // or there is no defined endpoint
            );

        /// <summary>
        /// Constructor
        /// 
        /// Sets up various contexts from the underlying environment
        /// </summary>
        public TestResultsBuilder()
        {
            HostContext = new HostContext();
            GitContext = GitContextReader.GetGitContext();
            // For CI check if the 'CI' environment variable exists
            InsideCI = Environment.GetEnvironmentVariable("CI") != null;
            // Allow override of default endpoint
            IngestorEndpoint = Environment.GetEnvironmentVariable("TEST_METRICS_INGESTOR") ?? DefaultIngestorEndpoint;
        }

        /// <summary>
        /// Publish the collected results
        /// </summary>
        public string? Publish()
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen)
                return null;
            // TODO: Publish new metric
            return null;
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        internal void ReportSkipped(string name, string classname, decimal time)
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen)
                return;
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        internal void ReportSuccess(string name, string classname, decimal time)
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen)
                return;
        }

        /// <summary>
        /// Report a failed test
        /// </summary>
        internal void ReportFailure(string name, string classname, decimal time, string message, string? text = null)
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen)
                return;
        }

        /// <summary>
        /// Report an error
        /// </summary>
        internal void ReportError(string name, string classname, decimal time, string message, string? text = null)
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen)
                return;
        }
    }
}
