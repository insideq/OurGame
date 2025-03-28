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

        private void DoorForest_Click(object sender, EventArgs e)
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
            // ������ � ������� �������
            string[] randomPhrases =
            {
                "������!",
                "��� �����?",
                "�� ������ ����!",
                "� �����...",
                "����� �� ��������!",
                "��� �� ������?",
                "� ���� �� ����",
                "������� ����",
                "�����, ���?",
                "��� �������"
            };

            // �������� ��������� �����
            Random rnd = new Random();
            int index = rnd.Next(randomPhrases.Length);

            // ������� �
            MainCharacterText.Text = randomPhrases[index];
            MainCharacterText.Visible = true;

            // �����������: ������������� ������ ����� 3 �������
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
            MessageBox.Show("� ������ �����");
        }
    }
}
