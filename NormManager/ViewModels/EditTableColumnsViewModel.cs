using NormManager.Commands;
using NormManager.Models;
using NormManager.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NormManager.ViewModels
{
    public class EditTableColumnsViewModel : BaseViewModel
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

        public EditTableColumnsViewModel(
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
                    bool isUsed = false;
                    var selectedMeasurableQuantity = measurableQuantityList.First(x => x.MeasurableQuantityName == _treeService.SelectedNameMeasurableQuantity).ParametersIncludeInValue;
                    for (int j = 0; j < selectedMeasurableQuantity.Count; j++)
                    {
                        if (paramList[i] == selectedMeasurableQuantity[j])
                        {
                            isUsed = true;
                            UsedParametersNames.Add(selectedMeasurableQuantity[j].Name.Text);
                            UsedParameters.Add(selectedMeasurableQuantity[j]);
                        }
                    }

                    if (!isUsed)
                    {
                        AllParametersNames.Add(paramList[i].Name.Text);
                        AllParameters.Add(paramList[i]);
                    }

                    CountInputUsedParameters = measurableQuantityList.First(x => x.MeasurableQuantityName == _treeService.SelectedNameMeasurableQuantity).CountInputParameters;
                }
            }
        }

        /// <summary>
        /// Количество используемых входных параметров
        /// </summary>
        public int CountInputUsedParameters
        {
            get => _countInputUsedParameters;
            set => SetProperty(ref _countInputUsedParameters, value);
        }

        /// <summary>
        /// Список используемых имен параметров
        /// </summary>
        public ObservableCollection<string> UsedParametersNames
        {
			get => _usedParametersNames;
            set => SetProperty(ref _usedParametersNames, value);
        }

        /// <summary>
        /// Список всех имен параметров
        /// </summary>
        public ObservableCollection<string> AllParametersNames
        {
            get => _allParametersNames;
            set => SetProperty(ref _allParametersNames, value);
        }

        /// <summary>
        /// Список используемых параметров
        /// </summary>
        public ObservableCollection<ItemOfParams> UsedParameters
        {
            get => _usedParameters;
            set => SetProperty(ref _usedParameters, value);
        }

        /// <summary>
        /// Список всех параметров
        /// </summary>
        public ObservableCollection<ItemOfParams> AllParameters
        {
            get => _allParameters;
            set => SetProperty(ref _allParameters, value);
        }

        /// <summary>
        /// Выбранный параметр из списка используемых
        /// </summary>
        public string SelectedUsedParameter
        {
            get => _selectedUsedParameters;
            set => SetProperty(ref _selectedUsedParameters, value);
        }

        /// <summary>
        /// Выбранный параметр из списка всех параметров
        /// </summary>
        public string SelectedAllParameter
        {
            get => _selectedAllParameters;
            set => SetProperty(ref _selectedAllParameters, value);
        }

        /// <summary>
        /// Переместить параметр в левый список
        /// </summary>
        public ICommand SwapOneLeft => _swapOneLeft ??= new RelayCommand(obj =>
        {
            var param = AllParameters.First(x => x.Name.Text == SelectedAllParameter);
            UsedParameters.Add(param);
            AllParameters.Remove(param);

            UsedParametersNames.Add(SelectedAllParameter);
            AllParametersNames.Remove(SelectedAllParameter);

            SelectedAllParameter = string.Empty;
        }, (obj) => SelectedAllParameter != string.Empty);

        /// <summary>
        /// Переместить параметр в правый список
        /// </summary>
        public ICommand SwapOneRight => _swapOneRight ??= new RelayCommand(obj =>
        {
            var param = UsedParameters.First(x => x.Name.Text == SelectedUsedParameter);
            AllParameters.Add(param);
            UsedParameters.Remove(param);

            AllParametersNames.Add(SelectedUsedParameter);
            UsedParametersNames.Remove(SelectedUsedParameter);

            SelectedUsedParameter = string.Empty;
        }, (obj) => SelectedUsedParameter != string.Empty);

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
            AllParameters.Clear();
        }, (obj) => AllParametersNames.Count > 0);

        /// <summary>
        /// Переместить все параметры в правый список
        /// </summary>
        public ICommand SwapAllRight => _swapAllRight ??= new RelayCommand(obj =>
        {
            for (int i = 0; i < UsedParameters.Count; i++)
            {
                AllParametersNames.Add(UsedParametersNames[i]);
                AllParameters.Add(UsedParameters[i]);
            }

            UsedParametersNames.Clear();
            UsedParameters.Clear();
        }, (obj) => UsedParametersNames.Count > 0);

        /// <summary>
        /// Закончить редактирование столбцов в таблице
        /// </summary>
        public ICommand FinishEdit => _finishEdit ??= new RelayCommand(obj =>
        {
            _treeService.EditMeasurableQuantity(CountInputUsedParameters, UsedParameters);

            _windowService.Close<EditTableColumnsViewModel>();
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
            _windowService.Close<EditTableColumnsViewModel>();
        });
    }
}
