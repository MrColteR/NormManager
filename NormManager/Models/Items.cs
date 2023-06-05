using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "items")]
    public class Items
    {
        [XmlElement(ElementName = "string")]
        public ObservableCollection<string> String { get; set; } = new ObservableCollection<string>();
    }
}
