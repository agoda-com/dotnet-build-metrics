using System.Collections.Generic;
using System.Threading.Tasks;
using Agoda.DevFeedback.Common;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Agoda.Tests.Metrics.xUnit
{
    /// <summary>
    /// Allow test results to be built up as tests progress
    /// </summary>
    internal class TestResultsBuilder
    {
        ///--- Used for synchronization
        private static object BuilderLock = new object();

        //--- State
        private List<TestCase> _testResults = new List<TestCase>();
        private IMessageSink _messageSink;
        private GitContext _gitContext;

        /// <summary>
        /// Constructor
        /// </summary>
        public TestResultsBuilder(IMessageSink messageSink)
        {
            _messageSink = messageSink;
            _gitContext = new GitContext();
        }

        /// <summary>
        /// Generate a diagnostic message
        /// </summary>
        public void Diagnostic(string message)
        {
            _messageSink?.OnMessage(new DiagnosticMessage(message ?? "** NO MESSAGE **"));
        }

        /// <summary>
        /// Publish the collected results
        /// </summary>
        public void Publish()
        {
            Diagnostic("BEGIN: Publish()");
            lock (BuilderLock)
            {
                // Create the payload
                Diagnostic("Creating payload");
                var payload = new TestCasePayload(
                        typeof(TestResultsBuilder).Assembly.GetName().Version.ToString(),
                        _gitContext,
                        _testResults
                        );
                // Publish it
                Diagnostic("Calling DevFeedbackPublisher");
                DevFeedbackPublisher.Publish(null, payload, DevLocalDataType.NUnit);
            }
            Diagnostic("END: Publish()");
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        internal void ReportSkipped(string id, string name, string fullname, string methodname, string classname, double duration)
        {
            var testCase = new TestCase()
            {
                Id = id,
                Name = name,
                Fullname = fullname,
                Methodname = methodname,
                Classname = classname,
                Result = "Skipped",
                Duration = duration
            };
            lock (BuilderLock)
            {
                _testResults.Add(testCase);
            }
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        internal void ReportSuccess(string id, string name, string fullname, string methodname, string classname, double duration)
        {
            var testCase = new TestCase()
            {
                Id = id,
                Name = name,
                Fullname = fullname,
                Methodname = methodname,
                Classname = classname,
                Result = "Passed",
                Duration = duration
            };
            lock (BuilderLock)
            {
                _testResults.Add(testCase);
            }
        }

        /// <summary>
        /// Report a failed test
        /// </summary>
        internal void ReportFailure(string id, string name, string fullname, string methodname, string classname, double duration)
        {
            var testCase = new TestCase()
            {
                Id = id,
                Name = name,
                Fullname = fullname,
                Methodname = methodname,
                Classname = classname,
                Result = "Failed",
                Duration = duration
            };
            lock (BuilderLock)
            {
                _testResults.Add(testCase);
            }
        }
    }
}
