using NormManager.ViewModels;
using System;
using System.Windows;

namespace NormManager.Views
{
    /// <summary>
    /// Interaction logic for AddParamsWindow.xaml
    /// </summary>
    public partial class AddParamsWindow : Window
    {
        public AddParamsWindow()
        {
            InitializeComponent();
            Closed += WindowClosed;
        }

        private void WindowClosed(object? sender, EventArgs e)
        {
            (DataContext as EditorTreeViewModel)?.Close.Execute(null);
        }
    }
}
