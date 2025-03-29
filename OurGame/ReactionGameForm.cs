namespace OurGame
{
    public partial class ReactionGameForm: Form
    {
        // Добавляем событие для уведомления о успешном прохождении
        public event EventHandler<bool> GameCompleted;

        private Random random = new Random();
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer spawnTimer;
        private int score = 0;
        private int timeLeft = 30;
        private const int MinClicksRequired = 15; // Минимум 20 кликов
        private Button target;
        private Label scoreLabel;
        private Label timeLabel;
        private Label instructionLabel;

        // Модифицированный конструктор для передачи ссылки на дверь
        public ReactionGameForm()
        {
            this.Text = "Игра на реакцию";
            this.ClientSize = new Size(600, 400);
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            SetupUI();
            SetupTimers();
        }

        private void SetupUI()
        {
            // Инструкция
            instructionLabel = new Label
            {
                Text = $"Соберите {MinClicksRequired} кликов за 30 секунд!",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 10),
                AutoSize = true
            };
            this.Controls.Add(instructionLabel);

            // Надпись счета
            scoreLabel = new Label
            {
                Text = "Счет: 0",
                Font = new Font("Arial", 14),
                Location = new Point(20, 50),
                AutoSize = true
            };
            this.Controls.Add(scoreLabel);

            // Надпись времени
            timeLabel = new Label
            {
                Text = "Время: 30",
                Font = new Font("Arial", 14),
                Location = new Point(20, 80),
                AutoSize = true
            };
            this.Controls.Add(timeLabel);

            // Мишень
            target = new Button
            {
                Size = new Size(60, 60),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Arial", 10),
                Text = "Клик!",
                Visible = false
            };
            target.Click += Target_Click;
            this.Controls.Add(target);
        }

        private void SetupTimers()
        {
            gameTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            spawnTimer = new System.Windows.Forms.Timer { Interval = 1500 };
            spawnTimer.Tick += SpawnTimer_Tick;
            spawnTimer.Start();
        }

        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            if (!target.Visible)
            {
                target.Location = new Point(
                    random.Next(20, this.ClientSize.Width - 80),
                    random.Next(100, this.ClientSize.Height - 80));

                target.BackColor = Color.FromArgb(
                    random.Next(150, 256),
                    random.Next(150, 256),
                    random.Next(150, 256));

                target.Visible = true;

                if (spawnTimer.Interval > 500)
                    spawnTimer.Interval -= 50;
            }
        }

        private void Target_Click(object sender, EventArgs e)
        {
            score++;
            scoreLabel.Text = $"Счет: {score}";
            target.Visible = false;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            timeLabel.Text = $"Время: {timeLeft}";

            if (timeLeft <= 0)
            {
                gameTimer.Stop();
                spawnTimer.Stop();
                target.Visible = false;

                bool isSuccess = score >= MinClicksRequired;

                if (isSuccess)
                {
                    MessageBox.Show($"Успех! Вы набрали {score} кликов!", "Победа");
                }
                else
                {
                    var result = MessageBox.Show(
                        $"Только {score} кликов. Нужно {MinClicksRequired}. Попробовать снова?",
                        "Неудача",
                        MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        // Перезапуск игры
                        score = 0;
                        timeLeft = 30;
                        spawnTimer.Interval = 1500;
                        scoreLabel.Text = "Счет: 0";
                        timeLabel.Text = "Время: 30";
                        gameTimer.Start();
                        spawnTimer.Start();
                        return;
                    }
                }

                // Вызываем событие с результатом игры
                GameCompleted?.Invoke(this, isSuccess);
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            gameTimer?.Stop();
            spawnTimer?.Stop();
            base.OnFormClosing(e);
        }
    }
}