using NormManager.ViewModels;
using System;
using System.Windows;

namespace NormManager.Views
{
    /// <summary>
    /// Interaction logic for TableColumnsWindow.xaml
    /// </summary>
    public partial class EditTableColumnsWindow : Window
    {
        public EditTableColumnsWindow()
        {
            InitializeComponent();
            Closed += WindowClosed;
        }

        private void WindowClosed(object? sender, EventArgs e)
        {
            (DataContext as EditTableColumnsViewModel)?.Close.Execute(null);
        }
    }
}
