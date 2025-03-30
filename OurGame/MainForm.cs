using System.Media;

namespace OurGame
{
    /// <summary>
    /// ������� �����
    /// </summary>
    public partial class MainForm : Form
    {
        private SoundPlayer backgroundMusic; // ������ �� ���
        private SoundPlayer openDoor;

        private float floatYPosition; // ������� Y-���������� ��� �������� ��������
        private float floatSpeed = 0.2f; // �������� "�������"
        private float floatAmplitude = 5f; // ������ "�������������"
        private int originalY; // ����������� ������� ������
        private bool doorPuzzleSolved = false; // ���� ������� �����������

        // ������ ��� �������� ������������ ������
        private System.Windows.Forms.Timer doorFadeTimer;
        private Control currentFadingDoor;
        private float doorOpacity = 1.0f;

        public MainForm()
        {
            InitializeComponent();

            originalY = MainCharacter.Top; // ���������� ��������� �������
            floatYPosition = 0;

            // ������ ��� �������� ��������� (����������� ������ 20 ��)
            System.Windows.Forms.Timer floatTimer = new System.Windows.Forms.Timer();
            floatTimer.Interval = 20;
            floatTimer.Tick += FloatAnimation_Tick;
            floatTimer.Start();

            // ������������� ������� ��� �������� ������
            doorFadeTimer = new System.Windows.Forms.Timer();
            doorFadeTimer.Interval = 30; // �������� � �������������
            doorFadeTimer.Tick += DoorFadeAnimation_Tick;

            // �������� ������ �� ��������
            //backgroundMusic = new SoundPlayer(Properties.Resources.MORGENSHTERN_����);

            // ��� �� ����� �� ����
            //backgroundMusic = new SoundPlayer(@"C:\Users\���\Desktop\OurGame\OurGame\Resources\MORGENSHTERN-����.wav");

            //backgroundMusic.PlayLooping();
        }

        // �������� ������������ �����
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

                // ��������� ������������
                currentFadingDoor.BackColor = Color.FromArgb((int)(doorOpacity * 255),
                    currentFadingDoor.BackColor);

                // ��������� ������
                float scale = 0.9f; // ����������� ����������
                currentFadingDoor.Width = (int)(currentFadingDoor.Width * scale);
                currentFadingDoor.Height = (int)(currentFadingDoor.Height * scale);
                currentFadingDoor.Left += (int)(currentFadingDoor.Width * (1 - scale) / 2);
                currentFadingDoor.Top += (int)(currentFadingDoor.Height * (1 - scale) / 2);
            }
        }

        // ������ �������� ������������ �����
        private void StartDoorFade(Control door)
        {
            currentFadingDoor = door;
            doorOpacity = 1.0f;
            doorFadeTimer.Start();
        }

        /// <summary>
        /// �����, ������� ��� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoorForest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("��������� ����..."); 
            StartDoorFade(DoorForest);
            MessageBox.Show("����� �������...");
            MainCharacterText.Text = "��� � �������...";
            MainCharacterText.Visible = true;
            MainCharacter.BackgroundImage = Properties.Resources.����_�����;
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
                StartDoorFade(DoorFind);
                MessageBox.Show("����� �������! �������� ����� ����!");
                MainCharacterText.Text = "��� � ���������...";
                MainCharacterText.Visible = true;
                MainCharacter.BackgroundImage = Properties.Resources.����_���;
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
            TentacleBossFightForm imagePuzzle = new TentacleBossFightForm();
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
                MainCharacterText.Text = "��� � ��������...";
                MainCharacterText.Visible = true;
                MainCharacter.BackgroundImage = Properties.Resources.����_����;
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
                StartDoorFade(DoorMazeGame);
                MainCharacterText.Text = "��� � ������������...";
                MainCharacterText.Visible = true;
                MainCharacter.BackgroundImage = Properties.Resources.����_����;
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
                    MessageBox.Show("����� �������! �������� ����� ����!");
                    MainCharacterText.Text = "��� � ���������...";
                    MainCharacterText.Visible = true;
                    MainCharacter.BackgroundImage = Properties.Resources.����_�����;
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
                StartDoorFade(DoorGuessGame);
                MessageBox.Show("����� �������! �������� ����� ����!");
                MainCharacterText.Text = "��� � ��������...";
                MainCharacterText.Visible = true;
                MainCharacter.BackgroundImage = Properties.Resources.����_����;
            };

            game.Show();
        }

        private void DoorTruePuzzle_Click(object sender, EventArgs e)
        {
            TruePuzzleGameForm puzzle = new TruePuzzleGameForm();

            puzzle.PuzzleCompleted += () =>
            {
                StartDoorFade(DoorTruePuzzle);
                MessageBox.Show("����� �������! ����� ������ ������!");
                MainCharacterText.Text = "��� � �������...";
                MainCharacterText.Visible = true;
                MainCharacter.BackgroundImage = Properties.Resources.��������;
            };

            puzzle.Show();
        }
    }
}