# SnakeEater (Avalonia UI)

A classic "Snake" game developed in C# using the Avalonia UI cross-platform framework.
This project was created for educational purposes to practice Object-Oriented Programming (OOP) and graphical user interface (GUI) development.

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

## Project Structure
* `GameWindow.axaml`: The main game arena and logic.
* `ScoreWindow.axaml`: High score display and data entry.
* `App.axaml`: Application styles and entry point configuration.
