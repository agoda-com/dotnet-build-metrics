using Xunit.Abstractions;
using Xunit.Sdk;

namespace Agoda.Tests.Metrics.xUnit
{
    /// <summary>
    /// Run all tests in an assembly
    /// </summary>
    internal class AgodaAssemblyRunner : XunitTestAssemblyRunner
    {
        private readonly TestSuiteReporter? _reporter;

        public AgodaAssemblyRunner(ITestAssembly testAssembly, IEnumerable<IXunitTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions, TestResultsBuilder builder)
            : base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
        {
            // Treat each assembly as a suite and set up a report for it
            var name = Path.GetFileName(testAssembly.Assembly.AssemblyPath);
            _reporter = builder.BeginSuite(name, name);
        }

        protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus, ITestCollection testCollection, IEnumerable<IXunitTestCase> testCases, CancellationTokenSource cancellationTokenSource)
        {
            var messageBusWrapper = new AgodaMessageBus(messageBus, _reporter, DiagnosticMessageSink);
            return new XunitTestCollectionRunner(testCollection, testCases, DiagnosticMessageSink, messageBusWrapper, TestCaseOrderer, new ExceptionAggregator(Aggregator), cancellationTokenSource).RunAsync();
        }
    }
}
