using FlashcardGame.Views;
using System.Windows;
using System.Windows.Input;

namespace FlashcardGame.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private StudentConfigViewModel studentConfig;
        public ICommand NavigateToClassesCommand { get; }
        public ICommand NavigateToCoursesCommand { get; }
        public ICommand NavigateToFlashcardsCommand { get; }
        public ICommand NavigateToStudentCommand { get; }

        private BaseViewModel _currentPageViewModel; // Or a more specific base class/interface

        public BaseViewModel CurrentPageViewModel
        {
            get { return _currentPageViewModel; }
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }

        public MainViewModel()
        {
            studentConfig = new StudentConfigViewModel();
            NavigateToStudentCommand = new RelayCommand(NavigateToStudent);
            NavigateToClassesCommand = new RelayCommand(NavigateToClasses);
            NavigateToCoursesCommand = new RelayCommand(NavigateToCourses);
            NavigateToFlashcardsCommand = new RelayCommand(NavigateToFlashcards);


        }

        public void NavigateToStudent()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.MainFrame != null)
            {
                mainWindow.MainFrame.Navigate(new StudentConfig()); // Navigate to the StudentConfig page
            }
        }

        private void NavigateToClasses()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new ClassesPage());
        }

        private void NavigateToCourses()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new StudySetsPage());
        }

        private void NavigateToFlashcards()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new FlashcardsPage());
        }
    }
}
