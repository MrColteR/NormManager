using Microsoft.Win32;
using NormManager.ViewModels;
using System.IO;
using System.Windows;

namespace NormManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Multiselect = false,
                Filter = "XML files (*.xml)|*.xml"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var vm = (DataContext as MainWindowViewModel);
                vm.Path = openFileDialog.FileName;
                vm.FileName = Path.GetFileNameWithoutExtension(openFileDialog.SafeFileName);
            }
        }
    }
}
