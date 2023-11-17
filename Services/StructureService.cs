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

        public Main Structure 
        {
            get => _structure;
            set => _structure = value;
        }

        public string FileName
        {
            get => _fileName;
        }

        public void SetFileName(string fileName) => _fileName = fileName;

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

        public void RemoveFolder(string folderName)
        {
            var folderStructure = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName);
            _structure.Groups.ItemOfGroup.Remove(folderStructure);

            _treeService.RemoveFolder(folderName);
        }

        public void RemoveMeasurableQuantity(string folderName, string measurableQuantityName)
        {
            var folderStructure = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName);
            var itemStructure = folderStructure.Subgroups.ItemOfSubgroup.First(x => x.Quantities.ItemOfType.Name.Text == measurableQuantityName);
            folderStructure.Subgroups.ItemOfSubgroup.Remove(itemStructure);

            _treeService.RemoveMeasurableQuantity(folderName, measurableQuantityName);
        }

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

        public void SetReadyStructure(Main mainStructure)
        {
            _structure = mainStructure;
           _treeService.CreateStructureFromXML(_structure);
        }

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

        public void AddMeasureValue(string idMeasurableQuantity, string folderName,
                                    string measurableQuantityName, string measurableQuantityType, 
                                    int countParamsFromInputValue, ObservableCollection<ItemOfParams> parametersIncludeInValue)
        {
            TreeLevel treeLevel = new TreeLevel();
            List<ItemOfParams> tempList = new List<ItemOfParams>(parametersIncludeInValue.ToList());
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
                        ID = idMeasurableQuantity,
                        ItemType = measurableQuantityType,
                        TreeLevel = treeLevel
                    }
                },
                Pattern = new Pattern()
                {
                    Ccols = countParamsFromInputValue,
                    Inherits = 1
                }
            });

            for (int i = 0; i < parametersIncludeInValue.Count; i++)
            {
                var id =  _structure.Params.ItemOfParams.First(x => x == parametersIncludeInValue[i]).Uid;
                items.First(x => x.Quantities.ItemOfType.Name.Text == measurableQuantityName).Pattern.Items.String.Add(id.ToString());
            }
        }

        public void EditMeasureValue(string idMeasurableQuantity, string folderName,
                                     string measurableQuantityName, string measurableQuantityType,
                                     int countParamsFromInputValue, ObservableCollection<ItemOfParams> parametersIncludeInValue)
        {
            TreeLevel treeLevel = new TreeLevel();
            List<ItemOfParams> tempList = new List<ItemOfParams>(parametersIncludeInValue.ToList());
            treeLevel = FillItemOfChildrenRecursively(treeLevel, tempList);

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
                        ID = idMeasurableQuantity,
                        ItemType = measurableQuantityType,
                        TreeLevel = treeLevel
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

        public void RenameMeasureValue(string folderName, string measurableQuantityName, string newMeasurableQuantityName)
        {
            var items = _structure.Groups.ItemOfGroup.First(x => x.Name.Text == folderName).Subgroups.ItemOfSubgroup;
            var measurableQuantity = items.First(x => x.Quantities.ItemOfType.Name.Text == measurableQuantityName);
            measurableQuantity.Quantities.ItemOfType.Name.Text = newMeasurableQuantityName;

            _treeService.RenameMeasureValue(folderName, measurableQuantityName, newMeasurableQuantityName);
        }

        public void AddRealParam(string paramId, string paramName, string lowerbound, string upperbound, string unit, string fname)
        {
            _structure.Params.ItemOfParams.Add(new ItemOfParams()
            {
                Uid = paramId,
                Name = new Name()
                {
                    Localizable = true,
                    Text = paramName
                },
                Fname = fname,
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

        public void EditRealParam(string paramId, string paramName, string lowerbound, string upperbound, string unit, string fname)
        {
            var param = _structure.Params.ItemOfParams.First(x => x.Name.Text == _treeService.SelectedParamName);
            param.Upperbound = upperbound;
            param.Lowerbound = lowerbound;
            param.Uid = paramId;
            param.Fname = fname;
            param.Type.Unitname.Text = unit;
            param.Name = new Name()
            {
                Localizable = true,
                Text = paramName
            };
        }

        public void AddEnumParam(string paramId, string paramName, ObservableCollection<string> parameterOptions)
        {
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

            var param = _structure.Params.ItemOfParams.First(x => x.Uid == paramId).Type.Enum;
            for (int i = 0; i < parameterOptions.Count; i++)
            {
                var parametrOptionId = Guid.NewGuid().ToString();
                param.Uid.Add(parametrOptionId);
                param.String.Add(new Models.String()
                {
                    Localizable = true,
                    Text = parameterOptions[i]
                });
            }
        }

        public void EditEnumParam(string paramId, string paramName, ObservableCollection<string> parameterOptions)
        {
            var param = _structure.Params.ItemOfParams.First(x => x.Name.Text == _treeService.SelectedParamName);
            param.Uid = paramId;
            param.Name = new Name()
            {
                Localizable = true,
                Text = paramName
            };

            var paramList = _structure.Params.ItemOfParams.First(x => x.Name.Text == _treeService.SelectedParamName).Type.Enum;
            paramList.Uid.Clear();
            paramList.String.Clear();
            for (int i = 0; i < parameterOptions.Count; i++)
            {
                var parametrOptionId = Guid.NewGuid().ToString();
                paramList.Uid.Add(parametrOptionId);
                paramList.String.Add(new Models.String()
                {
                    Localizable = true,
                    Text = parameterOptions[i]
                });
            }
        }

        public ItemOfParams GetParam(string paramName) => _structure.Params.ItemOfParams.First(x => x.Name.Text == paramName);

        public void ClearStructure() => _structure = new();

        public List<ItemOfChildren> GetAllChildren(TreeLevel treeLevel, List<ItemOfChildren> childrens = null, List<ItemOfChildren> upperChildrens = null)
        {
            if (childrens == null)
            {
                childrens = new List<ItemOfChildren>();
                upperChildrens = new List<ItemOfChildren>();
            }

            for (int i = 0; i < treeLevel.Children.ItemOfChildren.Count; i++)
            {
                if (i != 0 && upperChildrens != null)
                {
                    childrens.AddRange(upperChildrens);
                }

                childrens.Add(treeLevel.Children.ItemOfChildren[i]);

                if (treeLevel.Children.ItemOfChildren[i].TreeLevel != null)
                {
                    // добавляем дублирующийся элемент
                    upperChildrens.Add(treeLevel.Children.ItemOfChildren[i]);

                    GetAllChildren(treeLevel.Children.ItemOfChildren[i].TreeLevel, childrens, upperChildrens);

                    // Убираем дубль после прохождения до последнего уровня рекурсии
                    upperChildrens.Reverse();
                    upperChildrens.RemoveAt(0);
                    upperChildrens.Reverse();
                }
            }

            return childrens;
        }

        private static void Swap<T>(ObservableCollection<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        private TreeLevel FillItemOfChildrenRecursively(TreeLevel treeLevel, List<ItemOfParams> itemOfParamsList)
        {
            if (itemOfParamsList.Count == 0)
            {
                return null;
            }

            ItemOfParams currentItemOfParams = itemOfParamsList[0];
            itemOfParamsList.RemoveAt(0);

            if (currentItemOfParams.Type.Kind == "real")
            {
                treeLevel.Children = new Children
                {
                    ItemOfChildren = new List<ItemOfChildren>()
                    {
                        new ItemOfChildren()
                            {
                            Lower = $"{currentItemOfParams.Lowerbound}",
                            Upper = $"{currentItemOfParams.Upperbound}",
                            TreeLevel = new TreeLevel()
                        }
                    }
                };

                treeLevel.Cpid = $"{currentItemOfParams.Uid}";
            }
            else
            {
                treeLevel.Children = new Children
                {
                    ItemOfChildren = new List<ItemOfChildren>()
                    {
                        new ItemOfChildren()
                        {
                            EnumIndex = "-1",
                            EnumId = "{8D994429-9DF2-4A72-AB46-E49FE3A7437C}",
                            Upperincl = 0,
                            Lowerincl = 0,
                            TreeLevel = new TreeLevel()
                        }
                    }
                };
                treeLevel.Cpid = $"{currentItemOfParams.Uid}";
            }

            treeLevel.Children.ItemOfChildren[0].TreeLevel = FillItemOfChildrenRecursively(treeLevel.Children.ItemOfChildren[0].TreeLevel, itemOfParamsList);
            return treeLevel;
        }
    }
}
