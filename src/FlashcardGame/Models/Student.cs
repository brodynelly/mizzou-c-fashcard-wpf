using System.Collections.Generic;

namespace FlashcardGame.Models
{
    public class Student
    {
        public string Name { get; set; }
        public List<Course> MyCourses { get; set; }

        public Student(string studentName = "No Name")
        {
            Name = studentName;
            MyCourses = new List<Course>();
        }

        public Student()
        {
            MyCourses = new List<Course>();
        }

        public Course AddNewCourse(string courseName)
        {
            Course newCourse = new Course(courseName);
            MyCourses.Add(newCourse);
            return newCourse;
        }

        public int HowManyCourses()
        {
            return MyCourses.Count;
        }
    }
}
