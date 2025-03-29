using System.Drawing.Drawing2D;

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

        private int playerX = 1, playerY = 1;
        private int keyX, keyY;
        private bool[,] walls;
        private int movesCount = 0;

        public event EventHandler PuzzleSolved;

        public MazeGameForm()
        {
            this.Text = "Найди ключ!";
            this.ClientSize = new Size(MazeWidth * CellSize, MazeHeight * CellSize + 40);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += MazeGameForm_KeyDown;

            GenerateMaze();
            PlaceKey();
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

        private void PlaceKey()
        {
            Random rnd = new Random();
            do
            {
                keyX = rnd.Next(1, MazeWidth - 1);
                keyY = rnd.Next(1, MazeHeight - 1);
            }
            while (walls[keyX, keyY] || (keyX == playerX && keyY == playerY));
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

            if (CanMoveTo(newX, newY))
            {
                playerX = newX;
                playerY = newY;
                movesCount++;

                // Проверка на достижение ключа
                if (playerX == keyX && playerY == keyY)
                {
                    PuzzleSolved?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show($"Вы нашли ключ за {movesCount} ходов!");
                    this.Close();
                }

                this.Invalidate();
            }
        }

        // Проверка на выход за границы и стены
        private bool CanMoveTo(int x, int y)
        {
            return x >= 0 && x < MazeWidth &&
                   y >= 0 && y < MazeHeight &&
                   !walls[x, y];
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Рисуем лабиринт
            DrawMaze(g);

            // Рисуем ключ
            DrawKey(g, keyX * CellSize + CellSize / 2, keyY * CellSize + CellSize / 2);

            // Рисуем человечка
            DrawCharacter(g, playerX * CellSize + CellSize / 2, playerY * CellSize + CellSize / 2);

            // Счетчик ходов
            g.DrawString($"Ходы: {movesCount}", new Font("Arial", 12), Brushes.Black, 10, MazeHeight * CellSize + 5);
        }

        private void DrawMaze(Graphics g)
        {
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
                        using (var brush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.SlateGray, Color.DarkSlateGray))
                        {
                            g.FillRectangle(brush, rect);
                        }
                    }
                    else
                    {
                        g.FillRectangle(Brushes.LightGray, rect);
                    }

                    g.DrawRectangle(Pens.Black, rect);
                }
            }
        }

        private void DrawKey(Graphics g, int centerX, int centerY)
        {
            // Головка ключа
            g.FillEllipse(Brushes.Gold, centerX - 20, centerY - 10, 20, 20);
            g.DrawEllipse(new Pen(Color.DarkGoldenrod, 2), centerX - 20, centerY - 10, 20, 20);

            // Зубчики
            Point[] teeth = {
                new Point(centerX, centerY - 5),
                new Point(centerX + 5, centerY - 3),
                new Point(centerX, centerY),
                new Point(centerX + 5, centerY + 3),
                new Point(centerX, centerY + 5)
            };
            g.FillPolygon(Brushes.Gold, teeth);
            g.DrawPolygon(new Pen(Color.DarkGoldenrod, 2), teeth);

            // Стержень ключа
            g.FillRectangle(Brushes.Goldenrod, centerX, centerY - 3, 15, 6);
            g.DrawRectangle(new Pen(Color.DarkGoldenrod, 2), centerX, centerY - 3, 15, 6);
        }

        private void DrawCharacter(Graphics g, int centerX, int centerY)
        {
            // Голова
            g.FillEllipse(Brushes.LightSkyBlue, centerX - 10, centerY - 20, 20, 20);
            g.DrawEllipse(Pens.Black, centerX - 10, centerY - 20, 20, 20);

            // Глаза
            g.FillEllipse(Brushes.White, centerX - 5, centerY - 15, 5, 5);
            g.FillEllipse(Brushes.White, centerX + 2, centerY - 15, 5, 5);
            g.FillEllipse(Brushes.Black, centerX - 3, centerY - 14, 2, 2);
            g.FillEllipse(Brushes.Black, centerX + 4, centerY - 14, 2, 2);

            // Тело
            g.FillRectangle(Brushes.RoyalBlue, centerX - 8, centerY, 16, 15);
            g.DrawRectangle(Pens.Black, centerX - 8, centerY, 16, 15);

            // Ноги
            g.FillRectangle(Brushes.DarkBlue, centerX - 8, centerY + 15, 6, 5);
            g.FillRectangle(Brushes.DarkBlue, centerX + 2, centerY + 15, 6, 5);
        }
    }
}