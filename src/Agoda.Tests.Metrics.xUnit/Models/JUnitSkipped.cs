using System.Xml.Serialization;

namespace Agoda.Tests.Metrics.xUnit.Models
{
    [XmlRoot(ElementName = "skipped")]
    public class JUnitSkipped
    {
        [XmlAttribute(AttributeName = "message")]
        public string Message { get; set; }
    }
}
