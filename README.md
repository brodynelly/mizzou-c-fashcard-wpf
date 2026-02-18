# Flashcard Game

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) [![CI](https://github.com/brodynelly/mizzou-c-fashcard-wpf/actions/workflows/ci.yml/badge.svg)](https://github.com/brodynelly/mizzou-c-fashcard-wpf/actions/workflows/ci.yml) ![C#](https://img.shields.io/badge/C%23-.NET_8-purple?logo=dotnet)

A WPF Flashcard application for students to manage courses, study sets, and flashcards.


## Tech Stack

- **C# / .NET 8** — application logic
- **WPF** — Windows Presentation Foundation UI  
- **MVVM** — architectural pattern
- **NUnit** — unit testing


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
