using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "item")]
    public class ItemOfType
    {
        [XmlElement(ElementName = "name")]
        public Name Name { get; set; } = new Name();

        [XmlElement(ElementName = "type")]
        public string ItemType { get; set; }

        [XmlElement(ElementName = "treelevel")]
        public TreeLevel TreeLevel { get; set; } = new TreeLevel();
    }
}
