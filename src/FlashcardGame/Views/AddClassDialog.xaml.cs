using System.Windows;

namespace FlashcardGame.Views
{
    public partial class AddClassDialog : Window
    {
        public string ClassName { get; private set; }
        public string Description { get; private set; }
        public bool IsAdded { get; private set; } = false;

        public AddClassDialog()
        {
            InitializeComponent();
        }

        // when they click add tell the place that called it that it worked and to update the info with the info provided
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ClassNameTextBox.Text))
            {
                ClassName = ClassNameTextBox.Text;
                Description = DescriptionTextBox.Text;
                IsAdded = true;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please enter a class name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // tells the caller to not update the information
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}