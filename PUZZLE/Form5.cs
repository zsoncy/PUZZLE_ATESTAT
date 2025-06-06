using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmElonezet : Form
    {
        public frmElonezet()
        {
            InitializeComponent();
        }
        Image kep;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            kep = Image.FromFile("tenger.png");
            e.Graphics.DrawImage(kep, new Rectangle(0,0,500,500));
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            kep = Image.FromFile("tehen.png");
            e.Graphics.DrawImage(kep, new Rectangle(0,0,500,500));
        }
    }
}
