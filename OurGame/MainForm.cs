using System.Media;

namespace OurGame
{
    public partial class MainForm : Form
    {
        private SoundPlayer backgroundMusic;

        private float floatYPosition; // ������� Y-���������� ��� �������� ��������
        private float floatSpeed = 0.2f; // �������� "�������"
        private float floatAmplitude = 5f; // ������ "�������������"
        private int originalY; // ����������� ������� ������


        public MainForm()
        {
            InitializeComponent();

            originalY = MainCharacter.Top; // ���������� ��������� �������
            floatYPosition = 0;

            // ������ ��� �������� (����������� ������ 20 ��)
            System.Windows.Forms.Timer floatTimer = new System.Windows.Forms.Timer();
            floatTimer.Interval = 20;
            floatTimer.Tick += FloatAnimation_Tick;
            floatTimer.Start();

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

        private void FloatAnimation_Tick(object sender, EventArgs e)
        {
            floatYPosition += floatSpeed;

            // �������������� �������� ��� ���������
            float newY = originalY + (float)(Math.Sin(floatYPosition) * floatAmplitude);

            // ��������� ����� �������
            MainCharacter.Top = (int)newY;
        }

        private void JustDoor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("� ������ �����");
        }
    }
}
