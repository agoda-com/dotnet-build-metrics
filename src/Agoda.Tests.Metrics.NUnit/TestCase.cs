using System;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Agoda.Tests.Metrics.NUnit
{
    [XmlRoot(ElementName = "test-case")]
    public class TestCase
    {
        private static readonly string[] Formats =
        {
            "yyyy-MM-ddTHH:mm:ss.fffffffZ",
            "yyyy-MM-dd HH:mm:ss.fffffffZ",
            "yyyy-MM-ddTHH:mm:ss.fffffff",
            "yyyy-MM-dd HH:mm:ss.fffffff",
            "yyyy-MM-ddTHH:mm:ssZ",
            "yyyy-MM-dd HH:mm:ssZ",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
        };

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "fullname")]
        public string Fullname { get; set; }

        [XmlAttribute(AttributeName = "methodname")]
        public string Methodname { get; set; }

        [XmlAttribute(AttributeName = "classname")]
        public string Classname { get; set; }

        [XmlAttribute(AttributeName = "runstate")]
        public string Runstate { get; set; }

        [XmlAttribute(AttributeName = "seed")]
        public int Seed { get; set; }

        [XmlAttribute(AttributeName = "result")]
        public string Result { get; set; }

        [XmlIgnore]
        public DateTime StartTime { get; set; }

        [XmlAttribute(AttributeName = "start-time")]
        public string StartTimeRaw
        {
            get => StartTime.ToString("yyyy-MM-ddTHH:mm:ss");
            set => StartTime = DateTime.TryParseExact(value, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : DateTime.MinValue;
        }

        [XmlIgnore]
        public DateTime EndTime { get; set; }

        [XmlAttribute(AttributeName = "end-time")]
        public string EndTimeRaw
        {
            get => EndTime.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
            set => EndTime = DateTime.TryParseExact(value, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : DateTime.MinValue;
        }

        [XmlAttribute(AttributeName = "duration")]
        public double Duration { get; set; }

        [XmlAttribute(AttributeName = "asserts")]
        public int Asserts { get; set; }

        [XmlAttribute(AttributeName = "parentId")]
        public string ParentId { get; set; }
    }
}