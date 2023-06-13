using System.Xml.Serialization;

namespace Agoda.Tests.Metrics.xUnit.Models
{
    [XmlRoot(ElementName = "property")]
    public class JUnitProperty
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public DateTime Value { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
