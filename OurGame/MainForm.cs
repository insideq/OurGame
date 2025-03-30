using System.Media;

namespace OurGame
{
    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class MainForm : Form
    {
        private SoundPlayer backgroundMusic; // Музыка на фон
        private SoundPlayer openDoor;

        private float floatYPosition; // Текущая Y-координата для плавного движения
        private float floatSpeed = 0.2f; // Скорость "парения"
        private float floatAmplitude = 5f; // Высота "подпрыгивания"
        private int originalY; // Изначальная позиция кнопки
        private bool doorPuzzleSolved = false; // Флаг решения головоломки

        // Таймер для анимации исчезновения дверей
        private System.Windows.Forms.Timer doorFadeTimer;
        private Control currentFadingDoor;
        private float doorOpacity = 1.0f;

        public MainForm()
        {
            InitializeComponent();

            originalY = MainCharacter.Top; // Запоминаем стартовую позицию
            floatYPosition = 0;

            // Таймер для анимации персонажа (обновляется каждые 20 мс)
            System.Windows.Forms.Timer floatTimer = new System.Windows.Forms.Timer();
            floatTimer.Interval = 20;
            floatTimer.Tick += FloatAnimation_Tick;
            floatTimer.Start();

            // Инициализация таймера для анимации дверей
            doorFadeTimer = new System.Windows.Forms.Timer();
            doorFadeTimer.Interval = 30; // Интервал в миллисекундах
            doorFadeTimer.Tick += DoorFadeAnimation_Tick;

            // Загрузка музыки из ресурсов
            //backgroundMusic = new SoundPlayer(Properties.Resources.MORGENSHTERN_ДУЛО);

            // Или из файла по пути
            //backgroundMusic = new SoundPlayer(@"C:\Users\Имя\Desktop\OurGame\OurGame\Resources\MORGENSHTERN-ДУЛО.wav");

            //backgroundMusic.PlayLooping();
        }

        // Анимация исчезновения двери
        private void DoorFadeAnimation_Tick(object sender, EventArgs e)
        {
            if (currentFadingDoor != null)
            {
                doorOpacity -= 0.2f;

                if (doorOpacity <= 0)
                {
                    currentFadingDoor.Visible = false;
                    currentFadingDoor = null;
                    doorFadeTimer.Stop();
                    return;
                }

                // Применяем прозрачность
                currentFadingDoor.BackColor = Color.FromArgb((int)(doorOpacity * 255),
                    currentFadingDoor.BackColor);

                // Уменьшаем размер
                float scale = 0.9f; // Коэффициент уменьшения
                currentFadingDoor.Width = (int)(currentFadingDoor.Width * scale);
                currentFadingDoor.Height = (int)(currentFadingDoor.Height * scale);
                currentFadingDoor.Left += (int)(currentFadingDoor.Width * (1 - scale) / 2);
                currentFadingDoor.Top += (int)(currentFadingDoor.Height * (1 - scale) / 2);
            }
        }

        // Запуск анимации исчезновения двери
        private void StartDoorFade(Control door)
        {
            currentFadingDoor = door;
            doorOpacity = 1.0f;
            doorFadeTimer.Start();
        }

        /// <summary>
        /// Дверь, которая шлёт лесом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorForest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Поставьте пять..."); 
            StartDoorFade(DoorForest);
            MessageBox.Show("Дверь исчезла...");
            MainCharacterText.Text = "ААА я посинел...";
            MainCharacterText.Visible = true;
            MainCharacter.BackgroundImage = Properties.Resources.перс_синий;
        }

