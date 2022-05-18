namespace FightIslands
{
    partial class Game
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
            this.components = new System.ComponentModel.Container();
            this.panelScrollMap = new System.Windows.Forms.Panel();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.labelEnergy = new System.Windows.Forms.Label();
            this.labelCash = new System.Windows.Forms.Label();
            this.labelPeople = new System.Windows.Forms.Label();
            this.buttonEndTurn = new System.Windows.Forms.Button();
            this.panelBuildings = new System.Windows.Forms.Panel();
            this.buttonInfo = new System.Windows.Forms.CheckBox();
            this.buttonAttack = new System.Windows.Forms.CheckBox();
            this.buttonZoomMinus = new System.Windows.Forms.Button();
            this.buttonZoomPlus = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timerMap = new System.Windows.Forms.Timer(this.components);
            this.toolTipMap = new System.Windows.Forms.ToolTip(this.components);
            this.panelIslandInfo = new System.Windows.Forms.Panel();
            this.labelInfoResources = new System.Windows.Forms.Label();
            this.panelCellInfo = new System.Windows.Forms.Panel();
            this.buttonDestroyBuilding = new System.Windows.Forms.Button();
            this.buttonTurnOffBuilding = new System.Windows.Forms.Button();
            this.buttonUpgradeBuilding = new System.Windows.Forms.Button();
            this.labelInfoCell = new System.Windows.Forms.Label();
            this.panelPlayerColor = new System.Windows.Forms.Panel();
            this.panelWeapons = new System.Windows.Forms.Panel();
            this.timerExplosionAnim = new System.Windows.Forms.Timer(this.components);
            this.panelScrollMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            this.panelIslandInfo.SuspendLayout();
            this.panelCellInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelScrollMap
            // 
            this.panelScrollMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelScrollMap.BackColor = System.Drawing.Color.Blue;
            this.panelScrollMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelScrollMap.Controls.Add(this.pictureBoxMap);
            this.panelScrollMap.Location = new System.Drawing.Point(12, 13);
            this.panelScrollMap.Name = "panelScrollMap";
            this.panelScrollMap.Size = new System.Drawing.Size(680, 636);
            this.panelScrollMap.TabIndex = 0;
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(500, 500);
            this.pictureBoxMap.TabIndex = 0;
            this.pictureBoxMap.TabStop = false;
            this.pictureBoxMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMap_MouseDown);
            this.pictureBoxMap.MouseEnter += new System.EventHandler(this.pictureBoxMap_MouseEnter);
            this.pictureBoxMap.MouseLeave += new System.EventHandler(this.pictureBoxMap_MouseLeave);
            this.pictureBoxMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMap_MouseMove);
            this.pictureBoxMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMap_MouseUp);
            // 
            // labelEnergy
            // 
            this.labelEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEnergy.Location = new System.Drawing.Point(701, 13);
            this.labelEnergy.Name = "labelEnergy";
            this.labelEnergy.Size = new System.Drawing.Size(171, 33);
            this.labelEnergy.TabIndex = 0;
            this.labelEnergy.Text = "Energia : 9999999";
            this.labelEnergy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCash
            // 
            this.labelCash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCash.Location = new System.Drawing.Point(701, 46);
            this.labelCash.Name = "labelCash";
            this.labelCash.Size = new System.Drawing.Size(171, 33);
            this.labelCash.TabIndex = 1;
            this.labelCash.Text = "Fundusze : 999 ";
            this.labelCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPeople
            // 
            this.labelPeople.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPeople.Location = new System.Drawing.Point(701, 79);
            this.labelPeople.Name = "labelPeople";
            this.labelPeople.Size = new System.Drawing.Size(171, 33);
            this.labelPeople.TabIndex = 2;
            this.labelPeople.Text = "Ludzie : 9999/9999";
            this.labelPeople.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonEndTurn
            // 
            this.buttonEndTurn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEndTurn.BackColor = System.Drawing.SystemColors.Control;
            this.buttonEndTurn.Location = new System.Drawing.Point(701, 614);
            this.buttonEndTurn.Name = "buttonEndTurn";
            this.buttonEndTurn.Size = new System.Drawing.Size(171, 35);
            this.buttonEndTurn.TabIndex = 4;
            this.buttonEndTurn.TabStop = false;
            this.buttonEndTurn.Text = "Zakończ turę";
            this.buttonEndTurn.UseVisualStyleBackColor = false;
            this.buttonEndTurn.Click += new System.EventHandler(this.buttonEndTurn_Click);
            // 
            // panelBuildings
            // 
            this.panelBuildings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBuildings.AutoScroll = true;
            this.panelBuildings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBuildings.Location = new System.Drawing.Point(701, 268);
            this.panelBuildings.Name = "panelBuildings";
            this.panelBuildings.Size = new System.Drawing.Size(171, 224);
            this.panelBuildings.TabIndex = 7;
            this.panelBuildings.Visible = false;
            // 
            // buttonInfo
            // 
            this.buttonInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInfo.Appearance = System.Windows.Forms.Appearance.Button;
            this.buttonInfo.Location = new System.Drawing.Point(701, 532);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(171, 35);
            this.buttonInfo.TabIndex = 8;
            this.buttonInfo.TabStop = false;
            this.buttonInfo.Tag = "INFO";
            this.buttonInfo.Text = "Wyspa";
            this.buttonInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.ButtonMainCheck);
            // 
            // buttonAttack
            // 
            this.buttonAttack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAttack.Appearance = System.Windows.Forms.Appearance.Button;
            this.buttonAttack.Location = new System.Drawing.Point(701, 573);
            this.buttonAttack.Name = "buttonAttack";
            this.buttonAttack.Size = new System.Drawing.Size(171, 35);
            this.buttonAttack.TabIndex = 9;
            this.buttonAttack.TabStop = false;
            this.buttonAttack.Tag = "ATTACK";
            this.buttonAttack.Text = "Atakuj";
            this.buttonAttack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonAttack.UseVisualStyleBackColor = true;
            this.buttonAttack.Click += new System.EventHandler(this.ButtonMainCheck);
            // 
            // buttonZoomMinus
            // 
            this.buttonZoomMinus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonZoomMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.buttonZoomMinus.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonZoomMinus.Location = new System.Drawing.Point(701, 498);
            this.buttonZoomMinus.Name = "buttonZoomMinus";
            this.buttonZoomMinus.Size = new System.Drawing.Size(84, 28);
            this.buttonZoomMinus.TabIndex = 10;
            this.buttonZoomMinus.TabStop = false;
            this.buttonZoomMinus.Tag = "-";
            this.buttonZoomMinus.Text = "Zoom-";
            this.buttonZoomMinus.UseVisualStyleBackColor = true;
            this.buttonZoomMinus.Click += new System.EventHandler(this.ZoomClick);
            // 
            // buttonZoomPlus
            // 
            this.buttonZoomPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonZoomPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.buttonZoomPlus.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonZoomPlus.Location = new System.Drawing.Point(790, 498);
            this.buttonZoomPlus.Name = "buttonZoomPlus";
            this.buttonZoomPlus.Size = new System.Drawing.Size(81, 28);
            this.buttonZoomPlus.TabIndex = 11;
            this.buttonZoomPlus.TabStop = false;
            this.buttonZoomPlus.Tag = "+";
            this.buttonZoomPlus.Text = "Zoom+";
            this.buttonZoomPlus.UseVisualStyleBackColor = true;
            this.buttonZoomPlus.Click += new System.EventHandler(this.ZoomClick);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 5000;
            this.toolTip.AutoPopDelay = 50000;
            this.toolTip.InitialDelay = 5;
            this.toolTip.ReshowDelay = 5;
            // 
            // timerMap
            // 
            this.timerMap.Enabled = true;
            this.timerMap.Tick += new System.EventHandler(this.timerMap_Tick);
            // 
            // toolTipMap
            // 
            this.toolTipMap.AutomaticDelay = 0;
            this.toolTipMap.ShowAlways = true;
            // 
            // panelIslandInfo
            // 
            this.panelIslandInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelIslandInfo.AutoScroll = true;
            this.panelIslandInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelIslandInfo.Controls.Add(this.labelInfoResources);
            this.panelIslandInfo.Location = new System.Drawing.Point(701, 115);
            this.panelIslandInfo.Name = "panelIslandInfo";
            this.panelIslandInfo.Size = new System.Drawing.Size(171, 151);
            this.panelIslandInfo.TabIndex = 8;
            this.panelIslandInfo.Visible = false;
            // 
            // labelInfoResources
            // 
            this.labelInfoResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfoResources.Location = new System.Drawing.Point(0, 0);
            this.labelInfoResources.Name = "labelInfoResources";
            this.labelInfoResources.Size = new System.Drawing.Size(169, 149);
            this.labelInfoResources.TabIndex = 0;
            this.labelInfoResources.Text = "Zasoby : \r\n\r\nEnergia :\r\nŁącznie na turę :\r\nUzyskiwana : \r\nUtracana :\r\n\r\nFundusze " +
    ":\r\nŁącznie na turę : \r\nUzyskiwane : \r\nUtracane : ";
            this.labelInfoResources.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelCellInfo
            // 
            this.panelCellInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCellInfo.AutoScroll = true;
            this.panelCellInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCellInfo.Controls.Add(this.buttonDestroyBuilding);
            this.panelCellInfo.Controls.Add(this.buttonTurnOffBuilding);
            this.panelCellInfo.Controls.Add(this.buttonUpgradeBuilding);
            this.panelCellInfo.Controls.Add(this.labelInfoCell);
            this.panelCellInfo.Location = new System.Drawing.Point(701, 115);
            this.panelCellInfo.Name = "panelCellInfo";
            this.panelCellInfo.Size = new System.Drawing.Size(171, 377);
            this.panelCellInfo.TabIndex = 9;
            this.panelCellInfo.Visible = false;
            // 
            // buttonDestroyBuilding
            // 
            this.buttonDestroyBuilding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDestroyBuilding.Location = new System.Drawing.Point(3, 349);
            this.buttonDestroyBuilding.Name = "buttonDestroyBuilding";
            this.buttonDestroyBuilding.Size = new System.Drawing.Size(163, 23);
            this.buttonDestroyBuilding.TabIndex = 4;
            this.buttonDestroyBuilding.Tag = "Z";
            this.buttonDestroyBuilding.Text = "Zniszcz budynek";
            this.buttonDestroyBuilding.UseVisualStyleBackColor = true;
            this.buttonDestroyBuilding.Click += new System.EventHandler(this.CellInfoButton);
            // 
            // buttonTurnOffBuilding
            // 
            this.buttonTurnOffBuilding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTurnOffBuilding.Location = new System.Drawing.Point(3, 320);
            this.buttonTurnOffBuilding.Name = "buttonTurnOffBuilding";
            this.buttonTurnOffBuilding.Size = new System.Drawing.Size(163, 23);
            this.buttonTurnOffBuilding.TabIndex = 3;
            this.buttonTurnOffBuilding.Tag = "W";
            this.buttonTurnOffBuilding.Text = "Wyłącz budynek";
            this.buttonTurnOffBuilding.UseVisualStyleBackColor = true;
            this.buttonTurnOffBuilding.Visible = false;
            this.buttonTurnOffBuilding.Click += new System.EventHandler(this.CellInfoButton);
            // 
            // buttonUpgradeBuilding
            // 
            this.buttonUpgradeBuilding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUpgradeBuilding.Location = new System.Drawing.Point(3, 291);
            this.buttonUpgradeBuilding.Name = "buttonUpgradeBuilding";
            this.buttonUpgradeBuilding.Size = new System.Drawing.Size(163, 23);
            this.buttonUpgradeBuilding.TabIndex = 2;
            this.buttonUpgradeBuilding.Tag = "U";
            this.buttonUpgradeBuilding.Text = "Ulepsz budynek";
            this.buttonUpgradeBuilding.UseVisualStyleBackColor = true;
            this.buttonUpgradeBuilding.Click += new System.EventHandler(this.CellInfoButton);
            // 
            // labelInfoCell
            // 
            this.labelInfoCell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInfoCell.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelInfoCell.Location = new System.Drawing.Point(0, 0);
            this.labelInfoCell.Name = "labelInfoCell";
            this.labelInfoCell.Size = new System.Drawing.Size(169, 288);
            this.labelInfoCell.TabIndex = 1;
            this.labelInfoCell.Text = "Cell : ";
            this.labelInfoCell.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelPlayerColor
            // 
            this.panelPlayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPlayerColor.BackColor = System.Drawing.Color.Lime;
            this.panelPlayerColor.Location = new System.Drawing.Point(7, 8);
            this.panelPlayerColor.Name = "panelPlayerColor";
            this.panelPlayerColor.Size = new System.Drawing.Size(690, 646);
            this.panelPlayerColor.TabIndex = 12;
            // 
            // panelWeapons
            // 
            this.panelWeapons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWeapons.AutoScroll = true;
            this.panelWeapons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWeapons.Location = new System.Drawing.Point(701, 115);
            this.panelWeapons.Name = "panelWeapons";
            this.panelWeapons.Size = new System.Drawing.Size(171, 336);
            this.panelWeapons.TabIndex = 8;
            this.panelWeapons.Visible = false;
            // 
            // timerExplosionAnim
            // 
            this.timerExplosionAnim.Interval = 50;
            this.timerExplosionAnim.Tick += new System.EventHandler(this.timerExplosionAnim_Tick);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.panelIslandInfo);
            this.Controls.Add(this.panelBuildings);
            this.Controls.Add(this.panelCellInfo);
            this.Controls.Add(this.panelScrollMap);
            this.Controls.Add(this.panelWeapons);
            this.Controls.Add(this.buttonZoomMinus);
            this.Controls.Add(this.buttonAttack);
            this.Controls.Add(this.buttonZoomPlus);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.buttonEndTurn);
            this.Controls.Add(this.labelPeople);
            this.Controls.Add(this.labelCash);
            this.Controls.Add(this.labelEnergy);
            this.Controls.Add(this.panelPlayerColor);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Game";
            this.Text = "Fight Islands";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Game_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.Game_ResizeEnd);
            this.Resize += new System.EventHandler(this.Game_Resize);
            this.panelScrollMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            this.panelIslandInfo.ResumeLayout(false);
            this.panelCellInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelScrollMap;
        private System.Windows.Forms.Label labelEnergy;
        private System.Windows.Forms.Label labelCash;
        private System.Windows.Forms.Label labelPeople;
        private System.Windows.Forms.Button buttonEndTurn;
        private System.Windows.Forms.Panel panelBuildings;
        private System.Windows.Forms.CheckBox buttonInfo;
        private System.Windows.Forms.CheckBox buttonAttack;
        private System.Windows.Forms.PictureBox pictureBoxMap;
        private System.Windows.Forms.Button buttonZoomMinus;
        private System.Windows.Forms.Button buttonZoomPlus;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer timerMap;
        private System.Windows.Forms.ToolTip toolTipMap;
        private System.Windows.Forms.Panel panelIslandInfo;
        private System.Windows.Forms.Panel panelCellInfo;
        private System.Windows.Forms.Label labelInfoResources;
        private System.Windows.Forms.Label labelInfoCell;
        private System.Windows.Forms.Button buttonDestroyBuilding;
        private System.Windows.Forms.Button buttonTurnOffBuilding;
        private System.Windows.Forms.Button buttonUpgradeBuilding;
        private System.Windows.Forms.Panel panelPlayerColor;
        private System.Windows.Forms.Panel panelWeapons;
        private System.Windows.Forms.Timer timerExplosionAnim;
    }
}