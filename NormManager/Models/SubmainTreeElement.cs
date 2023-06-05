using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NormManager.Models
{
    public class SubmainTreeElement : BaseModel
    {
        private ObservableCollection<ItemOfParams> _parametersIncludeInValue = new();

        /// <summary>
        /// Имя родительской папки
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Название величины
        /// </summary>
        public string MeasurableQuantityName { get; set; }

        /// <summary>
        /// Количество входных параметров
        /// </summary>
        public int CountInputParameters { get; set; } = 0;

        /// <summary>
        /// Тип величины
        /// </summary>
        public MeasuredQuantityType ValueType { get; set; }

        /// <summary>
        /// Список параметров которые содержит величина
        /// </summary>
        public ObservableCollection<ItemOfParams> ParametersIncludeInValue 
        {
            get => _parametersIncludeInValue;
            set
            {
                _parametersIncludeInValue = value;
                OnPropertyChanged(nameof(ParametersIncludeInValue));
            }
        }
    }
}
