using System.Collections.Generic;
using System.Windows.Input;

namespace FlashcardGame.ViewModels
{
    internal class FlashcardsPageViewModel : BaseViewModel
    {
        // List of flashcards (you can replace this with your actual flashcard data)
        public List<Flashcard> Flashcards { get; set; }

        // Current flashcard the user is viewing
        private Flashcard _currentFlashcard;
        public Flashcard CurrentFlashcard
        {
            get { return _currentFlashcard; }
            set
            {
                _currentFlashcard = value;
                OnPropertyChanged(nameof(CurrentFlashcard)); // Notifies the UI when the flashcard changes
            }
        }

        // Command to move to the next flashcard
        public ICommand NextFlashcardCommand { get; }

        // Constructor to initialize the flashcards and command
        public FlashcardsPageViewModel()
        {
            // Initialize the list of flashcards (replace with actual flashcard data)
            Flashcards = new List<Flashcard>
            {
                new Flashcard { Question = "What is 2 + 2?", Answer = "4" },
                new Flashcard { Question = "What is the capital of France?", Answer = "Paris" },
                new Flashcard { Question = "Who wrote 'Hamlet'?", Answer = "Shakespeare" }
            };

            // Initialize the first flashcard to display
            CurrentFlashcard = Flashcards[0];

            // Initialize the NextFlashcardCommand
            NextFlashcardCommand = new RelayCommand(ShowNextFlashcard);
        }

        // Logic to show the next flashcard
        private void ShowNextFlashcard()
        {
            int currentIndex = Flashcards.IndexOf(CurrentFlashcard);
            if (currentIndex < Flashcards.Count - 1)
            {
                // Move to the next flashcard
                CurrentFlashcard = Flashcards[currentIndex + 1];
            }
            else
            {
                // If we are at the end, you can reset to the first flashcard (or handle as needed)
                CurrentFlashcard = Flashcards[0];
            }
        }
    }

    // Flashcard class to represent a single flashcard
    public class Flashcard
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
