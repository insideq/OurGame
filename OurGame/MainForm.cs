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

        private void Door1_Click(object sender, EventArgs e)
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
            MainCharacterText.Text = "Привет!";
            MainCharacterText.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Я просто дверь");
        }
    }
}
