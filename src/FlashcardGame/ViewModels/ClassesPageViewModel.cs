using System.Collections.ObjectModel;
using System.Windows.Input;
using FlashcardGame.Models; // Ensure you have a reference to your "Game" project
using FlashcardGame.Views;
using System.Windows;
using System.Windows.Controls; // For Application.Current

namespace FlashcardGame.ViewModels
{
    public class ClassesPageViewModel : BaseViewModel 
    {
        private Student _currentStudent = new Student("Default Student");
        public Student CurrentStudent
        {
            get { return _currentStudent; }
            set
            {
                _currentStudent = value;
                OnPropertyChanged(nameof(CurrentStudent));
                OnPropertyChanged(nameof(StudentName)); // Notify UI when CurrentStudent changes
                UpdateClassesList(); // Update the displayed list when the student changes
            }
        }

        private ObservableCollection<Course> _classesList;

        public ObservableCollection<Course> ClassesList
        {
            get { return _classesList; }
            set
            {
                _classesList = value;
                OnPropertyChanged(nameof(ClassesList));
            }
        }

        // Public property to expose the student's name for binding
        public string StudentName
        {
            get { return _currentStudent?.Name; }
        }

        public ICommand AddClassCommand { get; }

        public ClassesPageViewModel()
        {
            CurrentStudent = new Student("Default Student");
            AddClassCommand = new RelayCommand(OpenAddClassDialog);
            UpdateClassesList();
        }

        private void OpenAddClassDialog()
        {
            var addClassDialog = new AddClassDialog();
            addClassDialog.Owner = Application.Current.MainWindow;

            if (addClassDialog.ShowDialog() == true)
            {
                string newClassName = addClassDialog.ClassName;

                if (!string.IsNullOrEmpty(newClassName))
                {
                    // Use the Student's method to add a new course
                    CurrentStudent.AddNewCourse(newClassName);
                    // Update the ObservableCollection to reflect the change
                    UpdateClassesList();
                }
            }
        }

        private void UpdateClassesList()
        {
            ClassesList = new ObservableCollection<Course>(CurrentStudent?.MyCourses ?? new System.Collections.Generic.List<Course>());
        }
    }

}