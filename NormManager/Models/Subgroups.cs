using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "subgroups")]
    public class Subgroups
    {
        [XmlElement(ElementName = "item")]
        public ObservableCollection<ItemOfSubgroup> ItemOfSubgroup { get; set; } = new ObservableCollection<ItemOfSubgroup>();
    }
}
