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
        private bool _dataGridIsVisible = false;
        public EditorTreeWindow()
        {
            InitializeComponent();
            Closed += WindowClosed;
        }

        private void WindowClosed(object? sender, EventArgs e)
            => (DataContext as EditorTreeViewModel)?.Close.Execute(null);

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

        private void SolutionTree_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dataContext = (EditorTreeViewModel)DataContext;
            var oldSelectedSubmainItem = dataContext.SelectedSubmainItem is null ? new SubmainTreeElement() { MeasurableQuantityName = string.Empty } : dataContext.SelectedSubmainItem;

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
            else
            {
                DataGridClear();
                GenerateDataGrid(); 
                dataContext.SelectedMainItem = null;
                dataContext.SelectedSubmainItem = null;
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
            _dataGridIsVisible = true;
            var dataContext = ((EditorTreeViewModel)DataContext);
            if (dataContext.SelectedSubmainItem != null)
            {
                var countItems = dataContext.SelectedSubmainItem.ParametersIncludeInValue.Count;
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
                        break;

                    case MeasuredQuantityType.Fixedgradations:
                        DataGridAddColumn(DataGrid, "Фиксированные градации", countItems);
                        break;

                    //case TypeMeasuredQuantity.Variablegradations:
                    //    break;

                    case MeasuredQuantityType.Normalinterval:
                        DataGridAddColumn(DataGrid, "Нижняя граница", countItems);
                        DataGridAddColumn(DataGrid, "Норма", countItems + 1);
                        DataGridAddColumn(DataGrid, "Вверхняя граница", countItems + 2);
                        DataGridAddColumn(DataGrid, "Ср. отклонение", countItems + 3);
                        break;

                    //case TypeMeasuredQuantity.Fixedgradationsnorm:
                    //    break;

                    case MeasuredQuantityType.String:
                        DataGridAddColumn(DataGrid, "Строка", countItems);
                        break;
                }

                var rows = new List<object>();
                var childrens = dataContext.GetAllChildren();
                var countParams = dataContext.SelectedSubmainItem.ParametersIncludeInValue.Count;
                for (int i = 0; i < childrens.Count; i += countParams)
                {
                    var rowValues = new List<object>();
                    for (int j = 0; j < countParams && i + j < childrens.Count; j++)
                    {
                        var lowerbound = childrens[i + j].Lower;
                        var upperbound = childrens[i + j].Upper;
                        string value = (lowerbound == null || upperbound == null) ? "Любой" : $"{lowerbound} ... {upperbound}";
                        rowValues.Add(value);
                    }

                    rows.Add(rowValues);
                }

                DataGrid.ItemsSource = rows;
            }
        }

        private void DataGridAddColumn(DataGrid datagrid, string header, int bindingIndex) => datagrid.Columns.Add(new DataGridTextColumn()
        { 
            Header = header,
            Binding = new Binding("[" + bindingIndex + "]")
        });

        private void DataGridClear() => DataGrid.Columns.Clear();

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            DataGridCell cell = GetCell(dataGrid, e);
            if (cell != null)
            {
                object cellValue = cell.Content;
                int rowIndex = dataGrid.Items.IndexOf(cell.DataContext);
                int columnIndex = dataGrid.Columns.IndexOf(cell.Column);
            }
        }

        private DataGridCell GetCell(DataGrid dataGrid, MouseButtonEventArgs e)
        {
            Visual hitTestResult = (Visual)VisualTreeHelper.HitTest(dataGrid, e.GetPosition(dataGrid)).VisualHit;

            while (hitTestResult != null && hitTestResult != dataGrid)
            {
                if (hitTestResult is DataGridCell)
                {
                    return (DataGridCell)hitTestResult;
                }
                hitTestResult = (Visual)VisualTreeHelper.GetParent(hitTestResult);
            }

            return null;
        }
    }
}
