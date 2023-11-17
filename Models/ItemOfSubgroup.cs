using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "item")]
    public class ItemOfSubgroup
    {
        [XmlElement(ElementName = "quantities")]
        public Quantities Quantities { get; set; } = new Quantities();

        [XmlElement(ElementName = "pattern")]
        public Pattern Pattern { get; set; } = new Pattern();
    }
}
