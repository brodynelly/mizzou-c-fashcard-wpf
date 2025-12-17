// File: views/StudySetsPage.xaml.cs
using FlashcardGame.Models; // For FlashcardGame.Models.Course
using FlashcardGame.ViewModels; // For your StudySets ViewModel
using System.Windows.Controls;
using System.Windows;
using System.Text.Json;
using System.IO;

namespace FlashcardGame.Views
{
    public partial class StudySetsPage : Page
    {
        StudySet flashSet;
        Course course;
        Student student;

        // Constructor that accepts the selected Course from ClassesPage and student
        public StudySetsPage(FlashcardGame.Models.Course? selectedCourse, Student studentCurrent)
        {
            InitializeComponent();
            if (selectedCourse != null)
            {
                // correct the current course and student and set the datacontext to the correct viewmodel cs
                this.DataContext = new StudySets(selectedCourse);
                CourseNameBox.Text = selectedCourse.Name;
                course = selectedCourse;
                student = studentCurrent;
            }
            else
            {
                this.DataContext = new StudySets();
            }

        }

        // Default constructor needed for json
        public StudySetsPage()
        {
            InitializeComponent();
            this.DataContext = new StudySets();

        }

        // when the add button is clicked create a new flashSet object and prompt the user with the flash card creation screen and if they complete that screen successfully update flashcards
        private void AddCardButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            flashSet = new StudySet(course.Name);
            FlashcardCreation flashcardCreation = new FlashcardCreation(flashSet);
            bool? result = flashcardCreation.ShowDialog();
            if (result == true)
            {
                UpdateFlashCards();
            }
        }

        // from the viewmodel cs file add the flashcard set to the gui and to the students course and update the json file to reflect this change
        private void UpdateFlashCards()
        {
            if(DataContext is StudySets viewModel)
            {
                course.FlashSets.Add(flashSet);
                viewModel.StudySetList?.Add(flashSet);

                string filePath = @$"Saved\{student.Name}.json";
                var options = new JsonSerializerOptions { IncludeFields = true };
                string updatedJson = JsonSerializer.Serialize(student, options);
                File.WriteAllText(filePath, updatedJson);
            }
        }

        // when they click the button to study the flashcard open the proper flashcard study screen
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is StudySets viewModel)
            {
                if (sender is Button button && button.DataContext is StudySet selectedSet)
                {
                    var mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow?.MainFrame.Navigate(new FlashcardsPage(selectedSet));
                }
            }
        }
    }
}