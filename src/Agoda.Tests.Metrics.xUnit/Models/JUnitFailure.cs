using System.Xml.Serialization;

namespace Agoda.Tests.Metrics.xUnit.Models
{
    [XmlRoot(ElementName = "failure")]
    public class JUnitFailure
    {
        [XmlAttribute(AttributeName = "message")]
        public string Message;

        [XmlAttribute(AttributeName = "type")]
        public string Type = "failure";

        [XmlText]
        public string? Text;
    }
}
