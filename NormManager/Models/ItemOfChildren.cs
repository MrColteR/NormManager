using System.Xml.Serialization;

namespace NormManager.Models
{
    public class ItemOfChildren
    {
        [XmlElement(ElementName = "enumindex")]
        public string EnumIndex { get; set; }

        [XmlElement(ElementName = "enumid")]
        public string EnumId { get; set; }

        [XmlElement(ElementName = "upper")]
        public string Upper { get; set; }

        [XmlElement(ElementName = "leaf")]
        public int Leaf { get; set; } = 0;

        [XmlElement(ElementName = "visible")]
        public int Visible { get; set; } = 1;

        [XmlElement(ElementName = "upperincl")]
        public int Upperincl { get; set; } = 1;

        [XmlElement(ElementName = "lower")]
        public string Lower { get; set; }

        [XmlElement(ElementName = "lowerincl")]
        public int Lowerincl { get; set; } = 1;

        [XmlElement(ElementName = "treelevel")]
        public TreeLevel TreeLevel { get; set; }
    }
}
