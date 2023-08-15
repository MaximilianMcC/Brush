using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace Brush
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

        // Check for shortcuts
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.I) ImportMedia();
        }

        private void ImportMedia_Click(object sender, RoutedEventArgs e)
        {
            ImportMedia();
        }




        // Import media like an image or something idk
        private void ImportMedia()
        {
            // Create the file dialogue to get the media
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg",
                Title = "Select media to import"
            };

            // Get the selected thing from the dialog
            if (fileDialog.ShowDialog() == true)
            {
                string filePath = fileDialog.FileName;
                string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                outputLabel.Content = "Imported " + fileName;
            }
        }

    }

    
}