        /// <summary>
        /// Освобождаем ресурсы при закрытии формы
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            backgroundMusic?.Stop();
            backgroundMusic?.Dispose();
        }

        /// <summary>
        /// Анимация персонажа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FloatAnimation_Tick(object sender, EventArgs e)
        {
            floatYPosition += floatSpeed;

            // Синусоидальное движение для плавности
            float newY = originalY + (float)(Math.Sin(floatYPosition) * floatAmplitude);

            // Применяем новую позицию
            MainCharacter.Top = (int)newY;
        }

        /// <summary>
        /// Найди пару
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorFind_Click(object sender, EventArgs e)
        {
            MemoryPuzzleForm puzzle = new();
            puzzle.PuzzleSolved += (s, args) =>
            {
                // Это сработает когда головоломка решена
                doorPuzzleSolved = true;
                StartDoorFade(DoorFind);
                MessageBox.Show("Дверь исчезла! Появился новый путь!");
                MainCharacterText.Text = "ААА я позеленел...";
                MainCharacterText.Visible = true;
                MainCharacter.BackgroundImage = Properties.Resources.перс_зел;
            };
            puzzle.Show();
        }

        /// <summary>
        /// Обработка нажатия на персонажа, он говорит
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainCharacter_Click(object sender, EventArgs e)
        {
            // Массив с разными фразами
            string[] randomPhrases =
            {
                "Привет!",
                "Кто здесь?",
                "Не трогай меня!",
                "Я устал...",
                "Давай до свидания!",
                "Что ты хочешь?",
                "Я тебя не звал",
                "Покорми меня",
                "Может, чаю?",
                "Уже наливаю"
            };

            // Выбираем случайную фразу
            Random rnd = new Random();
            int index = rnd.Next(randomPhrases.Length);

            // Выводим её
            MainCharacterText.Text = randomPhrases[index];
            MainCharacterText.Visible = true;

            // Опционально: автоматически скрыть через 3 секунды
            Task.Delay(3000).ContinueWith(_ =>
            {
                if (MainCharacterText.InvokeRequired)
                    MainCharacterText.Invoke(new Action(() => MainCharacterText.Visible = false));
                else
                    MainCharacterText.Visible = false;
            });
        }

        /// <summary>
        /// Пятнашки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorSlidingPuzzle_Click(object sender, EventArgs e)
        {
            SlidingPuzzleForm slidingPuzzle = new();
            slidingPuzzle.Show();
        }

        /// <summary>
        /// Пятнашки с изображением
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorTentacles_Click(object sender, EventArgs e)
        {
            TentacleBossFightForm imagePuzzle = new TentacleBossFightForm();
            imagePuzzle.Show();
        }

        /// <summary>
        /// Составить слово (Ответ: Вспомни)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorWordScramble_Click(object sender, EventArgs e)
        {
            WordScrambleForm wordScramble = new WordScrambleForm();
            wordScramble.PuzzleSolved += (s, args) =>
            {
                // Скрываем дверь после решения головоломки
                DoorWordScramble.Visible = false;
                MessageBox.Show("Дверь исчезла! Появился новый путь!");
                MainCharacterText.Text = "ААА я пожелтел...";
                MainCharacterText.Visible = true;
                MainCharacter.BackgroundImage = Properties.Resources.перс_желт;
            };
            wordScramble.Show();
        }

        private void DoorMazeGame_Click(object sender, EventArgs e)
        {
            MazeGameForm mazeGame = new MazeGameForm();
            mazeGame.PuzzleSolved += (s, args) =>
            {
                // Действия при прохождении лабиринта
                MessageBox.Show("Вы получили ключ от следующей двери!");
                DoorTentacles.Visible = true;
                StartDoorFade(DoorMazeGame);
                MainCharacterText.Text = "ААА я поизумруднел...";
                MainCharacterText.Visible = true;
                MainCharacter.BackgroundImage = Properties.Resources.перс_изюм;
            };
            mazeGame.Show();
        }

        private void DoorReactionGame_Click(object sender, EventArgs e)
        {
            ReactionGameForm reactionGame = new ReactionGameForm();

            reactionGame.GameCompleted += (s, isSuccess) =>
            {
                if (isSuccess)
                {
                    StartDoorFade(DoorReactionGame);
                    MessageBox.Show("Дверь исчезла! Появился новый путь!");
                    MainCharacterText.Text = "ААА я покраснел...";
                    MainCharacterText.Visible = true;
                    MainCharacter.BackgroundImage = Properties.Resources.перс_красн;
                }
            };

            reactionGame.Show();
        }

        private void DoorGuessGame_Click(object sender, EventArgs e)
        {
            GuessNumberForm game = new GuessNumberForm();

            // Подписываемся на событие победы
            game.OnWin += () =>
            {
                StartDoorFade(DoorGuessGame);
                MessageBox.Show("Дверь исчезла! Появился новый путь!");
                MainCharacterText.Text = "ААА я полаймел...";
                MainCharacterText.Visible = true;
                MainCharacter.BackgroundImage = Properties.Resources.перс_лайм;
            };

            game.Show();
        }

        private void DoorTruePuzzle_Click(object sender, EventArgs e)
        {
            TruePuzzleGameForm puzzle = new TruePuzzleGameForm();

            puzzle.PuzzleCompleted += () =>
            {
                StartDoorFade(DoorTruePuzzle);
                MessageBox.Show("Дверь исчезла! Можно пройти дальше!");
                MainCharacterText.Text = "ААА я побелел...";
                MainCharacterText.Visible = true;
                MainCharacter.BackgroundImage = Properties.Resources.персонаж;
            };

            puzzle.Show();
        }
    }
}