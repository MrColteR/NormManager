using NormManager.Converters;
using NormManager.Models;
using NormManager.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace NormManager.Services
{
    public class TreeService : ITreeService
    {
        /// <summary>
        /// Список папок
        /// </summary>
        public ObservableCollection<MainTreeElement> MainTreeElementsList { get; set; } = new ObservableCollection<MainTreeElement>();

        /// <summary>
        /// Выбранная папка
        /// </summary>
        public string SelectedFolder { get; set; }

        /// <summary>
        /// Выбранный элемент
        /// </summary>
        public string SelectedMeasurableQuantity { get; set; }

        /// <summary>
        /// Тип величны
        /// </summary>
        public MeasuredQuantityType SelectedMeasuredQuantityType { get; set; }

        /// <summary>
        /// Создание структуры по существующему XML документу
        /// </summary>
        /// <param name="structure">Структура XML</param>
        public void CreateStructureFromXML(Main structure)
        {
            for (int i = 0; i < structure.Groups.ItemOfGroup.Count; i++)
            {
                MainTreeElementsList.Add(new MainTreeElement()
                {
                    FolderName = structure.Groups.ItemOfGroup[i].Name.Text
                });

                for (int j = 0; j < structure.Groups.ItemOfGroup[i].Subgroups.ItemOfSubgroup.Count; j++)
                {
                    ObservableCollection<ItemOfParams> param = new();
                    for (int o = 0; o < structure.Groups.ItemOfGroup[i].Subgroups.ItemOfSubgroup[j].Pattern.Items.String.Count; o++)
                    {
                        string id = structure.Groups.ItemOfGroup[i].Subgroups.ItemOfSubgroup[j].Pattern.Items.String[o];
                        param.Add(structure.Params.ItemOfParams.First(x => x.Uid == id));
                    }

                    MainTreeElementsList[i].SubmainElementsList.Add(new SubmainTreeElement()
                    {
                        FolderName = MainTreeElementsList[i].FolderName,
                        MeasurableQuantityName = structure.Groups.ItemOfGroup[i].Subgroups.ItemOfSubgroup[j].Quantities.ItemOfType.Name.Text,
                        CountInputParameters = structure.Groups.ItemOfGroup[i].Subgroups.ItemOfSubgroup[j].Pattern.Ccols,
                        ParametersIncludeInValue = param,
                        ValueType = EnumHelper.GetEnumFromDescription<MeasuredQuantityType>(structure.Groups.ItemOfGroup[i].Subgroups.ItemOfSubgroup[j].Quantities.ItemOfType.ItemType)
                    });
                }
            }
        }

        /// <summary>
        /// Добавление папки
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void AddNewFolder(string folderName)
        {
            MainTreeElementsList.Add(new MainTreeElement()
            {
                FolderName = folderName,
            });
        }

        /// <summary>
        /// Удаление папки
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void RemoveFolder(string folderName)
        {
            var folderTree = MainTreeElementsList.First(x => x.FolderName == folderName);
            MainTreeElementsList.Remove(folderTree);
        }

        /// <summary>
        /// Добавление величины
        /// </summary>
        /// <param name="countInputUsedParameters">Кол-во параметров для выпадающего списка</param>
        /// <param name="usedParameters">Список использоваемых параметров</param>
        public void AddMeasurableQuantity(int countInputUsedParameters, ObservableCollection<ItemOfParams> usedParameters)
        {
            var measurableQuantity = MainTreeElementsList.First(x => x.FolderName == SelectedFolder).SubmainElementsList;
            var submainTreeElement = new SubmainTreeElement()
            {
                CountInputParameters = countInputUsedParameters,
                FolderName = SelectedFolder,
                MeasurableQuantityName = SelectedMeasurableQuantity,
                ParametersIncludeInValue = usedParameters,
                ValueType = SelectedMeasuredQuantityType
            };

            measurableQuantity.Add(submainTreeElement);
        }

        /// <summary>
        /// Редактирование величины
        /// </summary>
        public void EditMeasurableQuantity()
        {
            var measurableQuantityList = MainTreeElementsList.First(x => x.FolderName == SelectedFolder).SubmainElementsList;
            var measurableQuantity = measurableQuantityList.First(x => x.MeasurableQuantityName == SelectedMeasurableQuantity);
            var index = measurableQuantityList.IndexOf(measurableQuantity);
            var submainTreeElement = new SubmainTreeElement()
            {
                CountInputParameters = measurableQuantity.CountInputParameters,
                FolderName = SelectedFolder,
                MeasurableQuantityName = SelectedMeasurableQuantity,
                ParametersIncludeInValue = measurableQuantity.ParametersIncludeInValue,
                ValueType = SelectedMeasuredQuantityType
            };
            
            measurableQuantityList[index] = submainTreeElement;
        }

        /// <summary>
        /// Редактирование величины
        /// </summary>
        /// <param name="countInputUsedParameters">Кол-во параметров для выпадающего списка</param>
        /// <param name="usedParameters">Список использоваемых параметров</param>
        public void EditMeasurableQuantity(int countInputUsedParameters, ObservableCollection<ItemOfParams> usedParameters)
        {
            var measurableQuantity = MainTreeElementsList.First(x => x.FolderName == SelectedFolder).SubmainElementsList;
            var submainTreeElement = new SubmainTreeElement()
            {
                CountInputParameters = countInputUsedParameters,
                FolderName = SelectedFolder,
                MeasurableQuantityName = SelectedMeasurableQuantity,
                ParametersIncludeInValue = usedParameters,
                ValueType = SelectedMeasuredQuantityType
            };

            var item = measurableQuantity.First(x => x.MeasurableQuantityName == SelectedMeasurableQuantity);
            var index = measurableQuantity.IndexOf(item);
            measurableQuantity[index] = submainTreeElement;
        }

        /// <summary>
        /// Удаление величины
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        public void RemoveMeasurableQuantity(string folderName, string measurableQuantityName)
        {
            var folderTree = MainTreeElementsList.First(x => x.FolderName == folderName);
            var itemTree = folderTree.SubmainElementsList.First(x => x.MeasurableQuantityName == measurableQuantityName);
            folderTree.SubmainElementsList.Remove(itemTree);
        }

        /// <summary>
        /// Изменить название величины
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        /// <param name="newMeasurableQuantityName">Новое название величины</param>
        public void RenameMeasureValue(string folderName, string measurableQuantityName, string newMeasurableQuantityName)
        {
            var mainItem = MainTreeElementsList.First(x => x.FolderName == folderName);
            var measurableQuantityItem = mainItem.SubmainElementsList.First(x => x.MeasurableQuantityName == measurableQuantityName);
            measurableQuantityItem.MeasurableQuantityName = newMeasurableQuantityName;
        }

        /// <summary>
        /// Очистка дерева
        /// </summary>
        public void ClearTree() => MainTreeElementsList.Clear();

        /// <summary>
        /// Очистка свойств структуры
        /// </summary>
        public void ClearTreeProps()
        {
            SelectedFolder = string.Empty;
            SelectedMeasurableQuantity= string.Empty;
        }
    }
}
