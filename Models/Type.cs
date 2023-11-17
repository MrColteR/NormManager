using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "type")]
    public class Type
    {
        [XmlElement(ElementName = "kind")]
        public string Kind { get; set; }

        [XmlElement(ElementName = "unitname")]
        public Unitname Unitname { get; set; } = new Unitname();

        [XmlElement(ElementName = "enum")]
        public Enum Enum { get; set; } = new Enum();
    }
}
