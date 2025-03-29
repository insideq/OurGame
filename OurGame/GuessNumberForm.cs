namespace OurGame
{
    /// <summary>
    /// Игра угадай число
    /// </summary>
    public partial class GuessNumberForm : Form
    {
        // Добавляем событие для уведомления о победе
        public event Action OnWin;

        private int secretNumber;
        private int attemptsLeft;
        private Random random = new Random();

        // Элементы интерфейса
        private Label titleLabel;
        private Label promptLabel;
        private TextBox inputBox;
        private Button guessButton;
        private Label attemptsLabel;
        private Label historyLabel;

        public GuessNumberForm()
        {
            this.Text = "Угадай число";
            this.ClientSize = new Size(400, 500);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            SetupUI(); // Сначала инициализируем интерфейс
            InitializeGame(); // Затем инициализируем игру
        }

        private void InitializeGame()
        {
            secretNumber = random.Next(1, 101); // Число от 1 до 100
            attemptsLeft = 10;
            UpdateAttemptsLabel();
            historyLabel.Text = ""; // Очищаем историю
        }

        private void SetupUI()
        {
            // Заголовок
            titleLabel = new Label
            {
                Text = "Угадай число от 1 до 100",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            // Подсказка
            promptLabel = new Label
            {
                Text = "Введите вашу догадку:",
                Location = new Point(20, 60),
                AutoSize = true
            };
            this.Controls.Add(promptLabel);

            // Поле ввода
            inputBox = new TextBox
            {
                Location = new Point(20, 90),
                Size = new Size(100, 20),
                Font = new Font("Arial", 12)
            };
            inputBox.KeyPress += InputBox_KeyPress;
            this.Controls.Add(inputBox);

            // Кнопка
            guessButton = new Button
            {
                Text = "Проверить",
                Location = new Point(130, 88),
                Size = new Size(100, 30)
            };
            guessButton.Click += GuessButton_Click;
            this.Controls.Add(guessButton);

            // Осталось попыток
            attemptsLabel = new Label
            {
                Location = new Point(20, 130),
                AutoSize = true,
                Text = "Осталось попыток: 10" // Инициализируем текст
            };
            this.Controls.Add(attemptsLabel);

            // История попыток
            historyLabel = new Label
            {
                Location = new Point(20, 160),
                Size = new Size(360, 220),
                Font = new Font("Arial", 10)
            };
            this.Controls.Add(historyLabel);
        }

        private void InputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void GuessButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inputBox.Text))
            {
                MessageBox.Show("Введите число!");
                return;
            }

            if (!int.TryParse(inputBox.Text, out int guess))
            {
                MessageBox.Show("Введите корректное число!");
                inputBox.Text = "";
                return;
            }

            if (guess < 1 || guess > 100)
            {
                MessageBox.Show("Число должно быть от 1 до 100!");
                inputBox.Text = "";
                return;
            }

            ProcessGuess(guess);
        }

        private void ProcessGuess(int guess)
        {
            attemptsLeft--;
            UpdateAttemptsLabel();

            if (guess == secretNumber)
            {
                historyLabel.Text += $"✔ Угадал! Загаданное число: {secretNumber}\n";
                MessageBox.Show("Поздравляем! Вы угадали число!", "Победа!");

                // Вызываем событие победы
                OnWin?.Invoke();

                // Закрываем форму после победы
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (attemptsLeft <= 0)
            {
                historyLabel.Text += $"✖ Проиграл! Загаданное число: {secretNumber}\n";
                MessageBox.Show($"К сожалению, вы проиграли. Загаданное число: {secretNumber}", "Поражение");
                // Закрываем форму после поражения
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            string hint = guess < secretNumber ? "больше" : "меньше";
            historyLabel.Text += $"✎ {guess} (нужно {hint})\n";
            inputBox.Text = "";
            inputBox.Focus();
        }

        private void UpdateAttemptsLabel()
        {
            if (attemptsLabel != null) // Добавляем проверку на null
            {
                attemptsLabel.Text = $"Осталось попыток: {attemptsLeft}";
                attemptsLabel.ForeColor = attemptsLeft < 3 ? Color.Red : Color.Black;
            }
        }
    }
}