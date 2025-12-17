using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlashcardGame.ViewModels;
using FlashcardGame.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FlashcardGame.Views
{
    /// <summary>
    /// Interaction logic for QuizPage.xaml
    /// </summary>
    public partial class QuizPage : Page
    {
        private readonly StudySet _studySet;

        public QuizPage(StudySet flashSet)
        {
            InitializeComponent();
            _studySet = flashSet;
            // Create the ViewModel and pass the StudySet to it
            var viewModel = new QuizViewModel(_studySet);
            // Set the DataContext for the Page
            this.DataContext = viewModel;
        }
        // Add a parameterless constructor if the XAML designer requires it,
        // but ensure it handles the null StudySet gracefully (e.g., shows an error).
        public QuizPage()
        {

        }
    }
}