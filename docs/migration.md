# Migration Guide

This document describes the changes made during the refactoring process.

## Summary of Changes

- **Repository Structure**:
  - Moved source code to `src/FlashcardGame`.
  - Moved/Renamed solution file to `FlashcardGame.sln`.
  - Added unit tests in `tests/FlashcardGame.Tests`.
  - Renamed namespace from `FashcardGame_FinalProject_WPF_C_` to `FlashcardGame`.

## Code Refactoring

- **Models**:
  - Split `classes.cs` into individual files (`Student.cs`, `Course.cs`, `StudySet.cs`, `Flashcard.cs`) in `Models/`.
  - Converted public fields to Properties (PascalCase).
  - Encapsulated logic where appropriate.

- **ViewModels & Views**:
  - Updated namespaces to `FlashcardGame.ViewModels` and `FlashcardGame.Views`.
  - Updated property references to match new Model property names (e.g., `.name` -> `.Name`).

## Testing

- Added xUnit test project.
- Added unit tests for Models.

## CI/CD

- Added GitHub Actions workflow `.github/workflows/ci.yml` to build and test on push/PR.
