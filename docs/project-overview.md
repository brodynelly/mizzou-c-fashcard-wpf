# Project Overview

We have developed a flashcard-based studying tool that incorporates various methods, including user profiles, class creation, study set creation, and flashcard creation for quizzing purposes. 
The application is structured around the enrolled classes of students. Each student initiates by creating their own classes, which serve as folders for organizing related study sets.

Within each class, students can create an unlimited number of study sets. Upon creation, students can begin adding flashcards. 
Each flashcard contains a term and a definition. During study, the term is displayed on the front of the card, while the definition remains concealed until the card is flipped.
The program utilizes the terms and definitions to generate a quiz for students to assess their knowledge.

The quiz operates by presenting a term and displaying three randomly selected definitions, along with the correct definition, to track the knowledge gained through the flashcards.
The terms are randomly ordered, and the definitions are also randomly ordered. The score is recorded and when a user gets a question wrong, they are prompted to try the term again until they succeed. 

## Test Cases

### Successful Test Case 1
- A user creates a profile without spaces.
- A user creates a class and study sets with more than five terms.
- A user closes the program and inputs the same name to load their previous profile.

### Successful Test Case 2
- A user creates a profile without spaces.
- A user creates a class and study sets with more than five terms.
- A user quizzes themselves on the terms until they correctly answer all of them.

### Failed Test Case 1
- A user creates a profile with spaces, and the program accounts for the space by creating a string for the user that does not include the space.

### Failed Test Case 2
- A user inputs a non-integer value for the amount of flashcards when adding study sets.
- Accounts for error handling and prompts user to input an integer value.

## Acknowledgment
The application utilizes only the standard Windows included packages that were covered during the course.
