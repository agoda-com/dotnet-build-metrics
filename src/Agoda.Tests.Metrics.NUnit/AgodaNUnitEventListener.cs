using NUnit.Engine;
using System;
using NUnit.Engine.Extensibility;
using System.Xml.Linq;
using Agoda.DevFeedback.Common;

namespace Agoda.Tests.Metrics.NUnit
{
    [Extension]
    public class AgodaNUnitEventListener : ITestEventListener
    {
        public AgodaNUnitEventListener()
        {

        }
        public void OnTestEvent(string report)
        {
            try
            {
                var xmlConverter = new NUnitXmlEventConverter(report);

                if (xmlConverter.TestCases.Count == 0) return;
                if (!report.StartsWith("<test-run")) return;

                var gitContext = GitContextReader.GetGitContext();

                var data = new NUnitTestCasePayload(
                    typeof(AgodaNUnitEventListener).Assembly.GetName().Version.ToString(),
                    gitContext,
                    xmlConverter.TestCases
                );
                DevFeedbackPublisher.Publish(null, data, DevLocalDataType.NUint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
