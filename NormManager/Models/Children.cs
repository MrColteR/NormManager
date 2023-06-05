using System.Xml.Serialization;

namespace NormManager.Models
{
    public class Children
    {
        [XmlElement(ElementName = "item")]
        public ItemOfChildren ItemOfChildren { get; set; } = new ItemOfChildren();
    }
}
