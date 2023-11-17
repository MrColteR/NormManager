using System.Collections.ObjectModel;

namespace NormManager.Models
{
    public class MainTreeElement : BaseModel
    {
        private ObservableCollection<SubmainTreeElement> _submainElementsList = new();

        public string FolderName { get; set; }

        public ObservableCollection<SubmainTreeElement> SubmainElementsList 
        {
            get => _submainElementsList;
            set => SetProperty(ref _submainElementsList, value);
        }
    }
}
