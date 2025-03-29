using System.Drawing.Drawing2D;

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
        private Rectangle targetArea; // Область для сборки пазла
        private int pieceWidth, pieceHeight;

        public TruePuzzleGameForm()
        {
            this.Text = "Собери пазл";
            this.ClientSize = new Size(600, 500);
            this.DoubleBuffered = true;

            // Загрузка изображения (замените на свое)
            originalImage = new Bitmap("C:\\Users\\Имя\\Desktop\\OurGame\\OurGame\\Resources\\Puzzle.jpg");

            // Размеры кусочков
            pieceWidth = originalImage.Width / gridSize;
            pieceHeight = originalImage.Height / gridSize;

            // Область для сборки (верхний левый угол)
            targetArea = new Rectangle(0,0,
                originalImage.Width,
                originalImage.Height);

            InitializePuzzle();

            // Обработчики мыши
            this.MouseDown += Puzzle_MouseDown;
            this.MouseMove += Puzzle_MouseMove;
            this.MouseUp += Puzzle_MouseUp;
            this.Paint += Puzzle_Paint;
        }

        private void Puzzle_Paint(object sender, PaintEventArgs e)
        {
            // Очистка фона
            e.Graphics.Clear(Color.LightGray);

            // Рисуем целевую область в верхнем левом углу
            using (Pen dashPen = new Pen(Color.Blue, 2) { DashStyle = DashStyle.Dash })
            {
                e.Graphics.DrawRectangle(dashPen, targetArea);
            }

            // Надпись над целевой областью
            using (var font = new Font("Arial", 12, FontStyle.Bold))
            {
                e.Graphics.DrawString("Перетащите сюда кусочки",
                                    font, Brushes.Blue,
                                    targetArea.X, originalImage.Height + 15);
            }

            // Рисуем кусочки пазла
            foreach (var piece in pieces)
            {
                e.Graphics.DrawImage(piece.Image, piece.Bounds);

                // Рамка для выделения
                e.Graphics.DrawRectangle(
                    piece.IsCorrect ? Pens.Green : Pens.Black,
                    piece.Bounds);
            }
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