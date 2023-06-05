using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "params")]
    public class Params
    {
        [XmlElement(ElementName = "item")]
        public ObservableCollection<ItemOfParams> ItemOfParams { get; set; } = new ObservableCollection<ItemOfParams>();
    }
}
