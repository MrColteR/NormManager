using NormManager.Models;
using System.Collections.ObjectModel;

namespace NormManager.ViewModels
{
    /// <summary>
    /// Пример рабочей XML структуры 
    /// </summary>
    internal class WorkXMLStruc
    {
        public WorkXMLStruc() 
        {
            Main main = new Main();
            main = new Main();
            main.Version = "1.0";
            main.Groups = new Groups()
            {
                ItemOfGroup = new ObservableCollection<ItemOfGroup>()
                {
                    new ItemOfGroup()
                    {
                        Name = new Name()
                        {
                            Localizable = true,
                            Text = "Папка"
                        },
                        Subgroups = new Subgroups()
                        {
                            ItemOfSubgroup = new ObservableCollection<ItemOfSubgroup>()
                            {
                                new ItemOfSubgroup()
                                {
                                    Quantities = new Quantities()
                                {
                                    ItemOfType = new ItemOfType()
                                    {
                                        ItemType = "normalinterval",
                                        Name = new Name()
                                        {
                                            Localizable = true,
                                            Text = "Айтем"
                                        }
                                    }
                                },
                                Pattern = new Pattern()
                                {
                                    Inherits = 1,
                                    Ccols = 1,
                                    Items = new Items()
                                    {
                                        String = new ObservableCollection<string>()
                                       {
                                           "{102B2E65-D2A7-438A-87C3-EBAEA31B7FE6}",
                                           "{892B2E65-D2A7-438A-87C3-EBAEA31B7FE6}"
                                       }
                                    }
                                },
                                }
                            }
                        },
                    }
                }
            };
            main.Params = new Params()
            {
                ItemOfParams = new ObservableCollection<ItemOfParams>()
                {
                    new ItemOfParams()
                    {
                        Uid = "{102B2E65-D2A7-438A-87C3-EBAEA31B7FE6}",
                        Name = new Name()
                        {
                            Localizable = true,
                            Text = "Типы"
                        },
                        Type = new Models.Type()
                        {
                            Kind = "enumeration",
                            Enum= new Models.Enum()
                            {
                                String = new ObservableCollection<Models.String>()
                                {
                                    new Models.String()
                                    {
                                        Localizable= true,
                                        Text = "Тип 1"
                                    },
                                    new Models.String()
                                    {
                                        Localizable= true,
                                        Text = "Тип 2"
                                    }
                                },
                                Uid = new ObservableCollection<string>()
                                {
                                    "{F2EA13BF-840D-497F-A6AD-C13A10B6FDAF}",
                                    "{5260A89B-2CDA-4075-8259-B117B01A457A}"
                                }
                            }
                        }
                    },
                    new ItemOfParams()
                    {
                        Uid = "{892B2E65-D2A7-438A-87C3-EBAEA31B7FE6}",
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
                    }
                },
            };
        }
    }
}
