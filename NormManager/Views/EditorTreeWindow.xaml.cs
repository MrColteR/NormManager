using NormManager.Models;
using NormManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace NormManager.Views
{
    /// <summary>
    /// Interaction logic for TreeWindow.xaml
    /// </summary>
    public partial class EditorTreeWindow : Window
    {
        private const string SOLUTION_CONTEXT = "SolutionContext";
        public EditorTreeWindow()
        {
            InitializeComponent();
            Closed += WindowClosed;
        }

        private void WindowClosed(object? sender, EventArgs e)
        {
            (DataContext as EditorTreeViewModel)?.Close.Execute(null);
        }

        private void SolutionTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var value = e.NewValue;
            var dataContext = (EditorTreeViewModel)DataContext;
            var oldSelectedSubmainItem = dataContext.SelectedSubmainItem is null ? new SubmainTreeElement() { MeasurableQuantityName = string.Empty } : dataContext.SelectedSubmainItem;
            dataContext.SelectedMainItem = null;
            dataContext.SelectedSubmainItem = null;

            if (value is SubmainTreeElement submain)
            {
                dataContext.SelectedSubmainItem = submain;
                var folderName = dataContext.SelectedSubmainItem.FolderName;
                var newSelectedfolder = dataContext.MainElemenstList.First(x => x.FolderName == folderName);
                dataContext.SelectedMainItem = dataContext.MainElemenstList.First(x => x == newSelectedfolder);
                if (oldSelectedSubmainItem.MeasurableQuantityName != submain.MeasurableQuantityName)
                {
                    DataGridClear();
                    GenerateDataGrid();
                }
            }
            else if (value is MainTreeElement main)
            {
                dataContext.SelectedMainItem = main;
            }
        }

        private void SolutionTree_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataContext = (EditorTreeViewModel)DataContext;
            var oldSelectedSubmainItem = dataContext.SelectedSubmainItem is null ? new SubmainTreeElement() { MeasurableQuantityName = string.Empty } : dataContext.SelectedSubmainItem;
            dataContext.SelectedMainItem = null;
            dataContext.SelectedSubmainItem = null;

            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                if (treeViewItem.DataContext is SubmainTreeElement submain)
                {
                    dataContext.SelectedSubmainItem = submain;
                    var folderName = dataContext.SelectedSubmainItem.FolderName;
                    var newSelectedfolder = dataContext.MainElemenstList.First(x => x.FolderName == folderName);
                    dataContext.SelectedMainItem = dataContext.MainElemenstList.First(x => x == newSelectedfolder);
                    if (oldSelectedSubmainItem.MeasurableQuantityName != submain.MeasurableQuantityName)
                    {
                        DataGridClear();
                        GenerateDataGrid();
                    }
                }
                else if (treeViewItem.DataContext is MainTreeElement main)
                {
                    dataContext.SelectedMainItem = main;                    
                }
            }

            SolutionTree.ContextMenu = SolutionTree.Resources[SOLUTION_CONTEXT] as ContextMenu;
        }

        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
            {
                source = VisualTreeHelper.GetParent(source);
            }

            return source as TreeViewItem;
        }

        private void GenerateDataGrid()
        {
            var dataContext = ((EditorTreeViewModel)DataContext);
            var countItems = dataContext.SelectedSubmainItem.ParametersIncludeInValue.Count;
            int countColumns = 0; 
            for (int i = 0; i < countItems; i++)
            {
                DataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = $"{dataContext.SelectedSubmainItem.ParametersIncludeInValue[i].Name.Text}",
                    Binding = new Binding("[" + i + "]")
                }); 
            }

            switch (dataContext.SelectedSubmainItem.ValueType)
            {
                case MeasuredQuantityType.Formula:
                    DataGridAddColumn(DataGrid, "Формула", countItems);
                    countColumns = countItems + 1;
                    break;

                case MeasuredQuantityType.Fixedgradations:
                    DataGridAddColumn(DataGrid, "Фиксированные градации", countItems);
                    countColumns = countItems + 1;
                    break;

                //case TypeMeasuredQuantity.Variablegradations:
                //    break;

                case MeasuredQuantityType.Normalinterval:
                    DataGridAddColumn(DataGrid, "Нижняя граница", countItems);
                    DataGridAddColumn(DataGrid, "Норма", countItems + 1);
                    DataGridAddColumn(DataGrid, "Вверхняя граница", countItems + 2);
                    DataGridAddColumn(DataGrid, "Ср. отклонение", countItems + 3);
                    countColumns = countItems + 4;
                    break;

                //case TypeMeasuredQuantity.Fixedgradationsnorm:
                //    break;

                case MeasuredQuantityType.String:
                    DataGridAddColumn(DataGrid, "Строка", countItems);
                    countColumns = countItems + 1;
                    break;
            }

            List<object> rows = new();
            string[] value = new string[countColumns];

            for (int i = 0; i < countItems; i++)
            {
                var lowerbound = dataContext.SelectedSubmainItem.ParametersIncludeInValue[i].Lowerbound;
                var upperbound = dataContext.SelectedSubmainItem.ParametersIncludeInValue[i].Upperbound;
                value[i] = (lowerbound == null || upperbound == null) ? "Любой" : $"{lowerbound} ... {upperbound}";
            }

            rows.Add(value);
            DataGrid.ItemsSource = rows;
        }

        private void DataGridAddColumn(DataGrid datagrid, string header, int bindingIndex) => datagrid.Columns.Add(new DataGridTextColumn()
        { 
            Header = header,
            Binding = new Binding("[" + bindingIndex + "]")
        });

        private void DataGridClear() => DataGrid.Columns.Clear();

        private void DataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // your code to handle the right-click event goes here
        }
    }
}
