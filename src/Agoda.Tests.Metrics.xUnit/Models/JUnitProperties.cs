using System.Xml.Serialization;

namespace Agoda.Tests.Metrics.xUnit.Models
{
    [XmlRoot(ElementName = "properties")]
    public class JUnitProperties
    {
        [XmlElement(ElementName = "property")]
        public List<JUnitProperty> Property { get; set; }
    }
}
