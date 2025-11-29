using System.Collections.Generic;

namespace FlashcardGame.Models
{
    public class Course
    {
        public string Name { get; set; }
        public List<StudySet> FlashSets { get; set; }

        public Course(string courseName)
        {
            Name = courseName;
            FlashSets = new List<StudySet>();
        }

        public Course()
        {
            FlashSets = new List<StudySet>();
        }

        public void AddNewStudySet(string studySetName)
        {
            StudySet newStudySet = new StudySet(studySetName);
            FlashSets.Add(newStudySet);
        }

        public void AddNewStudySet(StudySet studySet)
        {
            FlashSets.Add(studySet);
        }

        public int HowManyStudySets()
        {
            return FlashSets.Count;
        }
    }
}
