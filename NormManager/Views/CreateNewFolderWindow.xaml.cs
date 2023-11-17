using NormManager.ViewModels;
using System.Windows;

namespace NormManager.Views
{
    /// <summary>
    /// Interaction logic for CreateNewFolder.xaml
    /// </summary>
    public partial class CreateNewFolderWindow : Window
    {
        public CreateNewFolderWindow()
        {
            InitializeComponent();
            Closed += WindowClosed;
        }

        private void WindowClosed(object? sender, System.EventArgs e)
            => (DataContext as CreateNewFolderViewModel)?.Close.Execute(null);
    }
}
