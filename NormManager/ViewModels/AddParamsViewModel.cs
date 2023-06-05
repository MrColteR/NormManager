using NormManager.Commands;
using NormManager.Models;
using NormManager.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NormManager.ViewModels
{
    public class AddParamsViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IStructureService _structureService;

        private ICommand _close;
        private ICommand _addNewParam;
        private ICommand _removeParam;
        private ICommand _addNewParameterOption;
        private ICommand _removeParameterOption;

        private string _paramsName = string.Empty;
        private string _newParameterOption = string.Empty;
        private string _selectedType = string.Empty;
        private string _selectedParam = string.Empty;
        private string _lowerbound = string.Empty;
        private string _upperbound = string.Empty;
        private string _unit = string.Empty;
        private bool _isReal = true;
        private bool _isEnum;

        public AddParamsViewModel(
            IWindowService windowService,
            IStructureService structureService)
        {
            _windowService = windowService;
            _structureService = structureService;

            var paramList = _structureService.Structure.Params.ItemOfParams;
            if (paramList.Count > 0) 
            {
                for (int i = 0; i < paramList.Count; i++)
                {
                    Params.Add(paramList[i].Name.Text);
                }
            }
        }

        /// <summary>
        /// Название параметра
        /// </summary>
        public string ParamsName
        {
            get => _paramsName;
            set
            {
                _paramsName = value;
                OnPropertyChanged(nameof(ParamsName));
            }
        }

        /// <summary>
        /// Выбранный параметр
        /// </summary>
        public string SelectedParam
        {
            get => _selectedParam;
            set
            {
                _selectedParam = value;
                OnPropertyChanged(nameof(SelectedParam));
            }
        }

        /// <summary>
        /// Выбранный тип параметра
        /// </summary>
        public string SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }

        /// <summary>
        /// Верхняя граница
        /// </summary>
        public string Upperbound
        {
            get => _upperbound;
            set
            {
                _upperbound = value;
                OnPropertyChanged(nameof(Upperbound));
            }
        }

        /// <summary>
        /// Нижняя граница
        /// </summary>
        public string Lowerbound
        {
            get => _lowerbound;
            set
            {
                _lowerbound = value;
                OnPropertyChanged(nameof(Lowerbound));
            }
        }

        /// <summary>
        /// Новый вариант параметра
        /// </summary>
        public string NewParameterOption
        {
            get => _newParameterOption;
            set
            {
                _newParameterOption = value;
                OnPropertyChanged(nameof(NewParameterOption));
            }
        }

        /// <summary>
        /// Тип целочисленный параметр
        /// </summary>
        public bool IsReal
        {
            get => _isReal;
            set
            {
                _isReal = value;
                OnPropertyChanged(nameof(IsReal));
            }
        }

        /// <summary>
        /// Тип параметр с перечислением
        /// </summary>
        public bool IsEnum
        {
            get => _isEnum;
            set
            {
                _isEnum = value;
                OnPropertyChanged(nameof(IsEnum));
            }
        }

        /// <summary>
        /// Список параметров
        /// </summary>
        public ObservableCollection<string> Params { get; set; } = new();

        /// <summary>
        /// Список вариантов параметра
        /// </summary>
        public ObservableCollection<string> TypesOfParams { get; set; } = new();

        /// <summary>
        /// Добавить новый параметр
        /// </summary>
        public ICommand AddNewParam => _addNewParam ??= new RelayCommand((obj) =>
        {
            Params.Add(ParamsName);

            if (IsReal)
            {
                _structureService.AddRealParam(ParamsName, Lowerbound, Upperbound, Unit);
            }

            if (IsEnum)
            {
                _structureService.AddEnumParam(ParamsName, TypesOfParams);
            }

            ClearFields();
        }, (obj) => (ParamsName != string.Empty && (TypesOfParams.Count > 0 && IsEnum) ||
                    (Unit != string.Empty && Lowerbound != string.Empty && Upperbound != string.Empty && IsReal)));

        /// <summary>
        /// Добавить новый вариант параметра
        /// </summary>
        public ICommand AddNewParameterOption => _addNewParameterOption ??= new RelayCommand((obj) =>
        {
            TypesOfParams.Add(NewParameterOption);
            NewParameterOption = string.Empty;
        }, (obj) => NewParameterOption != string.Empty);

        /// <summary>
        /// Удалить параметр
        /// </summary>
        public ICommand RemoveParam => _removeParam ??= new RelayCommand((obj) =>
        {
            Params.Remove(SelectedParam);
        }, (obj) => SelectedParam != string.Empty);

        /// <summary>
        /// Удалить вариант параметра
        /// </summary>
        public ICommand RemoveParameterOption => _removeParameterOption ??= new RelayCommand((obj) =>
        {
            TypesOfParams.Remove(SelectedType);
        }, (obj) => SelectedType != string.Empty);

        /// <summary>
        /// Команда закрытия окна
        /// </summary>
        public ICommand Close => _close ??= new RelayCommand(obj =>
        {
            _windowService.Close<AddParamsViewModel>();
        });

        private void ClearFields()
        {
            ParamsName = string.Empty;
            Unit = string.Empty;
            Lowerbound = string.Empty;
            Upperbound = string.Empty;
            SelectedType = string.Empty;
            TypesOfParams.Clear();
        }
    }
}
