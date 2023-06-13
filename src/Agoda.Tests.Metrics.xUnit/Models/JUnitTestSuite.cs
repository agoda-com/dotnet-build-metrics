using System.Xml.Serialization;

namespace Agoda.Tests.Metrics.xUnit.Models
{
    [XmlRoot(ElementName = "testsuite")]
    public class JUnitTestSuite
    {
        [XmlElement(ElementName = "properties")]
        public JUnitProperties? Properties;

        [XmlElement(ElementName = "system-out")]
        public string? SystemOut;

        [XmlElement(ElementName = "system-err")]
        public string? SystemErr;

        [XmlElement(ElementName = "testcase")]
        public List<JUnitTestCase> Testcase = new();

        [XmlAttribute(AttributeName = "name")]
        public string? Name;

        [XmlAttribute(AttributeName = "tests")]
        public int Tests => Testcase.Count;

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

        [XmlAttribute(AttributeName = "file")]
        public string? File;

        [XmlText]
        public string? Text;

        // custom attr?
        [XmlAttribute(AttributeName = "hostname")]
        public string? Hostname;

        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "package")]
        public string? Package;
    }
}
