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

        public AgodaExecutor(AssemblyName assemblyName, ISourceInformationProvider sourceInformationProvider, IMessageSink diagnosticMessageSink, TestResultsBuilder builder)
            : base(assemblyName, sourceInformationProvider, diagnosticMessageSink)
        {
            _builder = builder;
        }

        protected override async void RunTestCases(IEnumerable<IXunitTestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
        {
            try
            {
                var assemblyRunner = new AgodaAssemblyRunner(TestAssembly, testCases, DiagnosticMessageSink, executionMessageSink, executionOptions, _builder);
                await assemblyRunner.RunAsync();
            }
            catch (Exception ex)
            {
                _builder.Diagnostic($"{ex.Message}");
                _builder.Diagnostic($"{ex.StackTrace}");
                throw;
            }
        }
    }
}
