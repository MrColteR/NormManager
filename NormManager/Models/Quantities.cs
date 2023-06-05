using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "quantities")]
    public class Quantities
    {
        [XmlElement(ElementName = "item")]
        public ItemOfType ItemOfType { get; set; } = new ItemOfType();
    }
}
