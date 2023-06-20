﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Agoda.Tests.Metrics.xUnit
{
    /// <summary>
    /// Top level test executor
    /// </summary>
    internal class AgodaExecutor : XunitTestFrameworkExecutor
    {
        private readonly TestResultsBuilder _builder;
        private readonly IMessageSink _messageSink;

        public AgodaExecutor(AssemblyName assemblyName, ISourceInformationProvider sourceInformationProvider, IMessageSink diagnosticMessageSink, TestResultsBuilder builder, IMessageSink messageSink)
            : base(assemblyName, sourceInformationProvider, diagnosticMessageSink)
        {
            _builder = builder;
            _messageSink = messageSink;
        }

        protected override async void RunTestCases(IEnumerable<IXunitTestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
        {
            try
            {
                var assemblyRunner = new AgodaAssemblyRunner(TestAssembly, testCases, DiagnosticMessageSink, executionMessageSink, executionOptions, _builder);
                await assemblyRunner.RunAsync();
                _builder.Publish();
                _messageSink.OnMessage(new DiagnosticMessage("Published test results"));
            }
            catch (Exception ex)
            {
                _messageSink.OnMessage(new DiagnosticMessage($"{ex.Message}"));
                _messageSink.OnMessage(new DiagnosticMessage($"{ex.StackTrace}"));
                throw;
            }
        }
    }
}
