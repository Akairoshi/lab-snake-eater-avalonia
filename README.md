# SnakeEater (Avalonia UI)

A classic "Snake" game developed in C# using the Avalonia UI cross-platform framework.
This project was created for educational purposes to practice Object-Oriented Programming (OOP) and graphical user interface (GUI) development.

## Tech Stack
* **Language:** C# 12 (.NET 9)
* **Framework:** Avalonia UI 11.3
* **Data Storage:** File System (System.IO)

## Architectural Features
* **Immutability:** Used **Records** to handle game grid coordinates securely.
* **Game Loop:** Implemented via **DispatcherTimer** for consistent frame updates.
* **Asynchronous UI:** Handles window interactions and dialogs using `ShowDialog`.
* **Input Handling:** Advanced keyboard event filtering to prevent invalid 180° turns.
* **File Management:** Automatic directory and file creation for storing local high scores.

## How to Run
1. Install the **.NET 9 SDK**.
2. Clone this repository.
3. Run the command: `dotnet run`.

## Roadmap
- [ ] **Rendering Optimization:** Replace full `Canvas` clearing with an Object Pooling pattern to update existing object positions.
- [ ] **Refactoring:** Transition to the **MVVM** pattern to decouple business logic from the UI.
- [ ] **UI/UX:** Add custom window chrome and animations for a more modern look.

## Project Structure
* `GameWindow.axaml`: The main game arena and logic.
* `ScoreWindow.axaml`: High score display and data entry.
* `App.axaml`: Application styles and entry point configuration.