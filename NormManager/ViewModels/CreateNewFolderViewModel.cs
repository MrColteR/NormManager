using NormManager.Commands;
using NormManager.Services.Interfaces;
using System.Windows.Input;

namespace NormManager.ViewModels
{
    public class CreateNewFolderViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IStructureService _structureService;
        private string _folderName = string.Empty;

        public CreateNewFolderViewModel(
            IWindowService windowService, 
            IStructureService structureService)
        {
            _structureService = structureService;
            _windowService = windowService;
        }

        /// <summary>
        /// Название папки
        /// </summary>
        public string FolderName
        {
            get => _folderName;
            set => SetProperty(ref _folderName, value);
        }

        /// <summary>
        /// Команда создания новой папки
        /// </summary>
        public ICommand CreateNewFolder => new RelayCommand((obj) =>
        {
            _structureService.AddNewFolder(FolderName);
            CloseWindow();

        }, (obj) => FolderName != string.Empty);

        /// <summary>
        /// Команда закрытия окна
        /// </summary>
        public ICommand Close => new RelayCommand((obj) => CloseWindow());

        private void CloseWindow() => _windowService.Close<CreateNewFolderViewModel>();
    }
}
