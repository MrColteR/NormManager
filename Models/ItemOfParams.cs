using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "item")]
    public class ItemOfParams
    {
        [XmlElement(ElementName = "uid")]
        public string Uid { get; set; }

        [XmlElement(ElementName = "name")]
        public Name Name { get; set; } = new Name();

        [XmlElement(ElementName = "fname")]
        public string Fname { get; set; }

        [XmlElement(ElementName = "lowerbound")]
        public string Lowerbound { get; set; }

        [XmlElement(ElementName = "upperbound")]
        public string Upperbound { get; set; }

        [XmlElement(ElementName = "type")]
        public Type Type { get; set; } = new Type();
    }
}
