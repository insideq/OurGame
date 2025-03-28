using System.Media;

namespace OurGame
{
    public partial class MainForm : Form
    {
        private SoundPlayer backgroundMusic;

        public MainForm()
        {
            InitializeComponent();

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

        private void JustDoor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Я просто дверь");
        }
    }
}
