namespace OurGame
{
    public partial class TruePuzzleGameForm: Form
    {
        public event Action PuzzleCompleted; // Событие при завершении

        private Bitmap originalImage;
        private List<PuzzlePiece> pieces = new List<PuzzlePiece>();
        private PuzzlePiece selectedPiece = null;
        private Point offset;
        private int gridSize = 3; // 3x3 grid

        public TruePuzzleGameForm()
        {
            this.Text = "Собери пазл";
            this.ClientSize = new Size(600, 600);
            this.DoubleBuffered = true;

            // Загрузка изображения (замените на свое)
            originalImage = new Bitmap("C:\\Users\\Имя\\Desktop\\OurGame\\OurGame\\Resources\\Puzzle.jpg");
            InitializePuzzle();

            // Обработчики мыши
            this.MouseDown += Puzzle_MouseDown;
            this.MouseMove += Puzzle_MouseMove;
            this.MouseUp += Puzzle_MouseUp;
        }

        private void InitializePuzzle()
        {
            pieces.Clear();
            int pieceWidth = originalImage.Width / gridSize;
            int pieceHeight = originalImage.Height / gridSize;

            // Создаем перемешанные кусочки
            Random rand = new Random();
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    Rectangle sourceRect = new Rectangle(
                        x * pieceWidth, y * pieceHeight,
                        pieceWidth, pieceHeight);

                    Bitmap pieceImage = originalImage.Clone(
                        sourceRect, originalImage.PixelFormat);

                    // Случайная позиция для перемешивания
                    Point randomPos = new Point(
                        rand.Next(0, this.ClientSize.Width - pieceWidth),
                        rand.Next(0, this.ClientSize.Height - pieceHeight));

                    pieces.Add(new PuzzlePiece(
                        pieceImage,
                        randomPos,
                        new Point(x, y)));
                }
            }
        }

        private void Puzzle_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = pieces.Count - 1; i >= 0; i--)
            {
                if (pieces[i].Bounds.Contains(e.Location))
                {
                    selectedPiece = pieces[i];
                    offset = new Point(
                        e.X - selectedPiece.Position.X,
                        e.Y - selectedPiece.Position.Y);
                    pieces.RemoveAt(i);
                    pieces.Add(selectedPiece); // Перемещаем на верхний слой
                    break;
                }
            }
        }

        private void Puzzle_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedPiece != null)
            {
                selectedPiece.Position = new Point(
                    e.X - offset.X,
                    e.Y - offset.Y);
                this.Invalidate();
            }
        }

        private void Puzzle_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectedPiece != null)
            {
                // Проверяем, правильно ли размещен кусочек
                int pieceWidth = originalImage.Width / gridSize;
                int pieceHeight = originalImage.Height / gridSize;

                Point targetPos = new Point(
                    selectedPiece.GridPosition.X * pieceWidth,
                    selectedPiece.GridPosition.Y * pieceHeight);

                // Если кусочек близко к правильной позиции
                if (Math.Abs(selectedPiece.Position.X - targetPos.X) < 20 &&
                    Math.Abs(selectedPiece.Position.Y - targetPos.Y) < 20)
                {
                    selectedPiece.Position = targetPos;
                    selectedPiece.IsCorrect = true;
                }

                selectedPiece = null;
                this.Invalidate();

                // Проверяем завершение
                CheckCompletion();
            }
        }

        private void CheckCompletion()
        {
            foreach (var piece in pieces)
            {
                if (!piece.IsCorrect) return;
            }

            MessageBox.Show("Пазл собран! Дверь открыта!", "Успех");
            PuzzleCompleted?.Invoke();
            this.Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Рисуем все кусочки
            foreach (var piece in pieces)
            {
                e.Graphics.DrawImage(piece.Image, piece.Bounds);

                // Рамка для выделения
                e.Graphics.DrawRectangle(
                    piece.IsCorrect ? Pens.Green : Pens.Black,
                    piece.Bounds);
            }
        }

        private class PuzzlePiece
        {
            public Bitmap Image { get; }
            public Point Position { get; set; }
            public Point GridPosition { get; }
            public bool IsCorrect { get; set; }

            public Rectangle Bounds => new Rectangle(
                Position.X, Position.Y,
                Image.Width, Image.Height);

            public PuzzlePiece(Bitmap image, Point position, Point gridPosition)
            {
                Image = image;
                Position = position;
                GridPosition = gridPosition;
            }
        }
    }
}