using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace NormManager.Models
{
    [XmlRoot(ElementName = "enum")]
    public class Enum
    {
        [XmlElement(ElementName = "string")]
        public ObservableCollection<String> String { get; set; } = new ObservableCollection<String>();

        [XmlElement(ElementName = "uid")]
        public ObservableCollection<string> Uid { get; set; } = new ObservableCollection<string>();
    }
}
