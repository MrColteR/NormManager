using NormManager.ViewModels;
using System.Windows;

namespace NormManager.Views
{
    /// <summary>
    /// Interaction logic for CreatieMeasureValueWindow.xaml
    /// </summary>
    public partial class EditMeasureValueWindow : Window
    {
        public EditMeasureValueWindow()
        {
            InitializeComponent();
            Closed += WindowClosed;
        }

        private void WindowClosed(object? sender, System.EventArgs e)
            => (DataContext as EditorTreeViewModel)?.Close.Execute(null);
    }
}
