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
            components = new System.ComponentModel.Container();
            DoorForest = new Button();
            Door2 = new Button();
            DoorTentacles = new Button();
            JustDoor = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button9 = new Button();
            button10 = new Button();
            bindingSource1 = new BindingSource(components);
            MainCharacterText = new Label();
            MainCharacter = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
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
            // Door2
            // 
            Door2.BackgroundImage = Properties.Resources.door;
            Door2.BackgroundImageLayout = ImageLayout.Zoom;
            Door2.FlatAppearance.BorderSize = 0;
            Door2.FlatStyle = FlatStyle.Flat;
            Door2.Location = new Point(295, 37);
            Door2.Name = "Door2";
            Door2.Size = new Size(112, 233);
            Door2.TabIndex = 1;
            Door2.UseVisualStyleBackColor = true;
            Door2.Click += Door2_Click;
            // 
            // DoorTentacles
            // 
            DoorTentacles.BackColor = Color.Transparent;
            DoorTentacles.BackgroundImage = Properties.Resources.щупальца;
            DoorTentacles.BackgroundImageLayout = ImageLayout.Zoom;
            DoorTentacles.FlatAppearance.BorderSize = 0;
            DoorTentacles.FlatStyle = FlatStyle.Flat;
            DoorTentacles.Location = new Point(443, 41);
            DoorTentacles.Name = "DoorTentacles";
            DoorTentacles.Size = new Size(258, 229);
            DoorTentacles.TabIndex = 2;
            DoorTentacles.UseVisualStyleBackColor = false;
            // 
            // JustDoor
            // 
            JustDoor.BackgroundImage = Properties.Resources.door;
            JustDoor.BackgroundImageLayout = ImageLayout.Zoom;
            JustDoor.FlatAppearance.BorderSize = 0;
            JustDoor.FlatStyle = FlatStyle.Flat;
            JustDoor.Location = new Point(741, 37);
            JustDoor.Name = "JustDoor";
            JustDoor.Size = new Size(112, 233);
            JustDoor.TabIndex = 3;
            JustDoor.UseVisualStyleBackColor = true;
            JustDoor.Click += JustDoor_Click;
            // 
            // button5
            // 
            button5.BackgroundImage = Properties.Resources.door;
            button5.BackgroundImageLayout = ImageLayout.Zoom;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(62, 352);
            button5.Name = "button5";
            button5.Size = new Size(112, 233);
            button5.TabIndex = 4;
            button5.UseVisualStyleBackColor = true;
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
            // button7
            // 
            button7.BackgroundImage = Properties.Resources.door;
            button7.BackgroundImageLayout = ImageLayout.Zoom;
            button7.FlatAppearance.BorderSize = 0;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Location = new Point(520, 352);
            button7.Name = "button7";
            button7.Size = new Size(112, 233);
            button7.TabIndex = 6;
            button7.UseVisualStyleBackColor = true;
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
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(JustDoor);
            Controls.Add(DoorTentacles);
            Controls.Add(Door2);
            Controls.Add(DoorForest);
            Name = "MainForm";
            Text = "Всем привет с вами демон и андроид";
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)MainCharacter).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button DoorForest;
        private Button Door2;
        private Button DoorTentacles;
        private Button JustDoor;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button9;
        private Button button10;
        private BindingSource bindingSource1;
        private Label MainCharacterText;
        private PictureBox MainCharacter;
    }
}
