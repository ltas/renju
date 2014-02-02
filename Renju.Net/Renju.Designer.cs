namespace Renju.Net
{
    partial class Renju
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newtCtrlNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameBtn = new System.Windows.Forms.Button();
            this.connectBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.startMenu = new System.Windows.Forms.GroupBox();
            this.createNewGameBox = new System.Windows.Forms.GroupBox();
            this.localPortTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.initializeNewGameBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.serverNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.playersQuantityBox = new System.Windows.Forms.ComboBox();
            this.playersGroupBox = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.sendMessageBtn = new System.Windows.Forms.Button();
            this.msgBox = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.ListBox();
            this.connectBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.playerNameTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.startMenu.SuspendLayout();
            this.createNewGameBox.SuspendLayout();
            this.playersGroupBox.SuspendLayout();
            this.connectBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameGToolStripMenuItem,
            this.helpHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(751, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameGToolStripMenuItem
            // 
            this.gameGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newtCtrlNToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.gameGToolStripMenuItem.Name = "gameGToolStripMenuItem";
            this.gameGToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.gameGToolStripMenuItem.Text = "Файл";
            // 
            // newtCtrlNToolStripMenuItem
            // 
            this.newtCtrlNToolStripMenuItem.Name = "newtCtrlNToolStripMenuItem";
            this.newtCtrlNToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.newtCtrlNToolStripMenuItem.Text = "Меню";
            this.newtCtrlNToolStripMenuItem.Click += new System.EventHandler(this.OnNewGame);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(93, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.exitToolStripMenuItem.Text = "Вихід";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // helpHToolStripMenuItem
            // 
            this.helpHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpHToolStripMenuItem.Name = "helpHToolStripMenuItem";
            this.helpHToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.helpHToolStripMenuItem.Text = "Довідка";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.aboutToolStripMenuItem.Text = "Про програму";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAbout);
            // 
            // newGameBtn
            // 
            this.newGameBtn.Location = new System.Drawing.Point(22, 19);
            this.newGameBtn.Name = "newGameBtn";
            this.newGameBtn.Size = new System.Drawing.Size(176, 50);
            this.newGameBtn.TabIndex = 0;
            this.newGameBtn.Text = "Нова гра";
            this.newGameBtn.UseVisualStyleBackColor = true;
            this.newGameBtn.Click += new System.EventHandler(this.newGame);
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(22, 75);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(176, 50);
            this.connectBtn.TabIndex = 1;
            this.connectBtn.Text = "З\'єднання";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(22, 140);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(176, 50);
            this.exitBtn.TabIndex = 2;
            this.exitBtn.Text = "Вихід";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // startMenu
            // 
            this.startMenu.Controls.Add(this.newGameBtn);
            this.startMenu.Controls.Add(this.exitBtn);
            this.startMenu.Controls.Add(this.connectBtn);
            this.startMenu.Location = new System.Drawing.Point(12, 34);
            this.startMenu.Name = "startMenu";
            this.startMenu.Size = new System.Drawing.Size(227, 205);
            this.startMenu.TabIndex = 3;
            this.startMenu.TabStop = false;
            // 
            // createNewGameBox
            // 
            this.createNewGameBox.Controls.Add(this.localPortTextBox);
            this.createNewGameBox.Controls.Add(this.label7);
            this.createNewGameBox.Controls.Add(this.label6);
            this.createNewGameBox.Controls.Add(this.initializeNewGameBtn);
            this.createNewGameBox.Controls.Add(this.label2);
            this.createNewGameBox.Controls.Add(this.serverNameTextBox);
            this.createNewGameBox.Controls.Add(this.label1);
            this.createNewGameBox.Controls.Add(this.playersQuantityBox);
            this.createNewGameBox.Location = new System.Drawing.Point(263, 34);
            this.createNewGameBox.Name = "createNewGameBox";
            this.createNewGameBox.Size = new System.Drawing.Size(232, 183);
            this.createNewGameBox.TabIndex = 4;
            this.createNewGameBox.TabStop = false;
            this.createNewGameBox.Text = "Нова гра";
            this.createNewGameBox.Visible = false;
            // 
            // localPortTextBox
            // 
            this.localPortTextBox.Location = new System.Drawing.Point(54, 68);
            this.localPortTextBox.Name = "localPortTextBox";
            this.localPortTextBox.Size = new System.Drawing.Size(162, 20);
            this.localPortTextBox.TabIndex = 7;
            this.localPortTextBox.Text = "8888";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Порт:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "IP: ";
            // 
            // initializeNewGameBtn
            // 
            this.initializeNewGameBtn.Location = new System.Drawing.Point(126, 147);
            this.initializeNewGameBtn.Name = "initializeNewGameBtn";
            this.initializeNewGameBtn.Size = new System.Drawing.Size(100, 23);
            this.initializeNewGameBtn.TabIndex = 4;
            this.initializeNewGameBtn.Text = "Створити";
            this.initializeNewGameBtn.UseVisualStyleBackColor = true;
            this.initializeNewGameBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ваше ім\'я:";
            // 
            // serverNameTextBox
            // 
            this.serverNameTextBox.Location = new System.Drawing.Point(10, 121);
            this.serverNameTextBox.Name = "serverNameTextBox";
            this.serverNameTextBox.Size = new System.Drawing.Size(216, 20);
            this.serverNameTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Кількість гравців";
            // 
            // playersQuantityBox
            // 
            this.playersQuantityBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.playersQuantityBox.FormattingEnabled = true;
            this.playersQuantityBox.Items.AddRange(new object[] {
            "2",
            "3",
            "4"});
            this.playersQuantityBox.Location = new System.Drawing.Point(111, 19);
            this.playersQuantityBox.Name = "playersQuantityBox";
            this.playersQuantityBox.Size = new System.Drawing.Size(114, 21);
            this.playersQuantityBox.TabIndex = 0;
            // 
            // playersGroupBox
            // 
            this.playersGroupBox.Controls.Add(this.listView1);
            this.playersGroupBox.Controls.Add(this.sendMessageBtn);
            this.playersGroupBox.Controls.Add(this.msgBox);
            this.playersGroupBox.Controls.Add(this.chatBox);
            this.playersGroupBox.Location = new System.Drawing.Point(538, 34);
            this.playersGroupBox.Name = "playersGroupBox";
            this.playersGroupBox.Size = new System.Drawing.Size(205, 470);
            this.playersGroupBox.TabIndex = 5;
            this.playersGroupBox.TabStop = false;
            this.playersGroupBox.Text = "Гравці";
            this.playersGroupBox.Visible = false;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.Tan;
            this.listView1.Location = new System.Drawing.Point(12, 22);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(182, 40);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // sendMessageBtn
            // 
            this.sendMessageBtn.Location = new System.Drawing.Point(116, 438);
            this.sendMessageBtn.Name = "sendMessageBtn";
            this.sendMessageBtn.Size = new System.Drawing.Size(78, 26);
            this.sendMessageBtn.TabIndex = 3;
            this.sendMessageBtn.Text = "Надіслати";
            this.sendMessageBtn.UseVisualStyleBackColor = true;
            this.sendMessageBtn.Click += new System.EventHandler(this.sendMessageBtn_Click);
            // 
            // msgBox
            // 
            this.msgBox.Location = new System.Drawing.Point(12, 412);
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(182, 20);
            this.msgBox.TabIndex = 2;
            // 
            // chatBox
            // 
            this.chatBox.FormattingEnabled = true;
            this.chatBox.Location = new System.Drawing.Point(12, 78);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(182, 329);
            this.chatBox.TabIndex = 1;
            // 
            // connectBox
            // 
            this.connectBox.Controls.Add(this.button1);
            this.connectBox.Controls.Add(this.label5);
            this.connectBox.Controls.Add(this.playerNameTextBox);
            this.connectBox.Controls.Add(this.portTextBox);
            this.connectBox.Controls.Add(this.label4);
            this.connectBox.Controls.Add(this.label3);
            this.connectBox.Controls.Add(this.ipTextBox);
            this.connectBox.Location = new System.Drawing.Point(263, 223);
            this.connectBox.Name = "connectBox";
            this.connectBox.Size = new System.Drawing.Size(232, 154);
            this.connectBox.TabIndex = 6;
            this.connectBox.TabStop = false;
            this.connectBox.Text = "Під\'єднання до гри";
            this.connectBox.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "З\'єднатись";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Ваше ім\'я:";
            // 
            // playerNameTextBox
            // 
            this.playerNameTextBox.Location = new System.Drawing.Point(9, 98);
            this.playerNameTextBox.Name = "playerNameTextBox";
            this.playerNameTextBox.Size = new System.Drawing.Size(215, 20);
            this.playerNameTextBox.TabIndex = 5;
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(51, 45);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(173, 20);
            this.portTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Порт:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "IP:";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(51, 22);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(173, 20);
            this.ipTextBox.TabIndex = 0;
            this.ipTextBox.Text = "192.168.0.101";
            // 
            // Renju
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(751, 510);
            this.Controls.Add(this.connectBox);
            this.Controls.Add(this.playersGroupBox);
            this.Controls.Add(this.createNewGameBox);
            this.Controls.Add(this.startMenu);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Renju";
            this.Text = "Рензю";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Renju_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.startMenu.ResumeLayout(false);
            this.createNewGameBox.ResumeLayout(false);
            this.createNewGameBox.PerformLayout();
            this.playersGroupBox.ResumeLayout(false);
            this.playersGroupBox.PerformLayout();
            this.connectBox.ResumeLayout(false);
            this.connectBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newtCtrlNToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

        private System.Windows.Forms.Button newGameBtn;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.GroupBox startMenu;
        private System.Windows.Forms.GroupBox createNewGameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox playersQuantityBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox serverNameTextBox;
        private System.Windows.Forms.Button initializeNewGameBtn;
        private System.Windows.Forms.GroupBox playersGroupBox;
        private System.Windows.Forms.Button sendMessageBtn;
        private System.Windows.Forms.TextBox msgBox;
        private System.Windows.Forms.ListBox chatBox;
        private System.Windows.Forms.GroupBox connectBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox playerNameTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox localPortTextBox;
        private System.Windows.Forms.Label label7;
    }
}

