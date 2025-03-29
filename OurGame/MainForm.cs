using System.Media;

namespace OurGame
{
    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class MainForm : Form
    {
        private SoundPlayer backgroundMusic; // Музыка на фон

        private float floatYPosition; // Текущая Y-координата для плавного движения
        private float floatSpeed = 0.2f; // Скорость "парения"
        private float floatAmplitude = 5f; // Высота "подпрыгивания"
        private int originalY; // Изначальная позиция кнопки
        private bool doorPuzzleSolved = false; // Флаг решения головоломки


        public MainForm()
        {
            InitializeComponent();

            originalY = MainCharacter.Top; // Запоминаем стартовую позицию
            floatYPosition = 0;

            // Таймер для анимации (обновляется каждые 20 мс)
            System.Windows.Forms.Timer floatTimer = new System.Windows.Forms.Timer();
            floatTimer.Interval = 20;
            floatTimer.Tick += FloatAnimation_Tick;
            floatTimer.Start();

            // Загрузка музыки из ресурсов
            //backgroundMusic = new SoundPlayer(Properties.Resources.MORGENSHTERN_ДУЛО);

            // Или из файла по пути
            //backgroundMusic = new SoundPlayer(@"C:\Users\Имя\Desktop\OurGame\OurGame\Resources\MORGENSHTERN-ДУЛО.wav");

            //backgroundMusic.PlayLooping();
        }

        /// <summary>
        /// Дверь, которая шлёт лесом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorForest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы кто такие? Я вас не звал\nИдите лесом");
            DoorForest.Visible = false;
            MessageBox.Show("Дверь исчезла...");
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
                DoorFind.Visible = false; // Скрываем дверь
                MessageBox.Show("Дверь исчезла! Появился новый путь!");
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
            string imagePath = @"C:\Users\Имя\Desktop\OurGame\OurGame\Resources\Puzzle.jpg"; // Укажите путь к вашему изображению
            ImagePuzzleForm imagePuzzle = new ImagePuzzleForm(imagePath);
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
                DoorMazeGame.Visible = false;
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
                    // Скрываем дверь только при успешном прохождении
                    DoorReactionGame.Visible = false;

                    // Можно добавить дополнительные действия
                    MessageBox.Show("Дверь исчезла! Появился новый путь!");
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
                // Это выполнится при победе
                DoorGuessGame.Visible = false;

                // Можно добавить дополнительные действия
                MessageBox.Show("Дверь исчезла! Появился новый путь!");
            };

            game.Show();
        }
    }
}