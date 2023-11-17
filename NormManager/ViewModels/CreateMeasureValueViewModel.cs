using NormManager.Commands;
using NormManager.Converters;
using NormManager.Models;
using NormManager.Services;
using NormManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace NormManager.ViewModels
{
    public class CreateMeasureValueViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IStructureService _structureService;
        private readonly ITreeService _treeService;

        private ICommand _close;
        private ICommand _createMeasureValue;
        private ICommand _editTableColumns;

        private string _valueName = string.Empty;
        private string _id = string.Concat("{", $"{Guid.NewGuid()}", "}");
        private MeasuredQuantityType _selectTypeValue = MeasuredQuantityType.Formula;
        private List<MeasuredQuantityType> _typeOfValue = new()
        {
            MeasuredQuantityType.Formula,
            MeasuredQuantityType.Fixedgradations,
            //ParameterType.Variablegradations,
            MeasuredQuantityType.Normalinterval,
            //ParameterType.Fixedgradationsnorm,
            MeasuredQuantityType.String
        };

        public CreateMeasureValueViewModel(
            IWindowService windowService, 
            IStructureService structureService,
            ITreeService treeService)
        {
            _windowService = windowService;
            _structureService = structureService;
            _treeService = treeService;
        }

        public bool IsEdit { get; set; } = false;

        /// <summary>
        /// ID измеряемой величины
        /// </summary>
        public string ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// Название измеряемой величины
        /// </summary>
        public string ValueName 
        { 
            get => _valueName;
            set => SetProperty(ref _valueName, value);
        }

        /// <summary>
        /// Выбранный тип величины
        /// </summary>
        public MeasuredQuantityType SelectTypeValue
        {
            get => _selectTypeValue;
            set => SetProperty(ref _selectTypeValue, value);
        }

        /// <summary>
        /// Список типов измеряемой величины
        /// </summary>
        public List<MeasuredQuantityType> TypeOfValue { get => _typeOfValue; }

        /// <summary>
        /// Создать измеряемую величину
        /// </summary>
        public ICommand CreateMeasureValue => _createMeasureValue ??= new RelayCommand(obj =>
        {
            var selectedFolder = _treeService.SelectedFolder;
            var selectedMeasurableQuantity = _treeService.SelectedNameMeasurableQuantity;
            var measurableQuantity = _treeService.MainTreeElementsList.First(x => x.FolderName == selectedFolder).SubmainElementsList.First(x => x.MeasurableQuantityName == selectedMeasurableQuantity);
            measurableQuantity.ValueType = SelectTypeValue;

            _structureService.AddMeasureValue(ID, _treeService.SelectedFolder, ValueName, EnumHelper.GetEnumDescription(SelectTypeValue),
                measurableQuantity.CountInputParameters, measurableQuantity.ParametersIncludeInValue);
            
            CloseWindow();
        }, (obj) => ValueName != string.Empty);

        /// <summary>
        /// Открыть окно редактирования столбцов
        /// </summary>
        public ICommand EditTableColumns => _editTableColumns ??= new RelayCommand(obj =>
        {
            if (!IsEdit)
            {
                _treeService.SelectedNameMeasurableQuantity = ValueName;
                _treeService.SelectedTypeMeasuredQuantity = SelectTypeValue;
                _treeService.SelectedIdMeasurableQuantity = ID;
                _windowService.Show<TableColumnsViewModel>();
            }
            else
            {
                if (_treeService.SelectedNameMeasurableQuantity != ValueName)
                {
                    _treeService.RenameMeasureValue(_treeService.SelectedFolder, _treeService.SelectedNameMeasurableQuantity, ValueName);
                }

                _treeService.SelectedNameMeasurableQuantity = ValueName;
                _treeService.SelectedTypeMeasuredQuantity = SelectTypeValue;
                _treeService.SelectedIdMeasurableQuantity = ID;
                _windowService.Show<EditTableColumnsViewModel>();
            }

            IsEdit = true;
        }, (obj) => ValueName != string.Empty);

        /// <summary>
        /// Команда закрытия окна
        /// </summary>
        public ICommand Close => _close ??= new RelayCommand(obj =>
        {
            var selectedFolder = _treeService.SelectedFolder;
            var MeasurableQuantity = _treeService.MainTreeElementsList.First(x => x.FolderName == selectedFolder).SubmainElementsList;
            var item = MeasurableQuantity.First(x => x.MeasurableQuantityName == ValueName);
            MeasurableQuantity.Remove(item);

            CloseWindow();
        });

        private void CloseWindow()
        {
            _treeService.ClearTreeProps();
            _windowService.Close<CreateMeasureValueViewModel>();
        }
    }
}
