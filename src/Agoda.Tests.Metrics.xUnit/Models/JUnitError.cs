using System.Xml.Serialization;

namespace Agoda.Tests.Metrics.xUnit.Models
{
    [XmlRoot(ElementName = "error")]
    public class JUnitError
    {
        [XmlAttribute(AttributeName = "message")]
        public string Message { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }
}
