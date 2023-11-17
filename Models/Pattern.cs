using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "pattern")]
    public class Pattern
    {
        [XmlElement(ElementName = "inherits")]
        public int Inherits { get; set; }

        [XmlElement(ElementName = "items")]
        public Items Items { get; set; } = new Items();

        [XmlElement(ElementName = "ccols")]
        public int Ccols { get; set; }
    }
}
