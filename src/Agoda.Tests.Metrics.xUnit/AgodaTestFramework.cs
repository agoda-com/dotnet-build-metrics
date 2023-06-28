using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Agoda.Tests.Metrics.xUnit
{
    /// <summary>
    /// Customised wrapper around xUnit to generate test metrics
    /// </summary>
    public class AgodaTestFramework : XunitTestFramework
    {
        private readonly TestResultsBuilder _builder;

        /// <summary>
        /// Constructor with diagnostic message output
        /// </summary>
        public AgodaTestFramework(IMessageSink messageSink) : base(messageSink)
        {
            _builder = new TestResultsBuilder(messageSink);
            _builder.Diagnostic("Using AgodaTestFramework");
        }

        /// <summary>
        /// Use our custom executor to detect and run all tests
        /// </summary>
        protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
            => new AgodaExecutor(assemblyName, SourceInformationProvider, DiagnosticMessageSink, _builder);
    }
}
