using System.Xml.Serialization;

namespace NormManager.Models
{
    public class TreeLevel
    {
        [XmlElement(ElementName = "children")]
        public Children Children { get; set; } = new Children();

        [XmlElement(ElementName = "CPID")]
        public string Cpid { get; set; } = string.Empty;

        [XmlElement(ElementName = "empty")]
        public int Empty { get; set; } = 0;
    }
}
