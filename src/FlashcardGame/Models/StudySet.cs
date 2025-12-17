using System.Collections.Generic;

namespace FlashcardGame.Models
{
    public class StudySet
    {
        public string Name { get; set; }
        public List<Flashcard> Cards { get; set; }

        public StudySet(string studySetName)
        {
            Name = studySetName;
            Cards = new List<Flashcard>();
        }

        public StudySet()
        {
            Cards = new List<Flashcard>();
        }

        public void AddNewFlashcard(string question, string answer)
        {
            Flashcard newCard = new Flashcard(question, answer);
            Cards.Add(newCard);
        }

        public int HowManyFlashcards()
        {
            return Cards.Count;
        }
    }
}
