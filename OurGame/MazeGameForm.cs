namespace OurGame
{
    /// <summary>
    /// Игра лабиринт
    /// </summary>
    public partial class MazeGameForm: Form
    {
        private const int CellSize = 40;
        private const int MazeWidth = 15;
        private const int MazeHeight = 10;

        private int playerX = 1, playerY = 1; // Стартовая позиция
        private int treasureX, treasureY;
        private bool[,] walls;
        private int movesCount = 0;

        public event EventHandler PuzzleSolved;

        public MazeGameForm()
        {
            this.Text = "Лабиринт с сокровищами";
            this.ClientSize = new Size(MazeWidth * CellSize, MazeHeight * CellSize + 40);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += MazeGameForm_KeyDown;

            GenerateMaze();
            PlaceTreasure();
        }

        private void GenerateMaze()
        {
            walls = new bool[MazeWidth, MazeHeight];
            Random rnd = new Random();

            // Создаем случайные стены (25% клеток)
            for (int x = 0; x < MazeWidth; x++)
            {
                for (int y = 0; y < MazeHeight; y++)
                {
                    // Границы лабиринта всегда стены
                    if (x == 0 || y == 0 || x == MazeWidth - 1 || y == MazeHeight - 1)
                    {
                        walls[x, y] = true;
                    }
                    else
                    {
                        walls[x, y] = rnd.Next(4) == 0; // 25% chance
                    }
                }
            }

            // Гарантируем, что стартовая точка свободна
            walls[playerX, playerY] = false;
        }

        private void PlaceTreasure()
        {
            Random rnd = new Random();
            do
            {
                treasureX = rnd.Next(1, MazeWidth - 1);
                treasureY = rnd.Next(1, MazeHeight - 1);
            }
            while (walls[treasureX, treasureY] ||
                  (treasureX == playerX && treasureY == playerY));
        }

        private void MazeGameForm_KeyDown(object sender, KeyEventArgs e)
        {
            int newX = playerX;
            int newY = playerY;

            switch (e.KeyCode)
            {
                case Keys.Up: newY--; break;
                case Keys.Down: newY++; break;
                case Keys.Left: newX--; break;
                case Keys.Right: newX++; break;
                default: return;
            }

            // Проверка на выход за границы и стены
            if (newX >= 0 && newX < MazeWidth &&
                newY >= 0 && newY < MazeHeight &&
                !walls[newX, newY])
            {
                playerX = newX;
                playerY = newY;
                movesCount++;

                // Проверка на достижение сокровища
                if (playerX == treasureX && playerY == treasureY)
                {
                    PuzzleSolved?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show($"Вы нашли сокровище за {movesCount} ходов!");
                    this.Close();
                }

                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Рисуем стены
            for (int x = 0; x < MazeWidth; x++)
            {
                for (int y = 0; y < MazeHeight; y++)
                {
                    Rectangle rect = new Rectangle(
                        x * CellSize,
                        y * CellSize,
                        CellSize, CellSize);

                    if (walls[x, y])
                    {
                        g.FillRectangle(Brushes.DarkSlateGray, rect);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.LightGray, rect);
                    }

                    g.DrawRectangle(Pens.Black, rect);
                }
            }

            // Рисуем сокровище
            g.FillEllipse(Brushes.Gold,
                treasureX * CellSize + 5,
                treasureY * CellSize + 5,
                CellSize - 10, CellSize - 10);

            // Рисуем игрока
            g.FillEllipse(Brushes.Red,
                playerX * CellSize + 5,
                playerY * CellSize + 5,
                CellSize - 10, CellSize - 10);

            // Отображаем счетчик ходов
            g.DrawString($"Ходы: {movesCount}",
                new Font("Arial", 12),
                Brushes.Black, 10, MazeHeight * CellSize + 5);
        }
    }
}