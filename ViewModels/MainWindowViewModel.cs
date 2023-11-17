using NormManager.Services.Interfaces;
using System.Windows.Input;
using NormManager.Models;
using System.Xml.Serialization;
using System.Xml;
using NormManager.Commands;

namespace NormManager.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IStructureService _structureService;
        private string _path = string.Empty;
        private string _fileName;
        private XmlSerializer _xmlSerializer;

        public MainWindowViewModel(IWindowService windowService, IStructureService structureService) 
        {
            _windowService = windowService;
            _structureService = structureService;
        }
        
        /// <summary>
        /// Название XML файла
        /// </summary>
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        /// <summary>
        /// Путь до файла
        /// </summary>
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }

        /// <summary>
        /// Команда редактирования существующего файла
        /// </summary>
        public ICommand EditExists => new RelayCommand(obj =>
        {
            _xmlSerializer = new XmlSerializer(typeof(Main));
            using (XmlReader reader = XmlReader.Create(Path))
            {
                _structureService.SetReadyStructure((Main)_xmlSerializer.Deserialize(reader));
            }

            _structureService.SetFileName(FileName);

            _windowService.Show<EditorTreeViewModel>();

        }, (obj) => Path != string.Empty);

        /// <summary>
        /// Команда создания нового XML файла
        /// </summary>
        public ICommand CreateNew => new RelayCommand((obj) =>
        {
            _structureService.AddDefaultParams();
            _windowService.Show<CreateNewXmlViewModel>();
        });
    }
}
