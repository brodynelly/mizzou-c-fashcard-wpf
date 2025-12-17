using Xunit;
using FlashcardGame.Models;

namespace FlashcardGame.Tests
{
    public class CourseTests
    {
        [Fact]
        public void AddNewStudySet_ShouldAddStudySetToCourse()
        {
            // Arrange
            var course = new Course("Science");
            var studySetName = "Physics";

            // Act
            course.AddNewStudySet(studySetName);

            // Assert
            Assert.Single(course.FlashSets);
            Assert.Equal(studySetName, course.FlashSets[0].Name);
            Assert.Equal(1, course.HowManyStudySets());
        }
    }
}
