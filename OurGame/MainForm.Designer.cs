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
            Door1 = new Button();
            button2 = new Button();
            DoorTentacles = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button9 = new Button();
            button10 = new Button();
            MainCharacter = new Button();
            bindingSource1 = new BindingSource(components);
            MainCharacterText = new Label();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // Door1
            // 
            Door1.BackgroundImage = Properties.Resources.door;
            Door1.BackgroundImageLayout = ImageLayout.Zoom;
            Door1.FlatAppearance.BorderSize = 0;
            Door1.FlatStyle = FlatStyle.Flat;
            Door1.Location = new Point(62, 37);
            Door1.Name = "Door1";
            Door1.Size = new Size(112, 233);
            Door1.TabIndex = 0;
            Door1.UseVisualStyleBackColor = true;
            Door1.Click += Door1_Click;
            // 
            // button2
            // 
            button2.BackgroundImage = Properties.Resources.door;
            button2.BackgroundImageLayout = ImageLayout.Zoom;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(295, 37);
            button2.Name = "button2";
            button2.Size = new Size(112, 233);
            button2.TabIndex = 1;
            button2.UseVisualStyleBackColor = true;
            // 
            // DoorTentacles
            // 
            DoorTentacles.BackColor = Color.Transparent;
            DoorTentacles.BackgroundImage = Properties.Resources.щупальца;
            DoorTentacles.BackgroundImageLayout = ImageLayout.Zoom;
            DoorTentacles.FlatAppearance.BorderSize = 0;
            DoorTentacles.FlatStyle = FlatStyle.Flat;
            DoorTentacles.Location = new Point(440, 41);
            DoorTentacles.Name = "DoorTentacles";
            DoorTentacles.Size = new Size(267, 224);
            DoorTentacles.TabIndex = 2;
            DoorTentacles.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackgroundImage = Properties.Resources.door;
            button4.BackgroundImageLayout = ImageLayout.Zoom;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Location = new Point(741, 37);
            button4.Name = "button4";
            button4.Size = new Size(112, 233);
            button4.TabIndex = 3;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
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
            // MainCharacter
            // 
            MainCharacter.BackColor = Color.Transparent;
            MainCharacter.BackgroundImage = Properties.Resources.персонаж;
            MainCharacter.BackgroundImageLayout = ImageLayout.Zoom;
            MainCharacter.FlatAppearance.BorderSize = 0;
            MainCharacter.FlatAppearance.MouseDownBackColor = Color.Transparent;
            MainCharacter.FlatAppearance.MouseOverBackColor = Color.Transparent;
            MainCharacter.FlatStyle = FlatStyle.Flat;
            MainCharacter.Location = new Point(939, 49);
            MainCharacter.Margin = new Padding(0);
            MainCharacter.Name = "MainCharacter";
            MainCharacter.Size = new Size(150, 208);
            MainCharacter.TabIndex = 10;
            MainCharacter.UseVisualStyleBackColor = false;
            MainCharacter.Click += MainCharacter_Click;
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.ковер;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1137, 643);
            Controls.Add(MainCharacterText);
            Controls.Add(MainCharacter);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(DoorTentacles);
            Controls.Add(button2);
            Controls.Add(Door1);
            Name = "MainForm";
            Text = "Всем привет с вами демон и андроид";
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Door1;
        private Button button2;
        private Button DoorTentacles;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button9;
        private Button button10;
        private Button MainCharacter;
        private BindingSource bindingSource1;
        private Label MainCharacterText;
    }
}
