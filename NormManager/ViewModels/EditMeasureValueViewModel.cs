using NormManager.Commands;
using NormManager.Converters;
using NormManager.Models;
using NormManager.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace NormManager.ViewModels
{
    public class EditMeasureValueViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IStructureService _structureService;
        private readonly ITreeService _treeService;

        private ICommand _close;
        private ICommand _createMeasureValue;
        private ICommand _editTableColumns;

        private string _valueName = string.Empty;
        private string _id /*= string.Concat("{", $"{Guid.NewGuid()}", "}")*/;
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

        public EditMeasureValueViewModel(
            IWindowService windowService,
            IStructureService structureService,
            ITreeService treeService)
        {
            _windowService = windowService;
            _structureService = structureService;
            _treeService = treeService;

            ValueName = _treeService.SelectedNameMeasurableQuantity;
            SelectTypeValue = _treeService.SelectedTypeMeasuredQuantity;
            ID = _treeService.SelectedIdMeasurableQuantity;
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
        /// ID измеряемой величины
        /// </summary>
        public string ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
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

            if (_treeService.SelectedNameMeasurableQuantity != ValueName)
            {
                _structureService.RenameMeasureValue(_treeService.SelectedFolder, _treeService.SelectedNameMeasurableQuantity, ValueName);
            }

            _treeService.SelectedNameMeasurableQuantity = ValueName;
            _treeService.SelectedTypeMeasuredQuantity = SelectTypeValue;
            _treeService.EditMeasurableQuantity();

            _structureService.EditMeasureValue(ID, _treeService.SelectedFolder, ValueName, EnumHelper.GetEnumDescription(SelectTypeValue),
                measurableQuantity.CountInputParameters, measurableQuantity.ParametersIncludeInValue);


            CloseWindow();
        }, (obj) => ValueName != string.Empty);

        /// <summary>
        /// Открыть окно редактирования столбцов
        /// </summary>
        public ICommand EditTableColumns => _editTableColumns ??= new RelayCommand(obj =>
        {
            if (_treeService.SelectedNameMeasurableQuantity != ValueName)
            {
                _structureService.RenameMeasureValue(_treeService.SelectedFolder, _treeService.SelectedNameMeasurableQuantity, ValueName);
            }

            _treeService.SelectedNameMeasurableQuantity = ValueName;
            _treeService.SelectedTypeMeasuredQuantity = SelectTypeValue;
            _windowService.Show<EditTableColumnsViewModel>();
        }, (obj) => ValueName != string.Empty);

        /// <summary>
        /// Команда закрытия окна
        /// </summary>
        public ICommand Close => _close ??= new RelayCommand(obj =>
        {
            CloseWindow();
        });

        private void CloseWindow()
        {
            _treeService.ClearTreeProps();
            _windowService.Close<EditMeasureValueViewModel>();
        }
    }
}
