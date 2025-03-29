using System.Drawing.Drawing2D;

namespace OurGame
{
    /// <summary>
    /// Пятнашки
    /// </summary>
    public partial class SlidingPuzzleForm: Form
    {
        private int[,] grid;
        private int emptyX, emptyY;
        private int puzzleSize = 4;
        private int tileSize = 100;
        private bool isSolved;
        private System.Windows.Forms.Timer timer;
        private int moveCount;

        public SlidingPuzzleForm()
        {
            this.Text = "Пятнашки";
            this.ClientSize = new Size(puzzleSize * tileSize + 20, puzzleSize * tileSize + 70);
            this.DoubleBuffered = true;
            this.Paint += SlidingPuzzleForm_Paint;
            this.MouseClick += SlidingPuzzleForm_MouseClick;

            InitializePuzzle();

            // Таймер для анимации
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 16; // ~60 FPS
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializePuzzle()
        {
            grid = new int[puzzleSize, puzzleSize];

            // Заполняем сетку числами
            int num = 1;
            for (int y = 0; y < puzzleSize; y++)
            {
                for (int x = 0; x < puzzleSize; x++)
                {
                    grid[y, x] = (num < puzzleSize * puzzleSize) ? num++ : 0;
                }
            }

            emptyX = puzzleSize - 1;
            emptyY = puzzleSize - 1;

            // Перемешиваем
            ShufflePuzzle(100);
            moveCount = 0;
            isSolved = false;
        }

        private void ShufflePuzzle(int moves)
        {
            Random rand = new Random();
            for (int i = 0; i < moves; i++)
            {
                List<Point> possibleMoves = GetPossibleMoves();
                if (possibleMoves.Count > 0)
                {
                    Point move = possibleMoves[rand.Next(possibleMoves.Count)];
                    SwapTiles(move.X, move.Y, emptyX, emptyY);
                    emptyX = move.X;
                    emptyY = move.Y;
                }
            }
        }

        private List<Point> GetPossibleMoves()
        {
            List<Point> moves = new List<Point>();
            if (emptyX > 0) moves.Add(new Point(emptyX - 1, emptyY));
            if (emptyX < puzzleSize - 1) moves.Add(new Point(emptyX + 1, emptyY));
            if (emptyY > 0) moves.Add(new Point(emptyX, emptyY - 1));
            if (emptyY < puzzleSize - 1) moves.Add(new Point(emptyX, emptyY + 1));
            return moves;
        }

        private void SwapTiles(int x1, int y1, int x2, int y2)
        {
            int temp = grid[y1, x1];
            grid[y1, x1] = grid[y2, x2];
            grid[y2, x2] = temp;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isSolved && IsPuzzleSolved())
            {
                isSolved = true;
                MessageBox.Show($"Поздравляем! Вы решили головоломку за {moveCount} ходов.", "Победа!");
            }
            this.Invalidate();
        }

        private bool IsPuzzleSolved()
        {
            int num = 1;
            for (int y = 0; y < puzzleSize; y++)
            {
                for (int x = 0; x < puzzleSize; x++)
                {
                    if (y == puzzleSize - 1 && x == puzzleSize - 1)
                    {
                        if (grid[y, x] != 0) return false;
                    }
                    else if (grid[y, x] != num++) return false;
                }
            }
            return true;
        }

        private void SlidingPuzzleForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            int startX = (this.ClientSize.Width - puzzleSize * tileSize) / 2;
            int startY = 20;

            // Рисуем плитки
            for (int y = 0; y < puzzleSize; y++)
            {
                for (int x = 0; x < puzzleSize; x++)
                {
                    Rectangle rect = new Rectangle(
                        startX + x * tileSize,
                        startY + y * tileSize,
                        tileSize - 2,
                        tileSize - 2);

                    if (grid[y, x] != 0) // Не рисуем пустую плитку
                    {
                        // Градиентная заливка
                        using (var brush = new LinearGradientBrush(
                            rect,
                            Color.LightBlue,
                            Color.DodgerBlue,
                            LinearGradientMode.ForwardDiagonal))
                        {
                            g.FillRectangle(brush, rect);
                        }

                        // Текст номера
                        using (Font font = new Font("Arial", 24))
                        {
                            SizeF textSize = g.MeasureString(grid[y, x].ToString(), font);
                            g.DrawString(
                                grid[y, x].ToString(),
                                font,
                                Brushes.White,
                                rect.X + (rect.Width - textSize.Width) / 2,
                                rect.Y + (rect.Height - textSize.Height) / 2);
                        }

                        // Рамка
                        g.DrawRectangle(Pens.DarkBlue, rect);
                    }
                }
            }

            // Отображаем счетчик ходов
            using (Font font = new Font("Arial", 12))
            {
                g.DrawString($"Ходы: {moveCount}", font, Brushes.Black, 10, 10);
            }

            if (isSolved)
            {
                using (Font font = new Font("Arial", 16))
                {
                    string text = "Головоломка решена!";
                    SizeF textSize = g.MeasureString(text, font);
                    g.DrawString(
                        text,
                        font,
                        Brushes.Green,
                        (this.ClientSize.Width - textSize.Width) / 2,
                        startY + puzzleSize * tileSize + 10);
                }
            }
        }

        private void SlidingPuzzleForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (isSolved) return;

            int startX = (this.ClientSize.Width - puzzleSize * tileSize) / 2;
            int startY = 20;

            int clickedX = (e.X - startX) / tileSize;
            int clickedY = (e.Y - startY) / tileSize;

            if (clickedX >= 0 && clickedX < puzzleSize &&
                clickedY >= 0 && clickedY < puzzleSize)
            {
                // Проверяем, можно ли передвинуть эту плитку
                if ((Math.Abs(clickedX - emptyX) == 1 && clickedY == emptyY) ||
                    (Math.Abs(clickedY - emptyY) == 1 && clickedX == emptyX))
                {
                    SwapTiles(clickedX, clickedY, emptyX, emptyY);
                    emptyX = clickedX;
                    emptyY = clickedY;
                    moveCount++;
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timer.Stop();
            base.OnFormClosing(e);
        }

        public static void RunGame()
        {
            Application.Run(new SlidingPuzzleForm());
        }
    }
}
