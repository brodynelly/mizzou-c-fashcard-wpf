# Flashcard Game

A WPF Flashcard application for students to manage courses, study sets, and flashcards.

## Features

- Create and manage Students.
- Create Courses and Study Sets.
- Add Flashcards (Question/Answer) to Study Sets.
- Quiz mode to test your knowledge.

## Project Structure

- `src/FlashcardGame`: Main WPF Application.
- `tests/FlashcardGame.Tests`: Unit tests.

## Getting Started

### Prerequisites

- .NET 8.0 SDK

### Building

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

## Architecture

This project follows the MVVM (Model-View-ViewModel) pattern.
- **Models**: `Student`, `Course`, `StudySet`, `Flashcard` (POCOs).
- **ViewModels**: Handle logic and state.
- **Views**: XAML definitions for the UI.

## License

See [LICENSE.txt](LICENSE.txt).
