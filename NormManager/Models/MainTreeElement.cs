using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NormManager.Models
{
    public class MainTreeElement : BaseModel
    {
        private ObservableCollection<SubmainTreeElement> _submainElementsList = new();
        public string FolderName { get; set; }

        public ObservableCollection<SubmainTreeElement> SubmainElementsList 
        {
            get => _submainElementsList;
            set 
            {
                _submainElementsList = value;
                OnPropertyChanged(nameof(SubmainElementsList));
            }
        }
    }
}
