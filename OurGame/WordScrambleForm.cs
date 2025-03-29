namespace OurGame
{
    public partial class WordScrambleForm : Form
    {
        private string correctWord = "ВСПОМНИ"; // Правильное слово
        private List<Label> letterTiles = new List<Label>(); // Плитки с буквами
        private List<Label> dropZones = new List<Label>(); // Зоны для букв
        private List<Point> originalPositions = new List<Point>(); // Исходные позиции букв
        private Label draggedLabel; // Перетаскиваемая плитка

        public event EventHandler PuzzleSolved;

        public WordScrambleForm()
        {
            InitializeComponent();
            SetupGame();
        }

        private void SetupGame()
        {
            this.Text = "Собери слово";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Перемешиваем буквы
            List<char> letters = new List<char>(correctWord.ToCharArray());
            ShuffleLetters(letters);

            // Создаем плитки с буквами
            for (int i = 0; i < letters.Count; i++)
            {
                Label letterTile = new Label()
                {
                    Text = letters[i].ToString(),
                    Font = new Font("Arial", 16, FontStyle.Bold),
                    Size = new Size(40, 40),
                    Location = new Point(60 + i * 70, 50),
                    BackColor = Color.LightBlue,
                    ForeColor = Color.DarkBlue,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = i // Сохраняем исходный индекс
                };

                // Запоминаем исходные позиции
                originalPositions.Add(letterTile.Location);

                // Настройка перетаскивания
                letterTile.MouseDown += LetterTile_MouseDown;
                letterTile.AllowDrop = true;

                letterTiles.Add(letterTile);
                this.Controls.Add(letterTile);
            }

            // Создаем зоны для букв
            for (int i = 0; i < correctWord.Length; i++)
            {
                Label dropZone = new Label()
                {
                    Size = new Size(40, 40),
                    Location = new Point(60 + i * 70, 150),
                    BackColor = Color.LightGray,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AllowDrop = true
                };

                dropZone.DragEnter += DropZone_DragEnter;
                dropZone.DragDrop += DropZone_DragDrop;

                dropZones.Add(dropZone);
                this.Controls.Add(dropZone);
            }

            // Кнопка проверки
            Button checkButton = new Button()
            {
                Text = "Проверить",
                Size = new Size(120, 40),
                Location = new Point(240, 250),
                Font = new Font("Arial", 12)
            };
            checkButton.Click += CheckButton_Click;
            this.Controls.Add(checkButton);

            // Кнопка сброса
            Button resetButton = new Button()
            {
                Text = "Сбросить",
                Size = new Size(120, 40),
                Location = new Point(240, 300),
                Font = new Font("Arial", 12)
            };
            resetButton.Click += ResetButton_Click;
            this.Controls.Add(resetButton);
        }

        private void ShuffleLetters(List<char> letters)
        {
            Random rand = new Random();
            for (int i = letters.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                char temp = letters[i];
                letters[i] = letters[j];
                letters[j] = temp;
            }
        }

        private void LetterTile_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draggedLabel = (Label)sender;
                draggedLabel.DoDragDrop(draggedLabel, DragDropEffects.Move);
            }
        }

        private void DropZone_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(Label)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void DropZone_DragDrop(object sender, DragEventArgs e)
        {
            Label targetZone = (Label)sender;
            Label sourceTile = (Label)e.Data.GetData(typeof(Label));

            // Если в зоне уже есть буква, возвращаем ее обратно
            if (!string.IsNullOrEmpty(targetZone.Text))
            {
                ReturnTileToOriginalPosition(targetZone.Text);
            }

            // Помещаем новую букву в зону
            targetZone.Text = sourceTile.Text;
            targetZone.Tag = sourceTile; // Сохраняем ссылку на плитку
            sourceTile.Visible = false;
        }

        private void ReturnTileToOriginalPosition(string letter)
        {
            foreach (Label zone in dropZones)
            {
                if (zone.Text == letter)
                {
                    Label tile = (Label)zone.Tag;
                    tile.Visible = true;
                    zone.Text = string.Empty;
                    zone.Tag = null;
                    break;
                }
            }
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            string assembledWord = string.Empty;
            foreach (Label zone in dropZones)
            {
                assembledWord += zone.Text;
            }

            if (assembledWord == correctWord)
            {
                MessageBox.Show("Поздравляем! Слово собрано правильно!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                PuzzleSolved?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                MessageBox.Show("Слово собрано неправильно! Буквы возвращаются на место.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetLetters();
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetLetters();
        }

        private void ResetLetters()
        {
            // Возвращаем все буквы на исходные позиции
            foreach (Label zone in dropZones)
            {
                if (!string.IsNullOrEmpty(zone.Text))
                {
                    ReturnTileToOriginalPosition(zone.Text);
                }
            }

            // Восстанавливаем видимость всех плиток
            foreach (Label tile in letterTiles)
            {
                tile.Visible = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Очищаем ресурсы
            foreach (Label tile in letterTiles) tile.Dispose();
            foreach (Label zone in dropZones) zone.Dispose();
            base.OnFormClosing(e);
        }
    }
}