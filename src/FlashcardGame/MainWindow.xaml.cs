using System.Windows;
using System.Windows.Controls; // Required for Frame
using FlashcardGame.Views; // Required for StudentConfig

namespace FlashcardGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // This is crucial: it creates elements like PageHostFrame from XAML.
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Check if the MainFrame property (which gets PageHostFrame) is accessible
            if (this.MainFrame != null)
            {
                // Navigate to StudentConfig.xaml as the very first page the user sees
                this.MainFrame.Navigate(new StudentConfig());
                System.Diagnostics.Debug.WriteLine("MainWindow_Loaded: Successfully navigated to StudentConfig.");
            }
            else
            {
                // This is a critical error if it happens.
                System.Diagnostics.Debug.WriteLine("MainWindow_Loaded: FATAL ERROR - this.MainFrame (PageHostFrame) is null. Check MainWindow.xaml for x:Name='PageHostFrame' and the MainFrame C# property.");
                MessageBox.Show("A critical error occurred while initializing the application's navigation. The application might not function correctly.", "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Public C# property that allows ViewModels to access the Frame.
        // It returns the Frame control named 'PageHostFrame' from your MainWindow.xaml.
        public Frame MainFrame
        {
            get
            {
                // this.PageHostFrame refers to the XAML element.
                // The '!' (null-forgiving operator) assumes PageHostFrame is always initialized.
                return this.PageHostFrame!;
            }
        }
    }
}