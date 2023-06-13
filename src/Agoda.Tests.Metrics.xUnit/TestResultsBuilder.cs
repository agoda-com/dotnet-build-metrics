using System.Xml;
using System.Xml.Serialization;

using Agoda.Tests.Metrics.xUnit.Models;

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
        public GitHelper.GitContext GitContext { get; private set; }

        /// <summary>
        /// The collection of suites
        /// </summary>
        private JUnitTestSuites _testSuites = new();

        /// <summary>
        /// The current test suite
        /// </summary>
        private JUnitTestSuite? _testSuite;

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
            GitContext = GitHelper.GitContextReader.GetGitContext();
            // For CI check if the 'CI' environment variable exists
            InsideCI = Environment.GetEnvironmentVariable("CI") != null;
            // Allow override of default endpoint
            IngestorEndpoint = Environment.GetEnvironmentVariable("TEST_METRICS_INGESTOR") ?? DefaultIngestorEndpoint;
        }

        /// <summary>
        /// Start reporting a suite
        /// </summary>
        public TestSuiteReporter? BeginSuite(string name, string package)
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen)
                return null;
            lock(BuilderLock)
            {
                _testSuite = new JUnitTestSuite()
                {
                    Name = name,
                    Package = package,
                    Id = _testSuites.TestSuite.Count,
                    Hostname = HostContext.Hostname
                };
                _testSuites.TestSuite.Add(_testSuite);
            }
            return new TestSuiteReporter(this);
        }

        /// <summary>
        /// Publish the collected results
        /// </summary>
        public string? Publish()
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen)
                return null;
            // TODO: Do collation of counts
            // Convert to XML
            XmlSerializer xmlSerializer = new XmlSerializer(_testSuites.GetType());
            lock(BuilderLock)
            {
                using (TextWriter textWriter = new StreamWriter("test-results.xml"))
                using (var xmlWriter = new XmlTextWriter(textWriter))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlSerializer.Serialize(xmlWriter, _testSuites);
                }
                using (TextWriter textWriter = new StringWriter())
                using (var xmlWriter = new XmlTextWriter(textWriter))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlSerializer.Serialize(xmlWriter, _testSuites);
                    return textWriter.ToString();
                }
            }
        }

        /// <summary>
        /// Create the basic TestCase model
        /// </summary>
        internal JUnitTestCase CreateTestCase(string name, string classname, decimal time)
        {
            return new JUnitTestCase()
            {
                Name = name,
                Classname = classname,
                Time = (double)time
            };
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        internal void ReportSkipped(TestSuiteReporter reporter, string name, string classname, decimal time)
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen || _testSuite == null)
                return;
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        internal void ReportSuccess(TestSuiteReporter reporter, string name, string classname, decimal time)
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen || _testSuite == null)
                return;
            lock(BuilderLock)
            {
                _testSuite?.Testcase.Add(CreateTestCase(name, classname, time));
            }
        }

        /// <summary>
        /// Report a failed test
        /// </summary>
        internal void ReportFailure(TestSuiteReporter reporter, string name, string classname, decimal time, string message, string? text = null)
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen || _testSuite == null)
                return;
            lock(BuilderLock)
            {
                var tc = CreateTestCase(name, classname, time);
                tc.Failure = new JUnitFailure()
                {
                    Message = message,
                    Text = text
                };
                _testSuite.Testcase.Add(tc);
                _testSuite.Failures++;
            }
        }

        /// <summary>
        /// Report an error
        /// </summary>
        internal void ReportError(TestSuiteReporter reporter, string name, string classname, decimal time, string message, string? text = null)
        {
            // If we are not going to publish then don't bother
            if (!WillPublishingHappen || _testSuite == null)
                return;
        }
    }
}
