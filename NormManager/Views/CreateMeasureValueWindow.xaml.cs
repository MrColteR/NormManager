using NormManager.ViewModels;
using System.Windows;

namespace NormManager.Views
{
    /// <summary>
    /// Interaction logic for CreatieMeasureValueWindow.xaml
    /// </summary>
    public partial class CreateMeasureValueWindow : Window
    {
        public CreateMeasureValueWindow()
        {
            InitializeComponent();
            Closed += WindowClosed;
        }

        private void WindowClosed(object? sender, System.EventArgs e)
        {
            (DataContext as CreateNewFolderViewModel)?.Close.Execute(null);
        }
    }
}
