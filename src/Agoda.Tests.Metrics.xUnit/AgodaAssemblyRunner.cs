using Xunit.Abstractions;
using Xunit.Sdk;

namespace Agoda.Tests.Metrics.xUnit
{
    /// <summary>
    /// Run all tests in an assembly
    /// </summary>
    internal class AgodaAssemblyRunner : XunitTestAssemblyRunner
    {
        private readonly TestResultsBuilder? _builder;

        public AgodaAssemblyRunner(ITestAssembly testAssembly, IEnumerable<IXunitTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions, TestResultsBuilder builder)
            : base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
        {
            _builder = builder;
        }

        protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus, ITestCollection testCollection, IEnumerable<IXunitTestCase> testCases, CancellationTokenSource cancellationTokenSource)
        {
            var messageBusWrapper = new AgodaMessageBus(messageBus, _builder, DiagnosticMessageSink);
            return new XunitTestCollectionRunner(testCollection, testCases, DiagnosticMessageSink, messageBusWrapper, TestCaseOrderer, new ExceptionAggregator(Aggregator), cancellationTokenSource).RunAsync();
        }
    }
}
