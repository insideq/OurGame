namespace OurGame
{
    public partial class PuzzleForm : Form
    {
        private const int GridSize = 4; // 4x4 grid (16 tiles, 8 pairs)
        private const int TileSize = 100;
        private const int Margin = 10;

        private List<Color> tileColors;
        private List<bool> tileRevealed;
        private List<bool> tileMatched;
        private int firstSelected = -1;
        private int secondSelected = -1;
        private int pairsFound;
        private bool canClick = true;
        private System.Windows.Forms.Timer flipTimer;

        public event EventHandler PuzzleSolved; // Событие решения головоломки

        public PuzzleForm()
        {
            this.Text = "Пазл на память: Найди пары";
            this.ClientSize = new Size(
                GridSize * (TileSize + Margin) + Margin,
                GridSize * (TileSize + Margin) + Margin + 40);
            this.DoubleBuffered = true;
            this.Paint += MemoryPuzzleForm_Paint;
            this.MouseClick += MemoryPuzzleForm_MouseClick;

            InitializeGame();
        }

        private void InitializeGame()
        {
            // Создаем 8 пар цветов
            tileColors = new List<Color>();
            for (int i = 0; i < GridSize * GridSize / 2; i++)
            {
                Color color = GenerateRandomColor();
                tileColors.Add(color);
                tileColors.Add(color); // Добавляем пару
            }

            // Перемешиваем цвета
            ShuffleColors();

            tileRevealed = new List<bool>(new bool[GridSize * GridSize]);
            tileMatched = new List<bool>(new bool[GridSize * GridSize]);
            pairsFound = 0;
            firstSelected = -1;
            secondSelected = -1;

            // Таймер для скрытия непарных плиток
            flipTimer = new System.Windows.Forms.Timer();
            flipTimer.Interval = 1000; // 1 секунда
            flipTimer.Tick += FlipTimer_Tick;
        }

        private Color GenerateRandomColor()
        {
            Random rand = new Random();
            return Color.FromArgb(
                rand.Next(50, 200),
                rand.Next(50, 200),
                rand.Next(50, 200));
        }

        private void ShuffleColors()
        {
            Random rand = new Random();
            for (int i = tileColors.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                Color temp = tileColors[i];
                tileColors[i] = tileColors[j];
                tileColors[j] = temp;
            }
        }

        private void FlipTimer_Tick(object sender, EventArgs e)
        {
            // Скрываем непарные плитки
            tileRevealed[firstSelected] = false;
            tileRevealed[secondSelected] = false;
            firstSelected = -1;
            secondSelected = -1;
            canClick = true;
            flipTimer.Stop();
            this.Invalidate();
        }

        private void MemoryPuzzleForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            // Рисуем плитки
            for (int i = 0; i < GridSize * GridSize; i++)
            {
                int row = i / GridSize;
                int col = i % GridSize;
                int x = Margin + col * (TileSize + Margin);
                int y = Margin + row * (TileSize + Margin);

                if (tileMatched[i])
                {
                    // Уже совпавшие плитки
                    g.FillRectangle(Brushes.LightGray, x, y, TileSize, TileSize);
                    g.DrawString("✓", new Font("Arial", 24), Brushes.Green, x + TileSize / 2 - 10, y + TileSize / 2 - 15);
                }
                else if (tileRevealed[i])
                {
                    // Открытые плитки
                    using (Brush brush = new SolidBrush(tileColors[i]))
                    {
                        g.FillRectangle(brush, x, y, TileSize, TileSize);
                    }
                }
                else
                {
                    // Закрытые плитки
                    g.FillRectangle(Brushes.SteelBlue, x, y, TileSize, TileSize);
                }

                // Рамка
                g.DrawRectangle(Pens.Black, x, y, TileSize, TileSize);
            }

            // Отображаем счет
            using (Font font = new Font("Arial", 14))
            {
                g.DrawString($"Найдено пар: {pairsFound} из {GridSize * GridSize / 2}",
                            font, Brushes.Black, Margin, GridSize * (TileSize + Margin) + Margin);
            }

            // Сообщение о победе
            if (pairsFound == GridSize * GridSize / 2)
            {
                using (Font font = new Font("Arial", 18, FontStyle.Bold))
                {
                    string text = "Поздравляем! \nВы нашли все пары!";
                    SizeF size = g.MeasureString(text, font);
                    g.DrawString(text, font, Brushes.Green,
                        (this.ClientSize.Width - size.Width) / 2,
                        (this.ClientSize.Height - size.Height) / 2);
                }
            }

            if (pairsFound == GridSize * GridSize / 2)
            {
                PuzzleSolved?.Invoke(this, EventArgs.Empty);
            }
        }

        private void MemoryPuzzleForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!canClick || pairsFound == GridSize * GridSize / 2) return;

            // Определяем, по какой плитке кликнули
            int col = (e.X - Margin) / (TileSize + Margin);
            int row = (e.Y - Margin) / (TileSize + Margin);

            if (col >= 0 && col < GridSize && row >= 0 && row < GridSize)
            {
                int index = row * GridSize + col;

                // Игнорируем уже открытые или совпавшие плитки
                if (tileRevealed[index] || tileMatched[index]) return;

                // Первая выбранная плитка
                if (firstSelected == -1)
                {
                    firstSelected = index;
                    tileRevealed[index] = true;
                }
                // Вторая выбранная плитка (не та же самая)
                else if (secondSelected == -1 && index != firstSelected)
                {
                    secondSelected = index;
                    tileRevealed[index] = true;
                    canClick = false;

                    // Проверяем совпадение
                    if (tileColors[firstSelected] == tileColors[secondSelected])
                    {
                        tileMatched[firstSelected] = true;
                        tileMatched[secondSelected] = true;
                        pairsFound++;
                        firstSelected = -1;
                        secondSelected = -1;
                        canClick = true;
                    }
                    else
                    {
                        // Запускаем таймер для скрытия плиток
                        flipTimer.Start();
                    }
                }

                this.Invalidate();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            flipTimer?.Stop();
            base.OnFormClosing(e);
        }

        public static void RunGame()
        {
            Application.Run(new PuzzleForm());
        }
    }
}