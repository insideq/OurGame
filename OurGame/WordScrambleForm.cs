namespace OurGame
{
    public partial class WordScrambleForm: Form
    {
        private string correctWord = "ВСПОМНИ"; // Правильное слово
        private List<Label> letterTiles; // Плитки с буквами
        private List<Label> dropZones; // Зоны для перетаскивания букв
        private Label draggedLabel; // Текущая перетаскиваемая буква

        public event EventHandler PuzzleSolved; // Событие, которое сработает при решении головоломки

        public WordScrambleForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Собери слово";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            letterTiles = new List<Label>();
            dropZones = new List<Label>();

            // Перемешиваем буквы
            List<char> letters = new List<char>(correctWord.ToCharArray());
            Random rand = new Random();
            for (int i = letters.Count - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                char temp = letters[i];
                letters[i] = letters[j];
                letters[j] = temp;
            }

            // Создаём плитки с буквами
            for (int i = 0; i < letters.Count; i++)
            {
                Label letterTile = new Label
                {
                    Text = letters[i].ToString(),
                    Font = new Font("Arial", 16, FontStyle.Bold),
                    Size = new Size(40, 40),
                    Location = new Point(50 + i * 50, 50),
                    BackColor = Color.LightBlue,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = letters[i].ToString() // Сохраняем букву в Tag
                };

                // Настраиваем события для перетаскивания
                letterTile.MouseDown += LetterTile_MouseDown;
                letterTile.DragEnter += LetterTile_DragEnter;
                letterTile.DragDrop += LetterTile_DragDrop;
                letterTile.AllowDrop = true;

                letterTiles.Add(letterTile);
                this.Controls.Add(letterTile);
            }

            // Создаём зоны для перетаскивания (drop zones)
            for (int i = 0; i < correctWord.Length; i++)
            {
                Label dropZone = new Label
                {
                    Size = new Size(40, 40),
                    Location = new Point(50 + i * 50, 150),
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

            // Кнопка "Проверить"
            Button checkButton = new Button
            {
                Text = "Проверить",
                Size = new Size(100, 40),
                Location = new Point(250, 250),
                Font = new Font("Arial", 12)
            };
            checkButton.Click += CheckButton_Click;
            this.Controls.Add(checkButton);
        }

        private void LetterTile_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draggedLabel = sender as Label;
                if (draggedLabel != null)
                {
                    draggedLabel.DoDragDrop(draggedLabel, DragDropEffects.Move);
                }
            }
        }

        private void LetterTile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Label)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void LetterTile_DragDrop(object sender, DragEventArgs e)
        {
            Label targetTile = sender as Label;
            Label sourceTile = e.Data.GetData(typeof(Label)) as Label;

            if (targetTile != null && sourceTile != null && targetTile != sourceTile)
            {
                // Меняем местами текст и позиции
                string tempText = targetTile.Text;
                Point tempLocation = targetTile.Location;

                targetTile.Text = sourceTile.Text;
                targetTile.Location = sourceTile.Location;

                sourceTile.Text = tempText;
                sourceTile.Location = tempLocation;
            }
        }

        private void DropZone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Label)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void DropZone_DragDrop(object sender, DragEventArgs e)
        {
            Label dropZone = sender as Label;
            Label sourceTile = e.Data.GetData(typeof(Label)) as Label;

            if (dropZone != null && sourceTile != null)
            {
                // Если в зоне уже есть буква, возвращаем её на исходное место
                if (!string.IsNullOrEmpty(dropZone.Text))
                {
                    foreach (var tile in letterTiles)
                    {
                        if (tile.Text == dropZone.Text && tile.Location.Y == 50)
                        {
                            tile.Text = dropZone.Text;
                            break;
                        }
                    }
                }

                // Перемещаем букву в зону
                dropZone.Text = sourceTile.Text;
                dropZone.BackColor = Color.LightGreen;
                sourceTile.Text = string.Empty;
            }
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            // Собираем слово из зон
            string assembledWord = string.Empty;
            foreach (var dropZone in dropZones)
            {
                assembledWord += dropZone.Text;
            }

            if (assembledWord == correctWord)
            {
                MessageBox.Show("Поздравляем! Вы собрали слово!", "Победа!");
                PuzzleSolved?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильно. Попробуйте ещё раз!", "Ошибка");
            }
        }
    }
}