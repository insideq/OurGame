using System.Media;

namespace OurGame
{
    public partial class MainForm : Form
    {
        private SoundPlayer backgroundMusic;

        private float floatYPosition; // Текущая Y-координата для плавного движения
        private float floatSpeed = 0.2f; // Скорость "парения"
        private float floatAmplitude = 5f; // Высота "подпрыгивания"
        private int originalY; // Изначальная позиция кнопки


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

        private void DoorForest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы кто такие? Я вас не звал\nИдите лесом");
        }

        // Не забудьте освободить ресурсы при закрытии формы
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            backgroundMusic?.Stop();
            backgroundMusic?.Dispose();
        }

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
            //Task.Delay(3000).ContinueWith(_ =>
            //{
            //    if (MainCharacterText.InvokeRequired)
            //        MainCharacterText.Invoke(new Action(() => MainCharacterText.Visible = false));
            //    else
            //        MainCharacterText.Visible = false;
            //});
        }

        private void FloatAnimation_Tick(object sender, EventArgs e)
        {
            floatYPosition += floatSpeed;

            // Синусоидальное движение для плавности
            float newY = originalY + (float)(Math.Sin(floatYPosition) * floatAmplitude);

            // Применяем новую позицию
            MainCharacter.Top = (int)newY;
        }

        private void JustDoor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Я просто дверь");
        }
    }
}
