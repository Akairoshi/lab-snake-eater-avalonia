using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using HarfBuzzSharp;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SnakeEater;

public partial class ScoreWindow : Window
{
    private int _score;
    private GameWindow _parentWindow;
    private readonly string _filesDir = Path.Combine(AppContext.BaseDirectory, "bin");
    private readonly string _fileName = "scoreLeaders";
    private string _fullPath;

    public ScoreWindow()
    {
        InitializeComponent();
    }
    public ScoreWindow(int _score, GameWindow window) : this()
    {
        _parentWindow = window;
        _fullPath = Path.Combine(_filesDir, _fileName);
        this._score = _score;

        if (!Directory.Exists(_filesDir))
        {
            Directory.CreateDirectory(_filesDir);
        }
        if (!File.Exists(_fullPath))
        {
            File.CreateText(_fullPath).Close();
        }
        score_TextBlock.Text = $"Your score - {_score}";
        ReadScores();
    }
    private void ReadScores()
    {
        Score_TextBlock.Text = File.ReadAllText(_fullPath, Encoding.UTF8);
    }
    private void Save(object? sender, RoutedEventArgs e)
    {
        string playerName = string.IsNullOrWhiteSpace(name_TextBox.Text) ? "Anonymous" : name_TextBox.Text;
        string record = $"{playerName} | scores = {_score}{Environment.NewLine}";
        name_TextBox.IsEnabled = false;
        Save_btn.IsEnabled = false;
        File.AppendAllText(_fullPath, record, Encoding.UTF8);
        ReadScores();
    }
    private void Restart(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
    private void ExitGame(object? sender, RoutedEventArgs e)
    {
        _parentWindow.Close();
        this.Close();
    }
}