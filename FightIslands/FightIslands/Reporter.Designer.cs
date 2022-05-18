namespace FightIslands
{
    partial class Reporter
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.panelReport = new System.Windows.Forms.Panel();
            this.labelReport = new System.Windows.Forms.Label();
            this.panelReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(12, 165);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(540, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Zamknij";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // panelReport
            // 
            this.panelReport.AutoScroll = true;
            this.panelReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReport.Controls.Add(this.labelReport);
            this.panelReport.Location = new System.Drawing.Point(15, 12);
            this.panelReport.Name = "panelReport";
            this.panelReport.Size = new System.Drawing.Size(537, 147);
            this.panelReport.TabIndex = 5;
            // 
            // labelReport
            // 
            this.labelReport.AutoSize = true;
            this.labelReport.Location = new System.Drawing.Point(3, 3);
            this.labelReport.MaximumSize = new System.Drawing.Size(515, 0);
            this.labelReport.MinimumSize = new System.Drawing.Size(515, 120);
            this.labelReport.Name = "labelReport";
            this.labelReport.Size = new System.Drawing.Size(515, 120);
            this.labelReport.TabIndex = 5;
            this.labelReport.Text = "RAPORT";
            this.labelReport.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Reporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(564, 200);
            this.Controls.Add(this.panelReport);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "Reporter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Reporter";
            this.panelReport.ResumeLayout(false);
            this.panelReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panelReport;
        private System.Windows.Forms.Label labelReport;
    }
}