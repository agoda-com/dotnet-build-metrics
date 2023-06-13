using System.Xml.Serialization;

namespace Agoda.Tests.Metrics.xUnit.Models
{
    [XmlRoot(ElementName = "testcase")]
    public class JUnitTestCase
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name;

        [XmlAttribute(AttributeName = "classname")]
        public string Classname;

        [XmlAttribute(AttributeName = "assertions")]
        public int Assertions;

        [XmlAttribute(AttributeName = "time")]
        public double Time;

        [XmlAttribute(AttributeName = "file")]
        public string? File = null;

        [XmlAttribute(AttributeName = "line")]
        public int Line;

        [XmlElement(ElementName = "skipped")]
        public JUnitSkipped? Skipped;

        [XmlElement(ElementName = "failure")]
        public JUnitFailure? Failure;

        [XmlElement(ElementName = "error")]
        public JUnitError? Error;

        [XmlElement(ElementName = "system-out")]
        public string? SystemOut = null;

        [XmlElement(ElementName = "system-err")]
        public string? SystemErr = null;

        [XmlElement(ElementName = "properties")]
        public JUnitProperties? Properties;

        [XmlText]
        public string? Text;
    }
}
