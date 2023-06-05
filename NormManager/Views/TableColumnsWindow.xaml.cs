using NormManager.ViewModels;
using System;
using System.Windows;

namespace NormManager.Views
{
    /// <summary>
    /// Interaction logic for TableColumnsWindow.xaml
    /// </summary>
    public partial class TableColumnsWindow : Window
    {
        public TableColumnsWindow()
        {
            InitializeComponent();
            Closed += WindowClosed;
        }

        private void WindowClosed(object? sender, EventArgs e)
        {
            (DataContext as TableColumnsViewModel)?.Close.Execute(null);
        }
    }
}
