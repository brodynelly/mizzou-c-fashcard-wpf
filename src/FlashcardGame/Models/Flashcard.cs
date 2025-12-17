namespace FlashcardGame.Models
{
    public class Flashcard
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public Flashcard(string cardQuestion, string cardAnswer)
        {
            Question = cardQuestion;
            Answer = cardAnswer;
        }

        public Flashcard() { }
    }
}
