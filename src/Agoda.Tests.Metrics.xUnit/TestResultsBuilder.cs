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
        ///--- Used for synchronization
        private static object BuilderLock = new object();

        /// <summary>
        /// The collection of suites
        /// </summary>
        private List<TestCase> _testResults = new();

        /// <summary>
        /// Constructor
        /// </summary>
        public TestResultsBuilder()
        {
        }

        /// <summary>
        /// Publish the collected results
        /// </summary>
        public void Publish()
        {
            // Create the payload
            var payload = new TestCasePayload(
                    typeof(TestResultsBuilder).Assembly.GetName().Version.ToString(),
                    GitContextReader.GetGitContext(),
                    _testResults
                    );
            DevFeedbackPublisher.Publish(null, payload, DevLocalDataType.NUint);
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        internal void ReportSkipped(string name, string classname, decimal time)
        {
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        internal void ReportSuccess(string id, string name, string fullname, string methodname, string classname, decimal time)
        {
            var testCase = new TestCase()
            {
                Id = id,
                Name = name,
                Fullname = fullname,
                Methodname = methodname,
                Classname = classname,
            };
            lock (BuilderLock)
            {
                _testResults.Add(testCase);
            }
        }

        /// <summary>
        /// Report a failed test
        /// </summary>
        internal void ReportFailure(string name, string classname, decimal time, string message, string? text = null)
        {
        }

        /// <summary>
        /// Report an error
        /// </summary>
        internal void ReportError(string name, string classname, decimal time, string message, string? text = null)
        {
        }
    }
}
