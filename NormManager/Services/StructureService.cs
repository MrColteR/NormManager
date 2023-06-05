using NormManager.Models;
using NormManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NormManager.Services
{
    public class StructureService : IStructureService
    {
        private readonly ITreeService _treeService;
        private Main _structure = new();
        private string _fileName;

        public StructureService(ITreeService treeService)
        {
            _treeService = treeService;
        }

        /// <summary>
        /// Структура XAML
        /// </summary>
        public Main Structure 
        {
            get => _structure;
            set => _structure = value;
        }

        /// <summary>
        /// Название файла
        /// </summary>
        public string FileName
        {
            get => _fileName;
        }

        /// <summary>
        /// Установить название файла
        /// </summary>
        /// <param name="fileName">название файла</param>
        public void SetFileName(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Добавление папки
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void AddNewFolder(string folderName)
        {
            _structure.Groups.ItemOfGroup.Add(new ItemOfGroup()
            {
                Id = Guid.NewGuid().ToString(),
                Name = new Name()
                {
                    Localizable = true,
                    Text = folderName
                }
            });

            _treeService.AddNewFolder(folderName);
        }

        /// <summary>
        /// Удаление папки
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void RemoveFolder(string folderName)
        {
            var folderStructure = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName);
            _structure.Groups.ItemOfGroup.Remove(folderStructure);

            _treeService.RemoveFolder(folderName);
        }

        /// <summary>
        /// Удаление величины
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        public void RemoveMeasurableQuantity(string folderName, string measurableQuantityName)
        {
            var folderStructure = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName);
            var itemStructure = folderStructure.Subgroups.ItemOfSubgroup.First(x => x.Quantities.ItemOfType.Name.Text == measurableQuantityName);
            folderStructure.Subgroups.ItemOfSubgroup.Remove(itemStructure);

            _treeService.RemoveMeasurableQuantity(folderName, measurableQuantityName);
        }

        /// <summary>
        /// Перемещение папки вверх
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void MoveUpFolder(string folderName)
        {
            var itemStructure = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName);
            int index = _structure.Groups.ItemOfGroup.IndexOf(itemStructure);
            if (index > 0)
            {
                Swap(_structure.Groups.ItemOfGroup, index, index - 1);
                Swap(_treeService.MainTreeElementsList, index, index - 1);
            }
        }

        /// <summary>
        /// Перемещение папки вниз
        /// </summary>
        /// <param name="folderName">Название папка</param>
        public void MoveDownFolder(string folderName)
        {
            var itemStructure = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName);
            int index = _structure.Groups.ItemOfGroup.IndexOf(itemStructure);
            if (index < _structure.Groups.ItemOfGroup.Count)
            {
                Swap(_structure.Groups.ItemOfGroup, index, index + 1);
                Swap(_treeService.MainTreeElementsList, index, index + 1);
            }
        }

        /// <summary>
        /// Перемещение измеряемой величины вверх
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название измеряемой величины</param>
        public void MoveUpMeasurableQuantity(string folderName, string measurableQuantityName)
        {
            var itemList = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName).Subgroups.ItemOfSubgroup;
            var itemStructure = itemList.First(x => x.Quantities.ItemOfType.Name.Text == measurableQuantityName);
            int index = itemList.IndexOf(itemStructure);
            if (index > 0)
            {
                Swap(itemList, index, index - 1);
                Swap(_treeService.MainTreeElementsList.First(x => x.FolderName == folderName).SubmainElementsList, index, index - 1);
            }
        }

        /// <summary>
        /// Перемещение измеряемой величины вниз
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название измеряемой величины</param>
        public void MoveDownMeasurableQuantity(string folderName, string measurableQuantityName)
        {
            var itemList = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName).Subgroups.ItemOfSubgroup;
            var itemStructure = itemList.First(x => x.Quantities.ItemOfType.Name.Text == measurableQuantityName);
            int index = itemList.IndexOf(itemStructure);
            if (index < itemList.Count)
            {
                Swap(itemList, index, index + 1);
                Swap(_treeService.MainTreeElementsList.First(x => x.FolderName == folderName).SubmainElementsList, index, index + 1);
            }
        }

        /// <summary>
        /// Добавление структуры из файла
        /// </summary>
        /// <param name="mainStructure"></param>
        public void SetReadyStructure(Main mainStructure)
        {
            _structure = mainStructure;
           _treeService.CreateStructureFromXML(_structure);
        }

        /// <summary>
        /// Добавление дефолтных параметров
        /// </summary>
        public void AddDefaultParams()
        {
            _structure.Params.ItemOfParams.Add(new ItemOfParams()
            {
                Uid = "{83EA104F-DA02-40B5-9E92-C3D1DC60112D}",
                Name = new Name()
                {
                    Localizable = true,
                    Text = "Возраст"
                },
                Fname = "A",
                Lowerbound = "0",
                Upperbound = "43800",
                Type = new Models.Type()
                {
                    Kind = "real",
                    Unitname = new Unitname()
                    {
                        Localizable = true,
                        Text = "дн"
                    }
                }
            });
            _structure.Params.ItemOfParams.Add(new ItemOfParams()
            {
                Uid = "HeightId",
                Name = new Name()
                {
                    Localizable = true,
                    Text = "Рост"
                },
                Fname = "H",
                Lowerbound = "0",
                Upperbound = "250",
                Type = new Models.Type()
                {
                    Kind = "real",
                    Unitname = new Unitname()
                    {
                        Localizable = true,
                        Text = "см"
                    }
                }
            });
            _structure.Params.ItemOfParams.Add(new ItemOfParams()
            {
                Uid = "WeightId",
                Name = new Name()
                {
                    Localizable = true,
                    Text = "Вес"
                },
                Fname = "W",
                Lowerbound = "0",
                Upperbound = "250",
                Type = new Models.Type()
                {
                    Kind = "real",
                    Unitname = new Unitname()
                    {
                        Localizable = true,
                        Text = "кг"
                    }
                }
            });
            _structure.Params.ItemOfParams.Add(new ItemOfParams()
            {
                Uid = "SexId",
                Name = new Name()
                {
                    Localizable = true,
                    Text = "Пол"
                },
                Type = new Models.Type()
                {
                    Kind = "enumeration",
                    Enum = new Models.Enum()
                    {
                        String = new ObservableCollection<Models.String>()
                        {
                            new Models.String()
                            {
                                Localizable= true,
                                Text = "М"
                            },
                            new Models.String()
                            {
                                Localizable= true,
                                Text = "Ж"
                            }
                        },
                        Uid = new ObservableCollection<string>()
                        {
                            "MaleId",
                            "FemaleId"
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Добавление измеряемой величины
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        /// <param name="measurableQuantityType">Тип величины</param>
        /// <param name="countParamsFromInputValue">Количество входных параметров</param>
        /// <param name="parametersIncludeInValue">Список параметров которые содержит величина</param>
        public void AddMeasureValue(string folderName, string measurableQuantityName, string measurableQuantityType, 
            int countParamsFromInputValue, ObservableCollection<ItemOfParams> parametersIncludeInValue)
        {
            TreeLevel treeLevel = new TreeLevel();
            ObservableCollection<ItemOfParams> tempList = new ObservableCollection<ItemOfParams>(parametersIncludeInValue.ToList());
            treeLevel = FillItemOfChildrenRecursively(treeLevel, tempList);

            var items = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName).Subgroups.ItemOfSubgroup;
            items.Add(new ItemOfSubgroup()
            {
                Quantities = new Quantities()
                {
                    ItemOfType = new ItemOfType()
                    {
                        Name = new Name()
                        {
                            Localizable = true,
                            Text = measurableQuantityName
                        },
                        ItemType = measurableQuantityType,
                        TreeLevel = treeLevel
                    }
                },
                Pattern = new Pattern()
                {
                    Ccols = countParamsFromInputValue,
                    Inherits = 1
                }
            }); ;

            for (int i = 0; i < parametersIncludeInValue.Count; i++)
            {
                var id =  _structure.Params.ItemOfParams.First(x => x == parametersIncludeInValue[i]).Uid;
                items.First(x => x.Quantities.ItemOfType.Name.Text == measurableQuantityName).Pattern.Items.String.Add(id.ToString());
            }
        }

        /// <summary>
        /// Редактирование измеряемой величины
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        /// <param name="measurableQuantityType">Тип величины</param>
        /// <param name="countParamsFromInputValue">Количество входных параметров</param>
        /// <param name="parametersIncludeInValue">Список параметров которые содержит величина</param>
        public void EditMeasureValue(string folderName, string measurableQuantityName, string measurableQuantityType,
            int countParamsFromInputValue, ObservableCollection<ItemOfParams> parametersIncludeInValue)
        {
            var groups = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName).Subgroups.ItemOfSubgroup;
            var subgroup = groups.First(x => x.Quantities.ItemOfType.Name.Text == measurableQuantityName);
            var index = groups.IndexOf(subgroup);
            groups[index] = new ItemOfSubgroup()
            {
                Quantities = new Quantities()
                {
                    ItemOfType = new ItemOfType()
                    {
                        Name = new Name()
                        {
                            Localizable = true,
                            Text = measurableQuantityName
                        },
                        ItemType = measurableQuantityType
                    }
                },
                Pattern = new Pattern()
                {
                    Ccols = countParamsFromInputValue,
                    Inherits = 1
                }
            };

            for (int i = 0; i < parametersIncludeInValue.Count; i++)
            {
                var id = _structure.Params.ItemOfParams.First(x => x == parametersIncludeInValue[i]).Uid;
                _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName).Subgroups.ItemOfSubgroup.
                                              First(x => x.Quantities.ItemOfType.Name.Text == measurableQuantityName).Pattern.Items.String.Add(id.ToString());
            }
        }

        /// <summary>
        /// Изменить название величины
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        /// <param name="newMeasurableQuantityName">Новое название величины</param>
        public void RenameMeasureValue(string folderName, string measurableQuantityName, string newMeasurableQuantityName)
        {
            var items = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName).Subgroups.ItemOfSubgroup;
            var measurableQuantity = items.First(x => x.Quantities.ItemOfType.Name.Text == measurableQuantityName);
            measurableQuantity.Quantities.ItemOfType.Name.Text = newMeasurableQuantityName;

            _treeService.RenameMeasureValue(folderName, measurableQuantityName, newMeasurableQuantityName);
        }

        /// <summary>
        /// Добавление целочисленного параметра
        /// </summary>
        /// <param name="paramName">Название параметра</param>
        /// <param name="lowerbound">Нижняя граница</param>
        /// <param name="upperbound">Вверхняя граница</param>
        /// <param name="unit">Единиица измерения</param>
        public void AddRealParam(string paramName, string lowerbound, string upperbound, string unit)
        {
            string paramId = Guid.NewGuid().ToString();

            _structure.Params.ItemOfParams.Add(new ItemOfParams()
            {
                Uid = paramId,
                Name = new Name()
                {
                    Localizable = true,
                    Text = paramName
                },
                Fname = "A",
                Lowerbound = lowerbound,
                Upperbound = upperbound,
                Type = new Models.Type()
                {
                    Kind = "real",
                    Unitname = new Unitname()
                    {
                        Localizable = true,
                        Text = unit
                    }
                }
            });
        }

        /// <summary>
        /// Добавление параметра c перечислением
        /// </summary>
        /// <param name="paramName">Название параметра</param>
        /// <param name="parameterOptions">Список вариантов параметра</param>
        public void AddEnumParam(string paramName, ObservableCollection<string> parameterOptions)
        {
            string paramId = Guid.NewGuid().ToString();

            _structure.Params.ItemOfParams.Add(new ItemOfParams()
            {
                Uid = paramId,
                Name = new Name()
                {
                    Localizable = true,
                    Text = paramName
                },
                Type = new Models.Type()
                {
                    Kind = "enumeration"
                }
            });

            for (int i = 0; i < parameterOptions.Count; i++)
            {
                var parametrOptionId = Guid.NewGuid().ToString();
                var param = _structure.Params.ItemOfParams.First(x => x.Uid == paramId).Type.Enum;
                param.Uid.Add(parametrOptionId);
                param.String.Add(new Models.String()
                {
                    Localizable = true,
                    Text = parameterOptions[i]
                });
            }
        }

        /// <summary>
        /// Перемещение элементов внитри списка
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Список</param>
        /// <param name="index1">Индекс первого</param>
        /// <param name="index2">Индекс второго</param>
        private static void Swap<T>(ObservableCollection<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        /// <summary>
        /// Очитка структуры
        /// </summary>
        public void ClearStructure()
        {
            _structure = new();
        }

        private TreeLevel FillItemOfChildrenRecursively(TreeLevel treeLevel, ObservableCollection<ItemOfParams> itemOfParamsList)
        {
            if (itemOfParamsList.Count == 0)
            {
                return null;
            }

            ItemOfParams currentItemOfParams = itemOfParamsList[0];
            itemOfParamsList.RemoveAt(0);

            if (currentItemOfParams.Type.Kind == "real")
            {
                treeLevel.Children = new Children()
                {
                    ItemOfChildren = new ItemOfChildren()
                    {
                        Lower = $"{currentItemOfParams.Lowerbound}",
                        Upper = $"{currentItemOfParams.Upperbound}",
                        TreeLevel = new TreeLevel()
                    }
                };
                treeLevel.Cpid = $"{currentItemOfParams.Uid}";
            }
            else
            {
                treeLevel.Children = new Children()
                {
                    ItemOfChildren = new ItemOfChildren()
                    {
                        EnumIndex = "-1",
                        EnumId = "{8D994429-9DF2-4A72-AB46-E49FE3A7437C}",
                        Upperincl = 0,
                        Lowerincl = 0,
                        TreeLevel = new TreeLevel()
                    }
                };
                treeLevel.Cpid = $"{currentItemOfParams.Uid}";
            }

            treeLevel.Children.ItemOfChildren.TreeLevel = FillItemOfChildrenRecursively(treeLevel.Children.ItemOfChildren.TreeLevel, itemOfParamsList);
            return treeLevel;
        }
    }
}
