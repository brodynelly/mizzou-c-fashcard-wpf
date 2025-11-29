// File: viewModels/StudySets.cs
using FlashcardGame.Models; // For Course and StudySet models
using System.Collections.ObjectModel;
using System.Collections.Generic; // Added for List<string> if you keep AvailableCourses
using System.Windows.Input;    // For ICommand

namespace FlashcardGame.ViewModels
{
    public class StudySets : BaseViewModel // Assuming BaseViewModel for OnPropertyChanged
    {
        private FlashcardGame.Models.Course? _currentCourse;
        public FlashcardGame.Models.Course? CurrentCourse
        {
            get => _currentCourse;
            private set // Typically set only once via constructor when page is loaded with a course
            {
                _currentCourse = value;
                OnPropertyChanged(nameof(CurrentCourse));
                OnPropertyChanged(nameof(CourseName));
                LoadStudySets();
            }
        }

        public string? CourseName => CurrentCourse?.Name;

        private ObservableCollection<StudySet>? _studySetList;
        public ObservableCollection<StudySet>? StudySetList
        {
            get => _studySetList;
            set
            {
                _studySetList = value;
                OnPropertyChanged(nameof(StudySetList));
            }
        }

        // Constructor that takes the selected FlashcardGame.Models.Course object
        public StudySets(FlashcardGame.Models.Course course)
        {
            // The CurrentCourse property setter will call LoadStudySets
            CurrentCourse = course;
        }

        // Parameterless constructor:
        // Useful if StudySetsPage can be opened without a specific course (e.g., to show a selection list)
        // Or for XAML design-time instantiation if you use d:DataContext="{d:DesignInstance ...}"
        public StudySets()
        {
            // Initialize for a state where no course is selected, or load default data.
            // For example, if you want to use AvailableCourses and StartCourseCommand for this scenario:
            // InitializeStartCourseFunctionality(); 
            StudySetList = new ObservableCollection<StudySet>(); // Empty list initially
        }

        private void LoadStudySets()
        {
            if (CurrentCourse?.FlashSets != null)
            {
                StudySetList = new ObservableCollection<StudySet>(CurrentCourse.FlashSets);
            }
            else
            {
                StudySetList = new ObservableCollection<StudySet>();
            }
        }

        private string? _selectedCourseNameFromList; // Was 'SelectedCourse' (string) in your version
        public string? SelectedCourseNameFromList
        {
            get { return _selectedCourseNameFromList; }
            set
            {
                _selectedCourseNameFromList = value;
                OnPropertyChanged(nameof(SelectedCourseNameFromList));
            }
        }

        public List<string>? AvailableCourses { get; private set; }
        public ICommand? StartCourseCommand { get; private set; }

        // Call this from the parameterless constructor if you want this functionality active by default
        private void InitializeStartCourseFunctionality()
        {
            StartCourseCommand = new RelayCommand(StartCourseByName); // Assuming RelayCommand (non-generic)
            AvailableCourses = new List<string> { "Math 101 (Example)", "Science 101 (Example)", "History 101 (Example)" };
        }

        private void StartCourseByName()
        {
            if (!string.IsNullOrEmpty(SelectedCourseNameFromList))
            {
                // Logic to start the course based on its name
                // This is where you might load a different course or navigate differently
                // For now, just updates the string property as in your example:
                SelectedCourseNameFromList = $"Starting {SelectedCourseNameFromList}... (Example Action)";
                // If you want to load a FlashcardGame.Models.Course object based on this name, you'd need a lookup mechanism.
            }
        }
    }
}