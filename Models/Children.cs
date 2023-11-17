using System.Collections.Generic;
using System.Xml.Serialization;

namespace NormManager.Models
{
    public class Children
    {
        [XmlElement(ElementName = "item")]
        public List<ItemOfChildren> ItemOfChildren { get; set; } = new List<ItemOfChildren>();
    }
}
