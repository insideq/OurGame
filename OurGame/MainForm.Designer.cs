namespace OurGame
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DoorForest = new Button();
            DoorSlidingPuzzle = new Button();
            DoorTentacles = new Button();
            DoorFind = new Button();
            DoorMazeGame = new Button();
            button6 = new Button();
            DoorWordScramble = new Button();
            button9 = new Button();
            button10 = new Button();
            MainCharacterText = new Label();
            MainCharacter = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)MainCharacter).BeginInit();
            SuspendLayout();
            // 
            // DoorForest
            // 
            DoorForest.BackgroundImage = Properties.Resources.door;
            DoorForest.BackgroundImageLayout = ImageLayout.Zoom;
            DoorForest.FlatAppearance.BorderSize = 0;
            DoorForest.FlatStyle = FlatStyle.Flat;
            DoorForest.Location = new Point(62, 37);
            DoorForest.Name = "DoorForest";
            DoorForest.Size = new Size(112, 233);
            DoorForest.TabIndex = 0;
            DoorForest.UseVisualStyleBackColor = true;
            DoorForest.Click += DoorForest_Click;
            // 
            // DoorSlidingPuzzle
            // 
            DoorSlidingPuzzle.BackgroundImage = Properties.Resources.door;
            DoorSlidingPuzzle.BackgroundImageLayout = ImageLayout.Zoom;
            DoorSlidingPuzzle.FlatAppearance.BorderSize = 0;
            DoorSlidingPuzzle.FlatStyle = FlatStyle.Flat;
            DoorSlidingPuzzle.Location = new Point(295, 37);
            DoorSlidingPuzzle.Name = "DoorSlidingPuzzle";
            DoorSlidingPuzzle.Size = new Size(112, 233);
            DoorSlidingPuzzle.TabIndex = 1;
            DoorSlidingPuzzle.UseVisualStyleBackColor = true;
            DoorSlidingPuzzle.Click += DoorSlidingPuzzle_Click;
            // 
            // DoorTentacles
            // 
            DoorTentacles.BackColor = Color.Transparent;
            DoorTentacles.BackgroundImage = Properties.Resources.щупальца;
            DoorTentacles.BackgroundImageLayout = ImageLayout.Zoom;
            DoorTentacles.FlatAppearance.BorderSize = 0;
            DoorTentacles.FlatStyle = FlatStyle.Popup;
            DoorTentacles.Location = new Point(443, 41);
            DoorTentacles.Name = "DoorTentacles";
            DoorTentacles.Size = new Size(258, 229);
            DoorTentacles.TabIndex = 2;
            DoorTentacles.UseVisualStyleBackColor = false;
            DoorTentacles.Visible = false;
            DoorTentacles.Click += DoorTentacles_Click;
            // 
            // DoorFind
            // 
            DoorFind.BackgroundImage = Properties.Resources.door;
            DoorFind.BackgroundImageLayout = ImageLayout.Zoom;
            DoorFind.FlatAppearance.BorderSize = 0;
            DoorFind.FlatStyle = FlatStyle.Flat;
            DoorFind.Location = new Point(741, 37);
            DoorFind.Name = "DoorFind";
            DoorFind.Size = new Size(112, 233);
            DoorFind.TabIndex = 3;
            DoorFind.UseVisualStyleBackColor = true;
            DoorFind.Click += DoorFind_Click;
            // 
            // DoorMazeGame
            // 
            DoorMazeGame.BackgroundImage = Properties.Resources.door;
            DoorMazeGame.BackgroundImageLayout = ImageLayout.Zoom;
            DoorMazeGame.FlatAppearance.BorderSize = 0;
            DoorMazeGame.FlatStyle = FlatStyle.Flat;
            DoorMazeGame.Location = new Point(62, 352);
            DoorMazeGame.Name = "DoorMazeGame";
            DoorMazeGame.Size = new Size(112, 233);
            DoorMazeGame.TabIndex = 4;
            DoorMazeGame.UseVisualStyleBackColor = true;
            DoorMazeGame.Click += DoorMazeGame_Click;
            // 
            // button6
            // 
            button6.BackgroundImage = Properties.Resources.door;
            button6.BackgroundImageLayout = ImageLayout.Zoom;
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Location = new Point(295, 352);
            button6.Name = "button6";
            button6.Size = new Size(112, 233);
            button6.TabIndex = 5;
            button6.UseVisualStyleBackColor = true;
            // 
            // DoorWordScramble
            // 
            DoorWordScramble.BackgroundImage = Properties.Resources.door;
            DoorWordScramble.BackgroundImageLayout = ImageLayout.Zoom;
            DoorWordScramble.FlatAppearance.BorderSize = 0;
            DoorWordScramble.FlatStyle = FlatStyle.Flat;
            DoorWordScramble.Location = new Point(520, 352);
            DoorWordScramble.Name = "DoorWordScramble";
            DoorWordScramble.Size = new Size(112, 233);
            DoorWordScramble.TabIndex = 6;
            DoorWordScramble.UseVisualStyleBackColor = true;
            DoorWordScramble.Click += DoorWordScramble_Click;
            // 
            // button9
            // 
            button9.BackgroundImage = Properties.Resources.door;
            button9.BackgroundImageLayout = ImageLayout.Zoom;
            button9.FlatAppearance.BorderSize = 0;
            button9.FlatStyle = FlatStyle.Flat;
            button9.Location = new Point(741, 352);
            button9.Name = "button9";
            button9.Size = new Size(112, 233);
            button9.TabIndex = 8;
            button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            button10.BackgroundImage = Properties.Resources.door;
            button10.BackgroundImageLayout = ImageLayout.Zoom;
            button10.FlatAppearance.BorderSize = 0;
            button10.FlatStyle = FlatStyle.Flat;
            button10.Location = new Point(960, 352);
            button10.Name = "button10";
            button10.Size = new Size(112, 233);
            button10.TabIndex = 9;
            button10.UseVisualStyleBackColor = true;
            // 
            // MainCharacterText
            // 
            MainCharacterText.AutoSize = true;
            MainCharacterText.BackColor = SystemColors.GradientInactiveCaption;
            MainCharacterText.Location = new Point(895, 18);
            MainCharacterText.Name = "MainCharacterText";
            MainCharacterText.Size = new Size(0, 20);
            MainCharacterText.TabIndex = 11;
            MainCharacterText.Visible = false;
            // 
            // MainCharacter
            // 
            MainCharacter.BackColor = Color.Transparent;
            MainCharacter.BackgroundImage = Properties.Resources.персонаж;
            MainCharacter.BackgroundImageLayout = ImageLayout.Zoom;
            MainCharacter.Location = new Point(922, 41);
            MainCharacter.Name = "MainCharacter";
            MainCharacter.Size = new Size(150, 208);
            MainCharacter.TabIndex = 12;
            MainCharacter.TabStop = false;
            MainCharacter.Click += MainCharacter_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.ковер;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1137, 643);
            Controls.Add(MainCharacter);
            Controls.Add(MainCharacterText);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(DoorWordScramble);
            Controls.Add(button6);
            Controls.Add(DoorMazeGame);
            Controls.Add(DoorFind);
            Controls.Add(DoorTentacles);
            Controls.Add(DoorSlidingPuzzle);
            Controls.Add(DoorForest);
            Name = "MainForm";
            Text = "Всем привет с вами демон и андроид";
            ((System.ComponentModel.ISupportInitialize)MainCharacter).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button DoorForest;
        private Button DoorSlidingPuzzle;
        private Button DoorTentacles;
        private Button DoorFind;
        private Button DoorMazeGame;
        private Button button6;
        private Button DoorWordScramble;
        private Button button9;
        private Button button10;
        private Label MainCharacterText;
        private PictureBox MainCharacter;
    }
}
