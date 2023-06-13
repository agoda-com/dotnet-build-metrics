namespace Agoda.Tests.Metrics.xUnit
{
    /// <summary>
    /// Report test results
    /// </summary>
    internal class TestSuiteReporter
    {
        /// <summary>
        /// The build we are owned by
        /// </summary>
        private readonly TestResultsBuilder _builder;

        /// <summary>
        /// Constructor from a builder
        /// </summary>
        internal TestSuiteReporter(TestResultsBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        public void ReportSkipped(string name, string classname, decimal time)
        {
            _builder.ReportSkipped(this, name, classname, time);
        }

        /// <summary>
        /// Report a successful test
        /// </summary>
        public void ReportSuccess(string name, string classname, decimal time)
        {
            _builder.ReportSuccess(this, name, classname, time);
        }

        /// <summary>
        /// Report a failed test
        /// </summary>
        public void ReportFailure(string name, string classname, decimal time, string message, string? text = null)
        {
            _builder.ReportFailure(this, name, classname, time, message, text);
        }

        /// <summary>
        /// Report an error
        /// </summary>
        public void ReportError(string name, string classname, decimal time, string message, string? text = null)
        {
            _builder.ReportError(this, name, classname, time, message, text);
        }
    }
}
