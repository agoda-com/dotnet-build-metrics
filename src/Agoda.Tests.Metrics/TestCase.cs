using System;
using System.Xml.Serialization;

namespace Agoda.Tests.Metrics
{
    /// <summary>
    /// NUnit TestCase schema as reported by events.
    /// https://docs.nunit.org/articles/nunit/technical-notes/usage/Test-Result-XML-Format.html#test-case
    /// </summary>
    [XmlRoot(ElementName = "test-case")]
    public class TestCase
    {

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

        [XmlAttribute(AttributeName = "start-time")]
        public DateTime StartTime { get; set; }

        [XmlAttribute(AttributeName = "end-time")]
        public DateTime EndTime { get; set; }

        [XmlAttribute(AttributeName = "duration")]
        public double Duration { get; set; }

        [XmlAttribute(AttributeName = "asserts")]
        public int Asserts { get; set; }

        [XmlAttribute(AttributeName = "parentId")]
        public string ParentId { get; set; }
    }
}