using Xunit.Abstractions;
using Xunit.Sdk;

namespace Agoda.Tests.Metrics.xUnit
{
    internal class AgodaMessageBus : IMessageBus
    {
        IMessageBus _messageBus;
        IMessageSink _messageSink;
        TestResultsBuilder _builder;

        /// <summary>
        /// Constructor that wraps an existing MessageBus
        /// </summary>
        public AgodaMessageBus(IMessageBus messageBus, TestResultsBuilder builder, IMessageSink messageSink)
        {
            _messageBus = messageBus;
            _messageSink = messageSink;
            _builder = builder;
        }

        public void Dispose()
        {
            _messageBus.Dispose();
        }

        public bool QueueMessage(IMessageSinkMessage message)
        {
            switch (message)
            {
                case TestPassed testPassed:
                    _builder?.ReportSuccess(
                        testPassed.Test.TestCase.UniqueID,
                        testPassed.Test.TestCase.TestMethod.Method.Name,
                        $"{testPassed.Test.TestCase.TestMethod.TestClass.Class.Name}.{testPassed.Test.TestCase.TestMethod.Method.Name}",
                        testPassed.Test.TestCase.TestMethod.Method.Name,
                        testPassed.Test.TestCase.TestMethod.TestClass.Class.Name,
                        (double)testPassed.ExecutionTime
                        );
                    break;
                case TestFailed testFailed:
                    _builder?.ReportFailure(
                        testFailed.Test.TestCase.UniqueID,
                        testFailed.Test.TestCase.TestMethod.Method.Name,
                        $"{testFailed.Test.TestCase.TestMethod.TestClass.Class.Name}.{testFailed.Test.TestCase.TestMethod.Method.Name}",
                        testFailed.Test.TestCase.TestMethod.Method.Name,
                        testFailed.Test.TestCase.TestMethod.TestClass.Class.Name,
                        (double)testFailed.ExecutionTime
                        );
                    break;
                case TestSkipped testSkipped:
                    _builder?.ReportFailure(
                        testSkipped.Test.TestCase.UniqueID,
                        testSkipped.Test.TestCase.TestMethod.Method.Name,
                        $"{testSkipped.Test.TestCase.TestMethod.TestClass.Class.Name}.{testSkipped.Test.TestCase.TestMethod.Method.Name}",
                        testSkipped.Test.TestCase.TestMethod.Method.Name,
                        testSkipped.Test.TestCase.TestMethod.TestClass.Class.Name,
                        (double)testSkipped.ExecutionTime
                        );
                    break;
                case TestCollectionFinished testCollectionFinished:
                    // Publish the results before passing the message on
                    _builder.Publish();
                    break;
                default:
                    _builder?.Diagnostic($"{message.GetType().Name}");
                    break;
            }
            return _messageBus.QueueMessage(message);
        }
    }
}
