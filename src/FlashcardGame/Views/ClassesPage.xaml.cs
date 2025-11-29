using System.Windows.Controls;
using FlashcardGame.ViewModels;
using FlashcardGame.Models; // Make sure you have this using statement
using System.Windows;
using System.IO;
using System.Text.Json; // For RoutedEventArgs

namespace FlashcardGame.Views
{
    /// <summary>
    /// Interaction logic for ClassesPage.xaml
    /// </summary>
    public partial class ClassesPage : Page
    {
        Student student;

        // default constructor for json
        public ClassesPage()
        {
            InitializeComponent();
        }
        
        // constructor that is passed the current student and sets that up for the rest of this view
        public ClassesPage(Student currentStudent)
        {
            InitializeComponent();
            student = currentStudent;
        }

        // on click of view sets button switch to that classes flashcard sets view model
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ClassesPageViewModel viewModel)
            {
                if (sender is Button button && button.DataContext is Course selectedCourse)
                {
                    var mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow?.MainFrame.Navigate(new StudySetsPage(selectedCourse, student));
                }
            }
        }

        // on click of creation of new class button prompt user with pop up for creating class and when they save that add the class to the students list of classes
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // open the dialog for creating a class
            var addClassDialog = new AddClassDialog();
            addClassDialog.Owner = Application.Current.MainWindow;

            // when saved
            if (addClassDialog.ShowDialog() == true)
            {
                string newClassName = addClassDialog.ClassName;
                // if the class name is valid add it to the students course list and the xaml list of things to display and update the json file
                if (!string.IsNullOrEmpty(newClassName))
                {
                    // Use the Student's method to add a new course
                    Course course = student.AddNewCourse(newClassName);
                    var viewModel = this.DataContext as ClassesPageViewModel;
                    viewModel.ClassesList.Add(course);


                    string filePath = @$"Saved\{student.Name}.json";
                    var options = new JsonSerializerOptions { IncludeFields = true };
                    string updatedJson = JsonSerializer.Serialize(student, options);
                    File.WriteAllText(filePath, updatedJson);
                }
            }

        }
    }
}