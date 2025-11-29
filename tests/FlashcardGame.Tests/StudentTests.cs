using Xunit;
using FlashcardGame.Models;

namespace FlashcardGame.Tests
{
    public class StudentTests
    {
        [Fact]
        public void AddNewCourse_ShouldAddCourseToStudent()
        {
            // Arrange
            var student = new Student("Test Student");
            var courseName = "Math";

            // Act
            var course = student.AddNewCourse(courseName);

            // Assert
            Assert.NotNull(course);
            Assert.Equal(courseName, course.Name);
            Assert.Contains(course, student.MyCourses);
            Assert.Equal(1, student.HowManyCourses());
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var student = new Student("Test Student");

            // Assert
            Assert.Equal("Test Student", student.Name);
            Assert.NotNull(student.MyCourses);
            Assert.Empty(student.MyCourses);
        }
    }
}
