using NormManager.Commands;
using NormManager.Services.Interfaces;
using System.IO;
using System.Windows.Input;
using NormManager.Models;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Linq;

namespace NormManager.ViewModels
{
    public class EditorTreeViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IStructureService _structureService;
        private readonly ITreeService _treeService;

        private ICommand _close;
        private ICommand _addMeasurableQuantity;
        private ICommand _editMeasurableQuantity;
        private ICommand _removeMeasurableQuantity;
        private ICommand _addFolder;
        private ICommand _removeFolder;
        private ICommand _moveUpFolder;
        private ICommand _moveDownFolder;
        private ICommand _moveUpMeasurableQuantity;
        private ICommand _moveDownMeasurableQuantity;
        private ICommand _serialize;
        private ICommand _addParams;

        private bool _isMeasurableQuantity;
        private MainTreeElement _selectedMainItem = new();
        private SubmainTreeElement _selectedSubmainItem;

        public EditorTreeViewModel(
            IWindowService windowService,
            IStructureService structureService,
            ITreeService treeService)
        {
            _windowService = windowService;
            _structureService = structureService;
            _treeService = treeService;
            MainElemenstList = _treeService.MainTreeElementsList;
        }

        /// <summary>
        /// Список элементов для дерева
        /// </summary>
        public ObservableCollection<MainTreeElement> MainElemenstList { get; set; }

        /// <summary>
        /// Выбранная папка
        /// </summary>
        public MainTreeElement SelectedMainItem 
        {
            get => _selectedMainItem;
            set
            {
                _selectedMainItem = value;
            }
        } 

        /// <summary>
        /// Выбранный элемент
        /// </summary>
        public SubmainTreeElement SelectedSubmainItem 
        {
            get => _selectedSubmainItem;
            set
            {
                _selectedSubmainItem = value;
                UpdatePropertiesSelectedItem();
            }
        }

        /// <summary>
        /// Выбрано ли входное значение
        /// </summary>
        public bool IsMeasurableQuantity 
        {
            get => _isMeasurableQuantity; 
            set
            {
                _isMeasurableQuantity = value;
                OnPropertyChanged(nameof(IsMeasurableQuantity));
            }
        }

        /// <summary>
        /// Команда закрытия окна
        /// </summary>
        public ICommand Close => _close ??= new RelayCommand(obj =>
        {
            _structureService.ClearStructure();
            _treeService.ClearTree();
            _windowService.Close<EditorTreeViewModel>();
        });

        /// <summary>
        /// Команда добавление папки
        /// </summary>
        public ICommand AddFolder => _addFolder ??= new RelayCommand(obj =>
        {
            _windowService.Show<CreateNewFolderViewModel>();
        }, (obj) => SelectedSubmainItem == null);

        /// <summary>
        /// Команда удаления папки
        /// </summary>
        public ICommand RemoveFolder => _removeFolder ??= new RelayCommand(obj =>
        {
            _structureService.RemoveFolder(SelectedMainItem.FolderName);
        }, (obj) => SelectedMainItem != null && SelectedSubmainItem == null);

        /// <summary>
        /// Команда добавления элемента
        /// </summary>
        public ICommand AddMeasurableQuantity => _addMeasurableQuantity ??= new RelayCommand(obj =>
        {
            _treeService.SelectedFolder = SelectedMainItem.FolderName;
            _windowService.Show<CreateMeasureValueViewModel>();
        }, (obj) => SelectedMainItem != null);

        public ICommand EditMeasurableQuantity => _editMeasurableQuantity ??= new RelayCommand(obj =>
        {
            _treeService.SelectedFolder = SelectedMainItem.FolderName;
            _treeService.SelectedMeasurableQuantity = SelectedSubmainItem.MeasurableQuantityName;
            _windowService.Show<EditMeasureValueViewModel>();
        }, (obj) => SelectedSubmainItem != null);

        /// <summary>
        /// Команда удаление элемента
        /// </summary>
        public ICommand RemoveMeasurableQuantity => _removeMeasurableQuantity ??= new RelayCommand(obj =>
        {
            _structureService.RemoveMeasurableQuantity(SelectedSubmainItem.FolderName, SelectedSubmainItem.MeasurableQuantityName);
        }, (obj) => SelectedSubmainItem != null);

        /// <summary>
        /// Команда перемещение папки вверх 
        /// </summary>
        public ICommand MoveUpFolder => _moveUpFolder ??= new RelayCommand(obj =>
        {
            _structureService.MoveUpFolder(SelectedMainItem.FolderName);         
        }, 
        (obj) => (SelectedMainItem != null && MainElemenstList.IndexOf(SelectedMainItem) > 0));

        /// <summary>
        /// Команда перемещение папки вниз
        /// </summary>
        public ICommand MoveDownFolder => _moveDownFolder ??= new RelayCommand(obj =>
        {
            _structureService.MoveDownFolder(SelectedMainItem.FolderName);
        },
        (obj) => (SelectedMainItem != null && MainElemenstList.IndexOf(SelectedMainItem) != -1 && MainElemenstList.IndexOf(SelectedMainItem) < MainElemenstList.Count - 1));

        /// <summary>
        /// Команда перемещение элемента вверх 
        /// </summary>
        public ICommand MoveUpMeasurableQuantity => _moveUpMeasurableQuantity ??= new RelayCommand(obj =>
        {
            _structureService.MoveUpMeasurableQuantity(SelectedMainItem.FolderName, SelectedSubmainItem.MeasurableQuantityName);
        },
        (obj) => SelectedSubmainItem != null && MainElemenstList.First(x => x == SelectedMainItem).SubmainElementsList.IndexOf(SelectedSubmainItem) > 0);

        /// <summary>
        /// Команда перемещение элемента вниз
        /// </summary>
        public ICommand MoveDownMeasurableQuantity => _moveDownMeasurableQuantity ??= new RelayCommand(obj =>
        {
            _structureService.MoveDownMeasurableQuantity(SelectedMainItem.FolderName, SelectedSubmainItem.MeasurableQuantityName);
        }, 
        (obj) => SelectedSubmainItem != null &&
                 MainElemenstList.First(x => x == SelectedMainItem).SubmainElementsList.IndexOf(SelectedSubmainItem) < MainElemenstList.First(x => x == SelectedMainItem).SubmainElementsList.Count - 1);

        /// <summary>
        /// Создать XML файл
        /// </summary>
        public ICommand Serialize => _serialize ??= new RelayCommand(obj =>
        {
            using (StreamWriter fs = new($"{_structureService.FileName}.xml", false))
            {
                XmlSerializer xmlSerializer = new(typeof(Main));
                xmlSerializer.Serialize(fs, _structureService.Structure);
            }

            _windowService.Close<EditorTreeViewModel>();
        });

        /// <summary>
        /// Добавить параметры
        /// </summary>
        public ICommand AddParams => _addParams ??= new RelayCommand(obj =>
        {
            _windowService.Show<AddParamsViewModel>();
        });

        private void UpdatePropertiesSelectedItem() => IsMeasurableQuantity = SelectedSubmainItem is not null ? true : false;
    }
}
