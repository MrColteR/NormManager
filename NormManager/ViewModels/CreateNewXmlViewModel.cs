using NormManager.Commands;
using NormManager.Services.Interfaces;
using System.Windows.Input;

namespace NormManager.ViewModels
{
    public class CreateNewXmlViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IStructureService _structureService;
        private string _fileName = "EdirorNorm";

        public CreateNewXmlViewModel(IWindowService windowService, IStructureService structureService)
        {
            _windowService = windowService;
            _structureService = structureService;
        }

        /// <summary>
        /// Навзание XML файла
        /// </summary>
        public string FileName 
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        /// <summary>
        /// Команда создания нового визуального дерева
        /// </summary>
        public ICommand CreateNewTree => new RelayCommand((obj) =>
        {
            _structureService.SetFileName(FileName);

            CloseWindow();

            _windowService.Show<EditorTreeViewModel>();

        }, (obj) => FileName != string.Empty);

        /// <summary>
        /// Комнада закрытия окна
        /// </summary>
        public ICommand Close => new RelayCommand((obj) => CloseWindow());

        private void CloseWindow() => _windowService.Close<CreateNewXmlViewModel>();
    }
}
