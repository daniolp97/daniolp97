namespace FightIslands
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonHostGame = new System.Windows.Forms.Button();
            this.buttonJoinGame = new System.Windows.Forms.Button();
            this.panelHost = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownTurnSpyCells = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.labelStartBuildings = new System.Windows.Forms.Label();
            this.checkedListBoxStartBuildings = new System.Windows.Forms.CheckedListBox();
            this.checkBoxSameMap = new System.Windows.Forms.CheckBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.labelMapSize = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudPeople = new System.Windows.Forms.NumericUpDown();
            this.labelResPeople = new System.Windows.Forms.Label();
            this.nudCash = new System.Windows.Forms.NumericUpDown();
            this.labelResCash = new System.Windows.Forms.Label();
            this.nudEnergy = new System.Windows.Forms.NumericUpDown();
            this.labelStartRes = new System.Windows.Forms.Label();
            this.labelResEnergy = new System.Windows.Forms.Label();
            this.labelBlackLine1 = new System.Windows.Forms.Label();
            this.textBoxYourNickHost = new System.Windows.Forms.TextBox();
            this.labelNickLabel = new System.Windows.Forms.Label();
            this.labelYourIP = new System.Windows.Forms.Label();
            this.labelIPlabel = new System.Windows.Forms.Label();
            this.panelJoin = new System.Windows.Forms.Panel();
            this.textBoxGameIP = new System.Windows.Forms.TextBox();
            this.buttonConnectGame = new System.Windows.Forms.Button();
            this.textBoxYourNickJoin = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.labelIP = new System.Windows.Forms.Label();
            this.buttonSingle = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTurnSpyCells)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeople)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnergy)).BeginInit();
            this.panelJoin.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonHostGame
            // 
            this.buttonHostGame.Location = new System.Drawing.Point(3, 4);
            this.buttonHostGame.Name = "buttonHostGame";
            this.buttonHostGame.Size = new System.Drawing.Size(141, 33);
            this.buttonHostGame.TabIndex = 0;
            this.buttonHostGame.Text = "Stwórz grę";
            this.buttonHostGame.UseVisualStyleBackColor = true;
            this.buttonHostGame.Click += new System.EventHandler(this.ButtonTypeClick);
            // 
            // buttonJoinGame
            // 
            this.buttonJoinGame.Location = new System.Drawing.Point(144, 4);
            this.buttonJoinGame.Name = "buttonJoinGame";
            this.buttonJoinGame.Size = new System.Drawing.Size(141, 33);
            this.buttonJoinGame.TabIndex = 1;
            this.buttonJoinGame.Text = "Połącz";
            this.buttonJoinGame.UseVisualStyleBackColor = true;
            this.buttonJoinGame.Click += new System.EventHandler(this.ButtonTypeClick);
            // 
            // panelHost
            // 
            this.panelHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHost.Controls.Add(this.label1);
            this.panelHost.Controls.Add(this.numericUpDownTurnSpyCells);
            this.panelHost.Controls.Add(this.label3);
            this.panelHost.Controls.Add(this.labelStartBuildings);
            this.panelHost.Controls.Add(this.checkedListBoxStartBuildings);
            this.panelHost.Controls.Add(this.checkBoxSameMap);
            this.panelHost.Controls.Add(this.buttonCreate);
            this.panelHost.Controls.Add(this.nudSize);
            this.panelHost.Controls.Add(this.labelMapSize);
            this.panelHost.Controls.Add(this.label4);
            this.panelHost.Controls.Add(this.nudPeople);
            this.panelHost.Controls.Add(this.labelResPeople);
            this.panelHost.Controls.Add(this.nudCash);
            this.panelHost.Controls.Add(this.labelResCash);
            this.panelHost.Controls.Add(this.nudEnergy);
            this.panelHost.Controls.Add(this.labelStartRes);
            this.panelHost.Controls.Add(this.labelResEnergy);
            this.panelHost.Controls.Add(this.labelBlackLine1);
            this.panelHost.Controls.Add(this.textBoxYourNickHost);
            this.panelHost.Controls.Add(this.labelNickLabel);
            this.panelHost.Controls.Add(this.labelYourIP);
            this.panelHost.Controls.Add(this.labelIPlabel);
            this.panelHost.Location = new System.Drawing.Point(12, 80);
            this.panelHost.Name = "panelHost";
            this.panelHost.Size = new System.Drawing.Size(288, 467);
            this.panelHost.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 23);
            this.label1.TabIndex = 24;
            this.label1.Text = "Pokazywanie pól przez tury (szpieg) : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownTurnSpyCells
            // 
            this.numericUpDownTurnSpyCells.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownTurnSpyCells.Location = new System.Drawing.Point(207, 379);
            this.numericUpDownTurnSpyCells.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTurnSpyCells.Name = "numericUpDownTurnSpyCells";
            this.numericUpDownTurnSpyCells.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDownTurnSpyCells.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownTurnSpyCells.TabIndex = 23;
            this.numericUpDownTurnSpyCells.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(-1, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 1);
            this.label3.TabIndex = 16;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStartBuildings
            // 
            this.labelStartBuildings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStartBuildings.Location = new System.Drawing.Point(3, 218);
            this.labelStartBuildings.Name = "labelStartBuildings";
            this.labelStartBuildings.Size = new System.Drawing.Size(280, 20);
            this.labelStartBuildings.TabIndex = 22;
            this.labelStartBuildings.Text = "Początkowe budynki : ";
            this.labelStartBuildings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkedListBoxStartBuildings
            // 
            this.checkedListBoxStartBuildings.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBoxStartBuildings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBoxStartBuildings.FormattingEnabled = true;
            this.checkedListBoxStartBuildings.Location = new System.Drawing.Point(6, 239);
            this.checkedListBoxStartBuildings.Name = "checkedListBoxStartBuildings";
            this.checkedListBoxStartBuildings.Size = new System.Drawing.Size(270, 92);
            this.checkedListBoxStartBuildings.TabIndex = 21;
            // 
            // checkBoxSameMap
            // 
            this.checkBoxSameMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSameMap.Location = new System.Drawing.Point(18, 400);
            this.checkBoxSameMap.Name = "checkBoxSameMap";
            this.checkBoxSameMap.Size = new System.Drawing.Size(258, 24);
            this.checkBoxSameMap.TabIndex = 20;
            this.checkBoxSameMap.Text = "Gracze mają takie same mapy";
            this.checkBoxSameMap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxSameMap.UseVisualStyleBackColor = true;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreate.Location = new System.Drawing.Point(3, 426);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(280, 33);
            this.buttonCreate.TabIndex = 5;
            this.buttonCreate.Text = "Stwórz";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // nudSize
            // 
            this.nudSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSize.Location = new System.Drawing.Point(113, 353);
            this.nudSize.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudSize.Name = "nudSize";
            this.nudSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nudSize.Size = new System.Drawing.Size(163, 20);
            this.nudSize.TabIndex = 19;
            this.nudSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // labelMapSize
            // 
            this.labelMapSize.Location = new System.Drawing.Point(6, 350);
            this.labelMapSize.Name = "labelMapSize";
            this.labelMapSize.Size = new System.Drawing.Size(101, 23);
            this.labelMapSize.TabIndex = 18;
            this.labelMapSize.Text = "Wielkość mapy  :";
            this.labelMapSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(3, 334);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(280, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Dane Gry :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudPeople
            // 
            this.nudPeople.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPeople.Location = new System.Drawing.Point(103, 181);
            this.nudPeople.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPeople.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPeople.Name = "nudPeople";
            this.nudPeople.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nudPeople.Size = new System.Drawing.Size(173, 20);
            this.nudPeople.TabIndex = 15;
            this.nudPeople.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // labelResPeople
            // 
            this.labelResPeople.Location = new System.Drawing.Point(15, 178);
            this.labelResPeople.Name = "labelResPeople";
            this.labelResPeople.Size = new System.Drawing.Size(79, 23);
            this.labelResPeople.TabIndex = 14;
            this.labelResPeople.Text = "Ludzie : ";
            this.labelResPeople.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudCash
            // 
            this.nudCash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudCash.Location = new System.Drawing.Point(103, 155);
            this.nudCash.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCash.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCash.Name = "nudCash";
            this.nudCash.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nudCash.Size = new System.Drawing.Size(173, 20);
            this.nudCash.TabIndex = 13;
            this.nudCash.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // labelResCash
            // 
            this.labelResCash.Location = new System.Drawing.Point(15, 152);
            this.labelResCash.Name = "labelResCash";
            this.labelResCash.Size = new System.Drawing.Size(79, 23);
            this.labelResCash.TabIndex = 12;
            this.labelResCash.Text = "Fundusze : ";
            this.labelResCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudEnergy
            // 
            this.nudEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudEnergy.Location = new System.Drawing.Point(103, 129);
            this.nudEnergy.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudEnergy.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudEnergy.Name = "nudEnergy";
            this.nudEnergy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nudEnergy.Size = new System.Drawing.Size(173, 20);
            this.nudEnergy.TabIndex = 11;
            this.nudEnergy.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // labelStartRes
            // 
            this.labelStartRes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStartRes.Location = new System.Drawing.Point(3, 94);
            this.labelStartRes.Name = "labelStartRes";
            this.labelStartRes.Size = new System.Drawing.Size(280, 23);
            this.labelStartRes.TabIndex = 10;
            this.labelStartRes.Text = "Początkowe zasoby : ";
            this.labelStartRes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelResEnergy
            // 
            this.labelResEnergy.Location = new System.Drawing.Point(15, 126);
            this.labelResEnergy.Name = "labelResEnergy";
            this.labelResEnergy.Size = new System.Drawing.Size(79, 23);
            this.labelResEnergy.TabIndex = 9;
            this.labelResEnergy.Text = "Energia : ";
            this.labelResEnergy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBlackLine1
            // 
            this.labelBlackLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBlackLine1.BackColor = System.Drawing.Color.Black;
            this.labelBlackLine1.Location = new System.Drawing.Point(-1, 90);
            this.labelBlackLine1.Name = "labelBlackLine1";
            this.labelBlackLine1.Size = new System.Drawing.Size(288, 1);
            this.labelBlackLine1.TabIndex = 8;
            this.labelBlackLine1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxYourNickHost
            // 
            this.textBoxYourNickHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYourNickHost.Location = new System.Drawing.Point(103, 55);
            this.textBoxYourNickHost.Name = "textBoxYourNickHost";
            this.textBoxYourNickHost.Size = new System.Drawing.Size(173, 20);
            this.textBoxYourNickHost.TabIndex = 7;
            this.textBoxYourNickHost.Text = "ServerName";
            // 
            // labelNickLabel
            // 
            this.labelNickLabel.Location = new System.Drawing.Point(15, 52);
            this.labelNickLabel.Name = "labelNickLabel";
            this.labelNickLabel.Size = new System.Drawing.Size(79, 23);
            this.labelNickLabel.TabIndex = 6;
            this.labelNickLabel.Text = "Twoja nazwa : ";
            this.labelNickLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelYourIP
            // 
            this.labelYourIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelYourIP.Location = new System.Drawing.Point(100, 15);
            this.labelYourIP.Name = "labelYourIP";
            this.labelYourIP.Size = new System.Drawing.Size(176, 23);
            this.labelYourIP.TabIndex = 1;
            this.labelYourIP.Text = "255.255.255.255";
            this.labelYourIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelIPlabel
            // 
            this.labelIPlabel.Location = new System.Drawing.Point(15, 15);
            this.labelIPlabel.Name = "labelIPlabel";
            this.labelIPlabel.Size = new System.Drawing.Size(79, 23);
            this.labelIPlabel.TabIndex = 0;
            this.labelIPlabel.Text = "Twoje IP : ";
            this.labelIPlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelJoin
            // 
            this.panelJoin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelJoin.Controls.Add(this.textBoxGameIP);
            this.panelJoin.Controls.Add(this.buttonConnectGame);
            this.panelJoin.Controls.Add(this.textBoxYourNickJoin);
            this.panelJoin.Controls.Add(this.label14);
            this.panelJoin.Controls.Add(this.labelIP);
            this.panelJoin.Location = new System.Drawing.Point(12, 554);
            this.panelJoin.Name = "panelJoin";
            this.panelJoin.Size = new System.Drawing.Size(288, 135);
            this.panelJoin.TabIndex = 21;
            // 
            // textBoxGameIP
            // 
            this.textBoxGameIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGameIP.Location = new System.Drawing.Point(103, 17);
            this.textBoxGameIP.Name = "textBoxGameIP";
            this.textBoxGameIP.Size = new System.Drawing.Size(173, 20);
            this.textBoxGameIP.TabIndex = 8;
            this.textBoxGameIP.Text = "10.0.0.102";
            // 
            // buttonConnectGame
            // 
            this.buttonConnectGame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnectGame.Location = new System.Drawing.Point(3, 94);
            this.buttonConnectGame.Name = "buttonConnectGame";
            this.buttonConnectGame.Size = new System.Drawing.Size(280, 33);
            this.buttonConnectGame.TabIndex = 5;
            this.buttonConnectGame.Text = "Połącz : ";
            this.buttonConnectGame.UseVisualStyleBackColor = true;
            this.buttonConnectGame.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBoxYourNickJoin
            // 
            this.textBoxYourNickJoin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYourNickJoin.Location = new System.Drawing.Point(103, 55);
            this.textBoxYourNickJoin.Name = "textBoxYourNickJoin";
            this.textBoxYourNickJoin.Size = new System.Drawing.Size(173, 20);
            this.textBoxYourNickJoin.TabIndex = 7;
            this.textBoxYourNickJoin.Text = "ClientName";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(15, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 23);
            this.label14.TabIndex = 6;
            this.label14.Text = "Twoja nazwa : ";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelIP
            // 
            this.labelIP.Location = new System.Drawing.Point(15, 15);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(79, 23);
            this.labelIP.TabIndex = 0;
            this.labelIP.Text = "IP hosta : ";
            this.labelIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSingle
            // 
            this.buttonSingle.Location = new System.Drawing.Point(78, 41);
            this.buttonSingle.Name = "buttonSingle";
            this.buttonSingle.Size = new System.Drawing.Size(141, 33);
            this.buttonSingle.TabIndex = 22;
            this.buttonSingle.Text = "Graj z AI";
            this.buttonSingle.UseVisualStyleBackColor = true;
            this.buttonSingle.Click += new System.EventHandler(this.buttonSingle_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonHostGame);
            this.panel1.Controls.Add(this.buttonSingle);
            this.panel1.Controls.Add(this.buttonJoinGame);
            this.panel1.Location = new System.Drawing.Point(12, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 77);
            this.panel1.TabIndex = 23;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 698);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelJoin);
            this.Controls.Add(this.panelHost);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Islands";
            this.panelHost.ResumeLayout(false);
            this.panelHost.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTurnSpyCells)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeople)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnergy)).EndInit();
            this.panelJoin.ResumeLayout(false);
            this.panelJoin.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonHostGame;
        private System.Windows.Forms.Button buttonJoinGame;
        private System.Windows.Forms.Panel panelHost;
        private System.Windows.Forms.Label labelBlackLine1;
        private System.Windows.Forms.TextBox textBoxYourNickHost;
        private System.Windows.Forms.Label labelNickLabel;
        private System.Windows.Forms.Label labelYourIP;
        private System.Windows.Forms.Label labelIPlabel;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Label labelStartRes;
        private System.Windows.Forms.Label labelResEnergy;
        private System.Windows.Forms.CheckBox checkBoxSameMap;
        private System.Windows.Forms.NumericUpDown nudSize;
        private System.Windows.Forms.Label labelMapSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudPeople;
        private System.Windows.Forms.Label labelResPeople;
        private System.Windows.Forms.NumericUpDown nudCash;
        private System.Windows.Forms.Label labelResCash;
        private System.Windows.Forms.NumericUpDown nudEnergy;
        private System.Windows.Forms.Panel panelJoin;
        private System.Windows.Forms.Button buttonConnectGame;
        private System.Windows.Forms.TextBox textBoxYourNickJoin;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.TextBox textBoxGameIP;
        private System.Windows.Forms.Label labelStartBuildings;
        private System.Windows.Forms.CheckedListBox checkedListBoxStartBuildings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownTurnSpyCells;
        private System.Windows.Forms.Button buttonSingle;
        private System.Windows.Forms.Panel panel1;
    }
}

