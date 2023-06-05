using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "groups")]
    public class Groups
    {
        [XmlElement(ElementName = "item")]
        public ObservableCollection<ItemOfGroup> ItemOfGroup { get; set; } = new ObservableCollection<ItemOfGroup>();
    }
}
