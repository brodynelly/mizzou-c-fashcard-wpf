// File: viewModels/QuizViewModel.cs
using System.Collections.ObjectModel;
using System.Windows; // For Application.Current
using System.Windows.Input;
using FlashcardGame.Models; // For StudySet, Flashcard

namespace FlashcardGame.ViewModels
{
    public class QuizViewModel : BaseViewModel
    {
        // --- Fields ---

        private Random _random = new Random();
        private bool _isAnswerSubmitted;
        private string? _selectedAnswer; 
        private string _feedbackMessage = "";
        private bool _isQuizComplete;
        private string _quizProgressText = "";
        private int _correctAnswersOnFirstTry = 0;
        private int _totalQuestionsInSet = 0;
        private readonly StudySet _originalStudySet;
        private List<FlashcardGame.Models.Flashcard> _allFlashcards;
        private List<FlashcardGame.Models.Flashcard> _questionsToAsk;
        private List<FlashcardGame.Models.Flashcard> _incorrectlyAnswered;
        private FlashcardGame.Models.Flashcard? _currentFlashcard;


        public string CurrentQuestionText => _currentFlashcard?.Question ?? "Loading...";

        // Represents an answer option for the quiz
        public class AnswerOption : BaseViewModel
        {
            public string Text { get; set; }
            private bool _isSelected;
            public bool IsSelected
            {
                get => _isSelected;
                set { _isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
            }
        }

        // Collection of answer options for the current question
        public ObservableCollection<AnswerOption> AnswerOptions { get; } = new ObservableCollection<AnswerOption>();

        // Gets the selected answer text from the collection
        public string? SelectedAnswer
        {
            get => AnswerOptions.FirstOrDefault(opt => opt.IsSelected)?.Text;
        }

        // Feedback message to display to the user
        public string FeedbackMessage
        {
            get => _feedbackMessage;
            private set { _feedbackMessage = value; OnPropertyChanged(nameof(FeedbackMessage)); }
        }

        // Text showing quiz progress
        public string QuizProgressText
        {
            get => _quizProgressText;
            private set { _quizProgressText = value; OnPropertyChanged(nameof(QuizProgressText)); }
        }

        // Indicates if the answer has been submitted for the current question
        public bool IsAnswerSubmitted
        {
            get => _isAnswerSubmitted;
            private set
            {
                _isAnswerSubmitted = value;
                OnPropertyChanged(nameof(IsAnswerSubmitted));
            }
        }

        // Indicates if the quiz is complete
        public bool IsQuizComplete
        {
            get => _isQuizComplete;
            private set { _isQuizComplete = value; OnPropertyChanged(nameof(IsQuizComplete)); }
        }

        // Message showing the final score
        public string FinalScoreMessage => $"Quiz Complete! Score: {_correctAnswersOnFirstTry}/{_totalQuestionsInSet} correct on the first try.";


        public ICommand SubmitAnswerCommand { get; }
        public ICommand NextQuestionCommand { get; }
        public ICommand RetakeQuizCommand { get; }
        public ICommand ReturnToStudySetsCommand { get; }


        // Updates the quiz progress text
        private void UpdateProgressText()
        {
            int totalAsked = _totalQuestionsInSet - _questionsToAsk.Count;
            int retriesPending = _incorrectlyAnswered.Count;
            QuizProgressText = $"Progress: {totalAsked}/{_totalQuestionsInSet} unique questions attempted. Retries pending: {retriesPending}";
        }

        // Generates answer options for the current question (1 correct + up to 3 incorrect)
        private void GenerateAnswerOptions()
        {
            if (_currentFlashcard == null) return;

            AnswerOptions.Clear();
            string correctAnswer = _currentFlashcard.Answer;

            // Get potential incorrect answers (all answers except the correct one)
            var incorrectOptions = _allFlashcards
                .Select(fc => fc.Answer)
                .Where(ans => ans != correctAnswer)
                .Distinct() 
                .OrderBy(x => _random.Next()) 
                .ToList();

            // Determine how many incorrect options to add (max 3, or fewer if not enough available)
            int numberOfIncorrectToAdd = Math.Min(3, incorrectOptions.Count);

            // Add the incorrect options
            for (int i = 0; i < numberOfIncorrectToAdd; i++)
            {
                AnswerOptions.Add(new AnswerOption { Text = incorrectOptions[i], IsSelected = false });
            }

            AnswerOptions.Add(new AnswerOption { Text = correctAnswer, IsSelected = false });

            var shuffledOptions = AnswerOptions.OrderBy(x => _random.Next()).ToList();
            AnswerOptions.Clear();
            foreach (var option in shuffledOptions)
            {
                AnswerOptions.Add(option);
            }
        }

        // Determines if the user can submit an answer
        private bool CanSubmitAnswer()
        {
            // Can submit only if an answer is selected AND the answer hasn't already been submitted for this question
            return SelectedAnswer != null && !IsAnswerSubmitted;
        }

