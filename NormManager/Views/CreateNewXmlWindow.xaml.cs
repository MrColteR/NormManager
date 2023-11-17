using NormManager.ViewModels;
using System.Windows;

namespace NormManager.Views
{
    /// <summary>
    /// Interaction logic for CreateNewXmlFile.xaml
    /// </summary>
    public partial class CreateNewXmlWindow : Window
    {
        public CreateNewXmlWindow()
        {
            InitializeComponent();
            Closed += WindowClosed;
        }

        private void WindowClosed(object? sender, System.EventArgs e)
            => (DataContext as CreateNewXmlViewModel)?.Close.Execute(null);
    }
}
