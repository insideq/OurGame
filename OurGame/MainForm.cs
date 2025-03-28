using System.Media;

namespace OurGame
{
    public partial class MainForm : Form
    {
        private SoundPlayer backgroundMusic;

        public MainForm()
        {
            InitializeComponent();

            // �������� ������ �� ��������
            //backgroundMusic = new SoundPlayer(Properties.Resources.MORGENSHTERN_����);

            // ��� �� ����� �� ����
            //backgroundMusic = new SoundPlayer(@"C:\Users\���\Desktop\OurGame\OurGame\Resources\MORGENSHTERN-����.wav");

            //backgroundMusic.PlayLooping();
        }

        private void Door1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("�� ��� �����? � ��� �� ����\n����� �����");
        }

        // �� �������� ���������� ������� ��� �������� �����
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            backgroundMusic?.Stop();
            backgroundMusic?.Dispose();
        }

        private void MainCharacter_Click(object sender, EventArgs e)
        {
            MainCharacterText.Text = "������!";
            MainCharacterText.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("� ������ �����");
        }
    }
}