        private HashSet<FlashcardGame.Models.Flashcard> _alreadyAttemptedFirstTry = new HashSet<FlashcardGame.Models.Flashcard>();
        private HashSet<FlashcardGame.Models.Flashcard> _alreadyAttempted = new HashSet<FlashcardGame.Models.Flashcard>();

        // Starts or restarts the quiz
        private void StartQuiz_Final()
        {
            _questionsToAsk = _allFlashcards.OrderBy(x => _random.Next()).ToList();
            _incorrectlyAnswered = new List<FlashcardGame.Models.Flashcard>();
            _alreadyAttemptedFirstTry.Clear(); 
            IsQuizComplete = false;
            IsAnswerSubmitted = false;
            _correctAnswersOnFirstTry = 0; 
            _totalQuestionsInSet = _allFlashcards.Count; 

            if (_allFlashcards.Any())
            {
                LoadNextQuestion_Final();
            }
            else
            {
                FeedbackMessage = "This study set has no flashcards to quiz!";
                IsQuizComplete = true;
            }
        }

        // Loads the next question, either from incorrect answers or new questions
        private void LoadNextQuestion_Final()
        {
            IsAnswerSubmitted = false;
            FeedbackMessage = "";
            _selectedAnswer = null; 

            // Determine which list to pull from
            if (_incorrectlyAnswered.Any())
            {
                int index = _random.Next(_incorrectlyAnswered.Count);
                _currentFlashcard = _incorrectlyAnswered[index];
            }
            else if (_questionsToAsk.Any())
            {
                _currentFlashcard = _questionsToAsk[0];
                _questionsToAsk.RemoveAt(0);
            }
            else
            {
                EndQuiz();
                return;
            }

            GenerateAnswerOptions();
            UpdateProgressText();
            OnPropertyChanged(nameof(CurrentQuestionText));
        }

        private void SubmitAnswer_Final()
        {
            if (_currentFlashcard == null || SelectedAnswer == null) return;

            bool isCorrect = SelectedAnswer == _currentFlashcard.Answer;
            bool firstAttempt = !_alreadyAttemptedFirstTry.Contains(_currentFlashcard);

            bool wasRetryAttempt = _incorrectlyAnswered.Contains(_currentFlashcard);
            if (!wasRetryAttempt)
            {
                _alreadyAttemptedFirstTry.Add(_currentFlashcard); 
            }

            if (isCorrect)
            {
                FeedbackMessage = "Correct!";
                if (firstAttempt && !wasRetryAttempt)
                { 
                    _correctAnswersOnFirstTry++;
                }
                _incorrectlyAnswered.Remove(_currentFlashcard); 
            }
            else
            {
                FeedbackMessage = $"Incorrect. The correct answer was: {_currentFlashcard.Answer}";
                if (!_incorrectlyAnswered.Contains(_currentFlashcard))
                {
                    _incorrectlyAnswered.Add(_currentFlashcard);
                }
            }
            IsAnswerSubmitted = true;
        }

        // Determines if the user can load the next question
        private bool CanLoadNextQuestion()
        {
            return IsAnswerSubmitted;
        }

        private void EndQuiz()
        {
            IsQuizComplete = true;
            _currentFlashcard = null; 
            AnswerOptions.Clear(); 
            OnPropertyChanged(nameof(CurrentQuestionText));
            OnPropertyChanged(nameof(FinalScoreMessage)); 
            UpdateProgressText(); 
        }

        // Navigates back to the study sets or previous page
        private void ReturnToStudySets()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow?.MainFrame != null && mainWindow.MainFrame.CanGoBack)
            {
                mainWindow.MainFrame.GoBack(); 
                if (mainWindow.MainFrame.CanGoBack)
                { 
                    mainWindow.MainFrame.GoBack();
                }
            }
            if (mainWindow?.MainFrame != null && mainWindow.MainFrame.CanGoBack)
            {
                mainWindow.MainFrame.GoBack();
            }
        }


        // Initializes the QuizViewModel with the given study set
        public QuizViewModel(StudySet studySet)
        {
            _originalStudySet = studySet ?? throw new ArgumentNullException(nameof(studySet));
            _allFlashcards = new List<FlashcardGame.Models.Flashcard>(_originalStudySet.Cards ?? new List<FlashcardGame.Models.Flashcard>()); // Defensive copy

            // Assign the FINAL versions of commands/methods
            SubmitAnswerCommand = new RelayCommand(SubmitAnswer_Final, CanSubmitAnswer);
            NextQuestionCommand = new RelayCommand(LoadNextQuestion_Final, CanLoadNextQuestion);
            RetakeQuizCommand = new RelayCommand(StartQuiz_Final);
            ReturnToStudySetsCommand = new RelayCommand(ReturnToStudySets); 

            if (_allFlashcards.Any())
            {
                StartQuiz_Final();
            }
            else
            {
                FeedbackMessage = "This study set has no flashcards to quiz!";
                IsQuizComplete = true;
                UpdateProgressText(); // Show 0/0 progress
            }
        }
    }
}