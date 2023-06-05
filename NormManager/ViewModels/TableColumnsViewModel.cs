using NormManager.Commands;
using NormManager.Models;
using NormManager.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NormManager.ViewModels
{
    public class TableColumnsViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IStructureService _structureService;
        private readonly ITreeService _treeService;

        private ICommand _swapOneLeft;
        private ICommand _swapOneRight;
        private ICommand _swapAllLeft;
        private ICommand _swapAllRight;
        private ICommand _finishEdit;
        private ICommand _addInputUsedParameters;
        private ICommand _removeInputUsedParameters;
        private ICommand _close;

        private ObservableCollection<string> _usedParametersNames = new();
        private ObservableCollection<string> _allParametersNames = new();
        private ObservableCollection<ItemOfParams> _usedParameters = new();
        private ObservableCollection<ItemOfParams> _allParameters = new();
        private string _selectedUsedParameters = string.Empty;
        private string _selectedAllParameters = string.Empty;
        private int _countInputUsedParameters;

        public TableColumnsViewModel(
            IWindowService windowService,
            IStructureService structureService,
            ITreeService treeService)
        {
            _windowService = windowService;
            _structureService = structureService;
            _treeService = treeService;

            var groupList = _structureService.Structure.Groups.ItemOfGroup;
            var paramList = _structureService.Structure.Params.ItemOfParams;

            if (groupList.Count > 0)
            {
                var measurableQuantityList = _treeService.MainTreeElementsList.First(x => x.FolderName == _treeService.SelectedFolder).SubmainElementsList;
                for (int i = 0; i < paramList.Count; i++)
                {
                    AllParametersNames.Add(paramList[i].Name.Text);
                    AllParameters.Add(paramList[i]);
                }
            }
        }

        /// <summary>
        /// Количество используемых входных параметров
        /// </summary>
        public int CountInputUsedParameters
        {
            get => _countInputUsedParameters; 
            set 
            { 
                _countInputUsedParameters = value;
                OnPropertyChanged(nameof(CountInputUsedParameters));
            }
        }

        /// <summary>
        /// Список используемых имен параметров
        /// </summary>
        public ObservableCollection<string> UsedParametersNames
        {
			get => _usedParametersNames;
            set 
			{
                _usedParametersNames = value;
				OnPropertyChanged(nameof(UsedParametersNames));
			}
		}

        /// <summary>
        /// Список всех имен параметров
        /// </summary>
        public ObservableCollection<string> AllParametersNames
        {
            get => _allParametersNames;
            set
            {
                _allParametersNames = value;
                OnPropertyChanged(nameof(AllParametersNames));
            }
        }

        /// <summary>
        /// Список используемых параметров
        /// </summary>
        public ObservableCollection<ItemOfParams> UsedParameters
        {
            get => _usedParameters;
            set
            {
                _usedParameters = value;
                OnPropertyChanged(nameof(UsedParameters));
            }
        }

        /// <summary>
        /// Список всех параметров
        /// </summary>
        public ObservableCollection<ItemOfParams> AllParameters
        {
            get => _allParameters;
            set
            {
                _allParameters = value;
                OnPropertyChanged(nameof(AllParameters));
            }
        }

        /// <summary>
        /// Выбранный параметр из списка используемых
        /// </summary>
        public string SelectedUsedParameters
        {
            get => _selectedUsedParameters;
            set
            {
                _selectedUsedParameters = value;
                OnPropertyChanged(nameof(SelectedUsedParameters));
            }
        }

        /// <summary>
        /// Выбранный параметр из списка всех параметров
        /// </summary>
        public string SelectedAllParameters
        {
            get => _selectedAllParameters;
            set
            {
                _selectedAllParameters = value;
                OnPropertyChanged(nameof(SelectedAllParameters));
            }
        }

        /// <summary>
        /// Переместить параметр в левый список
        /// </summary>
        public ICommand SwapOneLeft => _swapOneLeft ??= new RelayCommand(obj =>
        {
            var param = AllParameters.First(x => x.Name.Text == SelectedAllParameters);
            UsedParameters.Add(param);
            AllParameters.Remove(param);

            UsedParametersNames.Add(SelectedAllParameters);
            AllParametersNames.Remove(SelectedAllParameters);

            SelectedAllParameters = string.Empty;
        }, (obj) => SelectedAllParameters != string.Empty);

        /// <summary>
        /// Переместить параметр в правый список
        /// </summary>
        public ICommand SwapOneRight => _swapOneRight ??= new RelayCommand(obj =>
        {
            var param = UsedParameters.First(x => x.Name.Text == SelectedUsedParameters);
            AllParameters.Add(param);
            UsedParameters.Remove(param);

            AllParametersNames.Add(SelectedUsedParameters);
            UsedParametersNames.Remove(SelectedUsedParameters);

            SelectedUsedParameters = string.Empty;
        }, (obj) => SelectedUsedParameters != string.Empty);

        /// <summary>
        /// Переместить все параметры в левый список
        /// </summary>
        public ICommand SwapAllLeft => _swapAllLeft ??= new RelayCommand(obj =>
        {
            for (int i = 0; i < AllParametersNames.Count; i++)
            {
                UsedParametersNames.Add(AllParametersNames[i]);
                UsedParameters.Add(AllParameters[i]);
            }

            AllParametersNames.Clear();
        }, (obj) => AllParametersNames.Count > 0);

        /// <summary>
        /// Переместить все параметры в правый список
        /// </summary>
        public ICommand SwapAllRight => _swapAllRight ??= new RelayCommand(obj =>
        {
            for (int i = 0; i < UsedParametersNames.Count; i++)
            {
                AllParametersNames.Add(UsedParametersNames[i]);
                AllParameters.Add(UsedParameters[i]);
            }

            UsedParametersNames.Clear();
        }, (obj) => UsedParametersNames.Count > 0);

        /// <summary>
        /// Закончить редактирование столбцов в таблице
        /// </summary>
        public ICommand FinishEdit => _finishEdit ??= new RelayCommand(obj =>
        {
            _treeService.AddMeasurableQuantity(CountInputUsedParameters, UsedParameters);

            _windowService.Close<TableColumnsViewModel>();
        });

        /// <summary>
        /// Увеличить количество используемых входных параметров
        /// </summary>
        public ICommand AddInputUsedParameters => _addInputUsedParameters ??= new RelayCommand(obj =>
        {
            CountInputUsedParameters++;
        }, (obj) => CountInputUsedParameters != _usedParametersNames.Count);

        /// <summary>
        /// Уменьшить количество используемых входных параметров
        /// </summary>
        public ICommand RemoveInputUsedParameters => _removeInputUsedParameters ??= new RelayCommand(obj =>
        {
            CountInputUsedParameters--;
        }, (obj) => CountInputUsedParameters != 0);

        /// <summary>
        /// Команда закрытия окна
        /// </summary>
        public ICommand Close => _close ??= new RelayCommand(obj =>
        {
            _windowService.Close<TableColumnsViewModel>();
        });
    }
}
