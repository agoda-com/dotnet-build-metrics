using System.Collections.Generic;
using Agoda.DevFeedback.Common;
using Xunit.Abstractions;
using Xunit.Sdk;
using System;

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
            _gitContext = GitContextReader.GetGitContext();
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
            lock (BuilderLock)
            {
                // Create the payload
                var payload = new TestCasePayload(
                        typeof(TestResultsBuilder).Assembly.GetName().Version.ToString(),
                        _gitContext,
                        _testResults
                        );
                // Publish it
                DevFeedbackPublisher.Publish(null, payload, DevLocalDataType.NUnit);
            }
        }

        /// <summary>
        /// Fill out the test case
        /// </summary>
        private TestCase CreateTestCase(string result, string id, string name, string fullname, string methodname, string classname, double duration)
        {
            var when = DateTime.UtcNow;
            return new TestCase()
            {
                Id = id,
                Name = name,
                Fullname = fullname,
                Methodname = methodname,
                Runstate = "Runnable",
                Classname = classname,
                Result = result,
                StartTime = when.AddSeconds(-duration),
                Duration = duration,
                EndTime = when
            };
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        internal void ReportSkipped(string id, string name, string fullname, string methodname, string classname, double duration)
        {
            var testCase = CreateTestCase("Skipped", id, name, fullname, methodname, classname, duration);
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
            var testCase = CreateTestCase("Passed", id, name, fullname, methodname, classname, duration);
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
            var testCase = CreateTestCase("Failed", id, name, fullname, methodname, classname, duration);
            lock (BuilderLock)
            {
                _testResults.Add(testCase);
            }
        }
    }
}
