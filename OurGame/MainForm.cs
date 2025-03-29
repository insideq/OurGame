using System.Media;

namespace OurGame
{
    /// <summary>
    /// ������� �����
    /// </summary>
    public partial class MainForm : Form
    {
        private SoundPlayer backgroundMusic; // ������ �� ���

        private float floatYPosition; // ������� Y-���������� ��� �������� ��������
        private float floatSpeed = 0.2f; // �������� "�������"
        private float floatAmplitude = 5f; // ������ "�������������"
        private int originalY; // ����������� ������� ������
        private bool doorPuzzleSolved = false; // ���� ������� �����������


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

        /// <summary>
        /// �����, ������� ��� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorForest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("�� ��� �����? � ��� �� ����\n����� �����");
            DoorForest.Visible = false;
            MessageBox.Show("����� �������...");
        }

        /// <summary>
        /// ����������� ������� ��� �������� �����
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            backgroundMusic?.Stop();
            backgroundMusic?.Dispose();
        }

        /// <summary>
        /// �������� ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FloatAnimation_Tick(object sender, EventArgs e)
        {
            floatYPosition += floatSpeed;

            // �������������� �������� ��� ���������
            float newY = originalY + (float)(Math.Sin(floatYPosition) * floatAmplitude);

            // ��������� ����� �������
            MainCharacter.Top = (int)newY;
        }

        /// <summary>
        /// ����� ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorFind_Click(object sender, EventArgs e)
        {
            MemoryPuzzleForm puzzle = new();
            puzzle.PuzzleSolved += (s, args) =>
            {
                // ��� ��������� ����� ����������� ������
                doorPuzzleSolved = true;
                DoorFind.Visible = false; // �������� �����
                MessageBox.Show("����� �������! �������� ����� ����!");
            };
            puzzle.Show();
        }

        /// <summary>
        /// ��������� ������� �� ���������, �� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            Task.Delay(3000).ContinueWith(_ =>
            {
                if (MainCharacterText.InvokeRequired)
                    MainCharacterText.Invoke(new Action(() => MainCharacterText.Visible = false));
                else
                    MainCharacterText.Visible = false;
            });
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorSlidingPuzzle_Click(object sender, EventArgs e)
        {
            SlidingPuzzleForm slidingPuzzle = new();
            slidingPuzzle.Show();
        }

        /// <summary>
        /// �������� � ������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorTentacles_Click(object sender, EventArgs e)
        {
            string imagePath = @"C:\Users\���\Desktop\OurGame\OurGame\Resources\Puzzle.jpg"; // ������� ���� � ������ �����������
            ImagePuzzleForm imagePuzzle = new ImagePuzzleForm(imagePath);
            imagePuzzle.Show();
        }

        /// <summary>
        /// ��������� ����� (�����: �������)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorWordScramble_Click(object sender, EventArgs e)
        {
            WordScrambleForm wordScramble = new WordScrambleForm();
            wordScramble.PuzzleSolved += (s, args) =>
            {
                // �������� ����� ����� ������� �����������
                DoorWordScramble.Visible = false;
                MessageBox.Show("����� �������! �������� ����� ����!");
            };
            wordScramble.Show();
        }

        private void DoorMazeGame_Click(object sender, EventArgs e)
        {
            MazeGameForm mazeGame = new MazeGameForm();
            mazeGame.PuzzleSolved += (s, args) =>
            {
                // �������� ��� ����������� ���������
                MessageBox.Show("�� �������� ���� �� ��������� �����!");
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
                    // �������� ����� ������ ��� �������� �����������
                    DoorReactionGame.Visible = false;

                    // ����� �������� �������������� ��������
                    MessageBox.Show("����� �������! �������� ����� ����!");
                }
            };

            reactionGame.Show();
        }

        private void DoorGuessGame_Click(object sender, EventArgs e)
        {
            GuessNumberForm game = new GuessNumberForm();

            // ������������� �� ������� ������
            game.OnWin += () =>
            {
                // ��� ���������� ��� ������
                DoorGuessGame.Visible = false;

                // ����� �������� �������������� ��������
                MessageBox.Show("����� �������! �������� ����� ����!");
            };

            game.Show();
        }
    }
}