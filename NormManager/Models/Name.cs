using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "name")]
    public class Name
    {
        [XmlAttribute(AttributeName = "Localizable")]
        public bool Localizable { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
