using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FightIslands
{
    public partial class Reporter : Form
    {
        public Reporter()
        {
            InitializeComponent();
        }

        public void SetReportText(string repText)
        {
            labelReport.Text = repText;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetMiddlePoint(Point loc, Size siz)
        {
            int middleX = loc.X + Math.Round(siz.Width / 2f).ToInt();
            int middleY = loc.Y + Math.Round(siz.Height / 2f).ToInt();

            int halfSizeX = Math.Round(this.Size.Width / 2f).ToInt();
            int halfSizeY = Math.Round(this.Size.Height / 2f).ToInt();

            int actX = middleX - halfSizeX;
            int actY = middleY - halfSizeY;

            this.Location = new Point(actX, actY);
        }

    }
}
