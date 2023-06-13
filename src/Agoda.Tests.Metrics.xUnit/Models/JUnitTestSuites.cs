using System.Xml.Serialization;

namespace Agoda.Tests.Metrics.xUnit.Models
{
    [XmlRoot(ElementName = "testsuites")]
    public class JUnitTestSuites
    {
        [XmlElement(ElementName = "testsuite")]
        public List<JUnitTestSuite> TestSuite { get; } = new List<JUnitTestSuite>();

        [XmlAttribute(AttributeName = "name")]
        public string? Name;

        [XmlAttribute(AttributeName = "tests")]
        public int Tests;

        [XmlAttribute(AttributeName = "failures")]
        public int Failures;

        [XmlAttribute(AttributeName = "errors")]
        public int Errors;

        [XmlAttribute(AttributeName = "skipped")]
        public int Skipped;

        [XmlAttribute(AttributeName = "assertions")]
        public int Assertions;

        [XmlAttribute(AttributeName = "time")]
        public double Time;

        [XmlAttribute(AttributeName = "timestamp")]
        public DateTime Timestamp = DateTime.Now;

        [XmlText]
        public string Text = String.Empty;
    }
}

