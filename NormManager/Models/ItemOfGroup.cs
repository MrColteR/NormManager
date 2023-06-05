using System;
using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "item")]
    public class ItemOfGroup
    {
        [XmlElement(ElementName = "name")]
        public Name Name { get; set; } = new Name();

        [XmlElement(ElementName = "subgroups")]
        public Subgroups Subgroups { get; set; } = new Subgroups();

        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
    }
}
