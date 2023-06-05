using System.Collections.Generic;
using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "main")]
    public class Main
    {
        [XmlElement(ElementName = "version")]
        public string Version { get; set; } = "1.0";

        [XmlElement(ElementName = "groups")]
        public Groups Groups { get; set; } = new Groups();

        [XmlElement(ElementName = "params")]
        public Params Params { get; set; } = new Params();
    }

}
