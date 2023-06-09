using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Agoda.Tests.Metrics.NUnit
{
    public class NUnitXmlEventConverter
    {
        public IList<TestCase> TestCases => _testCases;
        private XmlSerializer serialTestCase = new XmlSerializer(typeof(TestCase));
        private readonly List<TestCase> _testCases = new List<TestCase>();
        public NUnitXmlEventConverter(string report)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(report);
            XmlNode root = doc.DocumentElement;
            
            var nodes = root.SelectNodes("//test-case");

            foreach (var node in nodes)
            {
                using (XmlNodeReader reader = new XmlNodeReader((XmlNode)node))
                {
                    _testCases.Add((TestCase)serialTestCase.Deserialize(reader));
                }
            }
        }
    }
}