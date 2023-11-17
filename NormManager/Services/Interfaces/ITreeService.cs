using NormManager.Models;
using System.Collections.ObjectModel;

namespace NormManager.Services.Interfaces
{
    public interface ITreeService
    {
        /// <summary>
        /// Список папок
        /// </summary>
        public ObservableCollection<MainTreeElement> MainTreeElementsList { get; set; }

        /// <summary>
        /// Выбранная папка
        /// </summary>
        public string SelectedFolder { get; set; }

        /// <summary>
        /// Выбранная величина
        /// </summary>
        public string SelectedNameMeasurableQuantity { get; set; }

        /// <summary>
        /// Выбранный  элемент
        /// </summary>
        public string SelectedIdMeasurableQuantity { get; set; }

        /// <summary>
        /// Выбранный параметр
        /// </summary>
        public string SelectedParamName { get; set; }

        /// <summary>
        /// Тип величины
        /// </summary>
        public MeasuredQuantityType SelectedTypeMeasuredQuantity { get; set; }

        /// <summary>
        /// Создание структуры по существующему XML документу
        /// </summary>
        /// <param name="structure">Структура XML</param>
        public void CreateStructureFromXML(Main structure);

        /// <summary>
        /// Добавление папки
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void AddNewFolder(string folderName);

        /// <summary>
        /// Удаление папки
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void RemoveFolder(string folderName);

        /// <summary>
        /// Добавление величины
        /// </summary>
        /// <param name="id">ID величины</param>
        /// <param name="countInputUsedParameters">Кол-во параметров для выпадающего списка</param>
        /// <param name="usedParameters">Список использоваемых параметров</param>
        public void AddMeasurableQuantity(int countInputUsedParameters, ObservableCollection<ItemOfParams> usedParameters);

        /// <summary>
        /// Редактирование величины
        /// </summary>
        public void EditMeasurableQuantity();

        /// <summary>
        /// Редактирование величины
        /// </summary>
        /// <param name="countInputUsedParameters">Кол-во параметров для выпадающего списка</param>
        /// <param name="usedParameters">Список использоваемых параметров</param>
        public void EditMeasurableQuantity(int countInputUsedParameters, ObservableCollection<ItemOfParams> usedParameters);

        /// <summary>
        /// Удаление величины
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        public void RemoveMeasurableQuantity(string folderName, string measurableQuantityName);

        /// <summary>
        /// Изменить название величины
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        /// <param name="newMeasurableQuantityName">Новое название величины</param>
        public void RenameMeasureValue(string folderName, string measurableQuantityName, string newMeasurableQuantityName);

        /// <summary>
        /// Очистка деерва
        /// </summary>
        public void ClearTree();

        /// <summary>
        /// Очистка свойств структуры
        /// </summary>
        public void ClearTreeProps();
    }
}
