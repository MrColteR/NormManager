using NormManager.Commands;
using NormManager.Services;
using NormManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NormManager.ViewModels
{
    public class AddParamsViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IStructureService _structureService;
        private readonly ITreeService _treeService;

        private ICommand _close;
        private ICommand _addNewParam;
        private ICommand _removeParam;
        private ICommand _addNewParameterOption;
        private ICommand _removeParameterOption;
        private ICommand _editingParam;
        private ICommand _cancelAdding;

        private string _paramsName = string.Empty;
        private string _newParameterOption = string.Empty;
        private string _selectedType = string.Empty;
        private string _selectedParam = string.Empty;
        private string _lowerbound = string.Empty;
        private string _upperbound = string.Empty;
        private string _fname = string.Empty;
        private string _unit = string.Empty;
        private string _paramId = string.Concat("{", $"{Guid.NewGuid()}", "}");
        private bool _isReal = true;
        private bool _isEnum;
        private bool _isEdit;
        private bool _isAddition = true;

        public AddParamsViewModel(
            IWindowService windowService,
            IStructureService structureService,
            ITreeService treeService)
        {
            _windowService = windowService;
            _structureService = structureService;
            _treeService = treeService;

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
            set => SetProperty(ref _paramsName, value);
        }

        /// <summary>
        /// Выбранный тип параметра
        /// </summary>
        public string SelectedType
        {
            get => _selectedType;
            set => SetProperty(ref _selectedType, value);
        }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        /// <summary>
        /// Верхняя граница
        /// </summary>
        public string Upperbound
        {
            get => _upperbound;
            set => SetProperty(ref _upperbound, value);
        }

        /// <summary>
        /// Нижняя граница
        /// </summary>
        public string Lowerbound
        {
            get => _lowerbound;
            set => SetProperty(ref _lowerbound, value);
        }

        /// <summary>
        /// Новый вариант параметра
        /// </summary>
        public string NewParameterOption
        {
            get => _newParameterOption;
            set => SetProperty(ref _newParameterOption, value);
        }

        /// <summary>
        /// Обозначение в формуле
        /// </summary>
        public string Fname
        {
            get => _fname;
            set => SetProperty(ref _fname, value);
        }

        /// <summary>
        /// ID параметра
        /// </summary>
        public string ParamID
        {
            get => _paramId;
            set => SetProperty(ref _paramId, value);
        }

        /// <summary>
        /// Тип целочисленный параметр
        /// </summary>
        public bool IsReal
        {
            get => _isReal;
            set => SetProperty(ref _isReal, value);
        }

        /// <summary>
        /// Тип параметр с перечислением
        /// </summary>
        public bool IsEnum
        {
            get => _isEnum;
            set => SetProperty(ref _isEnum, value);
        }

        /// <summary>
        /// Редактирование существующего параметра
        /// </summary>
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        /// <summary>
        /// Добавление существующего элемента
        /// </summary>
        public bool IsAddition
        {
            get => _isAddition;
            set => SetProperty(ref _isAddition, value);
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
                StartEditing();
                OnPropertyChanged(nameof(SelectedParam));
            }
        }

        /// <summary>
        /// Список параметров
        /// </summary>
        public ObservableCollection<string> Params { get; set; } = new ObservableCollection<string>();

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
                _structureService.AddRealParam(ParamID, ParamsName, Lowerbound, Upperbound, Unit, Fname);
            }

            if (IsEnum)
            {
                _structureService.AddEnumParam(ParamID, ParamsName, TypesOfParams);
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
            IsEdit = !IsEdit;
            IsAddition = !IsAddition;
            ClearFields();
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

        /// <summary>
        /// Команда редактирования параметра
        /// </summary>
        public ICommand EditingParam => _editingParam ??= new RelayCommand(obj =>
        {
            if (IsReal)
            {
                _structureService.EditRealParam(ParamID, ParamsName, Lowerbound, Upperbound, Unit, Fname);
                CancelEditing();
            }

            if (IsEnum)
            {
                _structureService.EditEnumParam(ParamID, ParamsName, TypesOfParams);
                CancelEditing();
            }
        });

        /// <summary>
        /// Команда отмены редактирования
        /// </summary>
        public ICommand CancelAdding => _cancelAdding ??= new RelayCommand(obj =>
        {
            CancelEditing();
        });

        private void ClearFields()
        {
            ParamsName = string.Empty;
            Unit = string.Empty;
            Lowerbound = string.Empty;
            Upperbound = string.Empty;
            SelectedType = string.Empty;
            Fname = string.Empty;
            ParamID = string.Concat("{", $"{Guid.NewGuid()}", "}");
            TypesOfParams.Clear();
        }

        private void StartEditing()
        {
            IsAddition = false;
            IsEdit = true;
            _treeService.SelectedParamName = SelectedParam;

            var param = _structureService.GetParam(SelectedParam);
            ParamsName = param.Name.Text;
            ParamID = param.Uid;
            if (param.Type.Kind == "real") 
            {
                IsReal = true;
                IsEnum = false;
                Unit = param.Type.Unitname.Text;
                Lowerbound = param.Lowerbound;
                Upperbound = param.Upperbound;
                Fname = param.Fname;
            }
            else
            {
                IsReal = false;
                IsEnum = true;
                TypesOfParams.Clear();
                for (int i = 0; i < param.Type.Enum.String.Count; i++)
                {
                    TypesOfParams.Add(param.Type.Enum.String[i].Text);
                }
            }
        }

        private void CancelEditing()
        {
            IsEdit = !IsEdit;
            IsAddition = !IsAddition;
            ClearFields();
        }
    }
}
