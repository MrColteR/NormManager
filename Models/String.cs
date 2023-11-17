using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "string")]
    public class String
    {
        [XmlAttribute(AttributeName = "Localizable")]
        public bool Localizable { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
