    using Avalonia.Controls;
    using Avalonia.Controls.Shapes;
    using Avalonia.Input;
    using Avalonia.Media;
    using Avalonia.Threading;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace SnakeEater;

    public partial class GameWindow : Window
    {
        public enum Direction { Up, Down, Left, Right }
        public record Cell (int X, int Y);

        private int _score = 0;
        private const int GridSize = 20;
        private List<Cell> _snake = new();
        private Cell _food = new(0, 0);
        private Direction _currentDirection = Direction.Right;
        private DispatcherTimer _timer;
        private Random _random = new();
        private bool _moveFlag;

        public GameWindow()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(150) };
            _timer.Tick += (s, e) => Move();

            InitializeComponent();
            UpdateScore();
            StartGame();
        }
        private async void GameOver()
        {
            var scoreWindow = new ScoreWindow(_score, this);
            await scoreWindow.ShowDialog(this);
            StartGame();
        }
        private void resetValues()
        {
            _moveFlag = true;
            _currentDirection = Direction.Right;
            _score = 0;
            UpdateScore();
        }
        private void AddScore()
        {
            _score++;
            UpdateScore();
        }
        private void UpdateScore() 
        {
            ScoreCount.Text = $"Score: {_score}";
        }
        private void StartGame()
        {
            resetValues();
            _snake = new List<Cell> { new Cell(5, 5), new Cell(4, 5), new Cell(3, 5) };
            SpawnFood();
            _timer.Start();
        }

    private void SpawnFood()
    {
        int maxX = (int)(GameCanvas.Width / GridSize);
        int maxY = (int)(GameCanvas.Height / GridSize);
        _food = new Cell(_random.Next(0, maxX), _random.Next(0, maxY));
        while (_snake.Contains(_food))
        {
            _food = new Cell(_random.Next(0, maxX), _random.Next(0, maxY));
        };
    }

        private void Move()
        {
            _moveFlag = true;
            var head = _snake.First();
            var newHead = _currentDirection switch
            {
                Direction.Up => new Cell(head.X, head.Y - 1),
                Direction.Down => new Cell(head.X, head.Y + 1),
                Direction.Left => new Cell(head.X - 1, head.Y),
                Direction.Right => new Cell(head.X + 1, head.Y),
                _ => head
            };


            if (newHead.X < 0 || newHead.Y < 0 || newHead.X >= GameCanvas.Width / GridSize || newHead.Y >= GameCanvas.Height / GridSize || _snake.Contains(newHead))
            {
                _timer.Stop();
                GameOver();
                return;
            }

            _snake.Insert(0, newHead);

            if (newHead == _food)
            {
                AddScore();
                SpawnFood();
            }
            else
                _snake.RemoveAt(_snake.Count - 1);

            Draw();
        }

        private void Draw()
        {
            GameCanvas.Children.Clear();


            var foodRect = new Rectangle { Width = GridSize, Height = GridSize, Fill = Brushes.Red };
            Canvas.SetLeft(foodRect, _food.X * GridSize);
            Canvas.SetTop(foodRect, _food.Y * GridSize);
            GameCanvas.Children.Add(foodRect);


            foreach (var pos in _snake)
            {
                var segment = new Rectangle { Width = GridSize - 1, Height = GridSize - 1, Fill = Brushes.Green };
                Canvas.SetLeft(segment, pos.X * GridSize);
                Canvas.SetTop(segment, pos.Y * GridSize);
                GameCanvas.Children.Add(segment);
            }
        }


        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (_moveFlag) {
                _currentDirection = e.Key switch
                {
                    Key.W or Key.Up when _currentDirection != Direction.Down => Direction.Up,
                    Key.S or Key.Down when _currentDirection != Direction.Up => Direction.Down,
                    Key.A or Key.Left when _currentDirection != Direction.Right => Direction.Left,
                    Key.D or Key.Right when _currentDirection != Direction.Left => Direction.Right,
                    _ => _currentDirection
                };
            }
            _moveFlag = false;
        }
    }