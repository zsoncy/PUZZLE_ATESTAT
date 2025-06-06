using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using System.Linq;

namespace WindowsFormsApplication1
{
    public partial class frmJatek : Form
    {
        frmNyert fny = new frmNyert();
        frmToplista ft = new frmToplista();
        frmMent fm = new frmMent();
        struct lista
        {
            public string nev;
            public int lepes, min, sec,nxn;
        };
        lista[] toplista = new lista[10];
        int n;
        int toplistaN = 0;
        int keptrue = 0;
        Image kep;
        int[,] matrix = new int[50, 50];
        int nr, ido, lepesek;
        Font betu = new Font("arial", 15);
        Random rand = new Random();
        public frmJatek()
        {
            InitializeComponent();

            gomblathato(n);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (keptrue == 1)
            {

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        int sor, oszlop;
                        sor = (matrix[i, j] - 1) / n;
                        oszlop = (matrix[i, j] - 1) % n;

                        e.Graphics.DrawImage(kep, new Rectangle(j * (500/n), i * (500/n), (500/n), (500/n)),//hova rajzoljon a pictureboxra (adott téglalapba)
                                     new Rectangle(oszlop * (500 / n), sor * (500 / n), (500 / n), (500 / n)),//a "kep" képről honnan másoljon ki egy téglalap alakú területet
                                     GraphicsUnit.Pixel);

                    }

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {

                        e.Graphics.DrawString(Convert.ToString(matrix[i, j]), betu, Brushes.Black, j * (500 / n), i * (500 / n));
                        e.Graphics.DrawRectangle(Pens.Black, j * (500 / n), i * (500 / n), (500 / n), (500 / n));
                    }

            }
        }

        private void le(int j)
        {
            int id = 0, temp = matrix[n-1, j];
            for (int i = n-1; i > 0; i--)
            {
                id = matrix[i - 1, j];
                matrix[i - 1, j] = matrix[i, j];
                matrix[i, j] = id;
            }
            matrix[0, j] = temp;
            pictureBox1.Refresh();
        }

        private void fel(int j)
        {
            int id = 0, temp = matrix[0, j];
            for (int i = 0; i < n-1; i++)
            {
                id = matrix[i, j];
                matrix[i, j] = matrix[i + 1, j];
                matrix[i + 1, j] = id;
            }
            matrix[n-1, j] = temp;
            pictureBox1.Refresh();
        }

        private void jobb(int i)
        {
            int id = 0, temp = matrix[i, n-1];
            for (int j = n-1; j > 0; j--)
            {
                id = matrix[i, j - 1];
                matrix[i, j - 1] = matrix[i, j];
                matrix[i, j] = id;
            }
            matrix[i, 0] = temp;
            pictureBox1.Refresh();
        }

        private void bal(int i)
        {
            int id = 0, temp = matrix[i, 0];
            for (int j = 0; j < n-1; j++)
            {
                id = matrix[i, j];
                matrix[i, j] = matrix[i, j + 1];
                matrix[i, j + 1] = id;
            }
            matrix[i, n-1] = temp;
            pictureBox1.Refresh();
        }

       /* private bool nemkezdodottel()
        {
            int hiba = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n - 1; j++)
                    if (matrix[i, j] > matrix[i, j + 1] || matrix[i, j] != matrix[i + n, j]) hiba++;
            if (hiba == 0) return true;
            else 
            return false;
            
        }*/

        private void nyert()
        {
            int hiba = 0;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n-1; j++)
                    if (matrix[i, j] != matrix[i, j + 1]-1) hiba++;

            for (int i = 0; i < n-1; i++)
                for (int j = 0; j < n; j++)
                    if (matrix[i, j] != matrix[i + 1, j]-n) hiba++;

            if (hiba == 0)
            {
                fny.label5.Text = Convert.ToString(lepesek + 1);
                fny.label6.Text = Convert.ToString(ido / 60) + ":" + Convert.ToString(ido % 60);
                fny.label8.Text = Convert.ToString(n) + " x " + Convert.ToString(n);
                timer1.Enabled = false;
                //MessageBox.Show("Gratulálok!\nSikerült kiraknod a képet!");
                fny.ShowDialog();
                if (fny.DialogResult == DialogResult.OK)
                {
                    gyozelem(); 
                    ido = 0;
                    lepesek = 0;
                    n = 0;
                    gomblathato(n);
                    pictureBox1.Refresh();
                }
                else if (fny.DialogResult == DialogResult.Abort)
                {
                    Application.Exit();
                    gyozelem();
                    ido = 0;
                    lepesek = 0;
                    n = 0;
                    gomblathato(n);
                    pictureBox1.Refresh();
                }
                else if (fny.DialogResult == DialogResult.Cancel)
                {
                    gyozelem();
                    rendezes();
                    betolt();
                    kiir();
                    ido = 0;
                    lepesek = 0;
                    n = 0;
                    gomblathato(n);
                    pictureBox1.Refresh();
                    ft.ShowDialog();
                    if (ft.DialogResult == DialogResult.OK) ;
                    else if (ft.DialogResult == DialogResult.Abort) Application.Exit();
                }
            }
        }

        private void gyozelem()
        {

            try
            {

                toplista[toplistaN].nev = fny.textBox1.Text;
                toplista[toplistaN].min = ido / 60;
                toplista[toplistaN].sec = ido % 60;
                toplista[toplistaN].lepes = lepesek + 1;
                toplista[toplistaN].nxn = n;
                toplistaN++;
                rendezes();
                betolt();
                kiir();
                mentes();
                fny.textBox1.Text = "";
                fny.label5.Text = "-";
                fny.label6.Text = "-";
                fny.label8.Text = "-";
            }
            catch { };
        }

        private void mentes()
        {
            for (int k = 3; k <= 8; k++)
            {
                StreamWriter kimenet = new StreamWriter("lista" + k + ".txt");
                kimenet.WriteLine(toplistaN);
                for (int i = 0; i < toplistaN; i++)
                {
                    kimenet.WriteLine(toplista[i].nev);
                    kimenet.WriteLine(toplista[i].min);
                    kimenet.WriteLine(toplista[i].sec);
                    kimenet.WriteLine(toplista[i].lepes);
                    kimenet.WriteLine(toplista[i].nxn);
                }
                kimenet.Close();
            }
        }

        private void betolt()
        {
            for (int k = 3; k <= 8; k++)
            {
                StreamReader bemenet = new StreamReader("lista" + k + ".txt");
                toplistaN = Convert.ToInt32(bemenet.ReadLine());
                for (int i = 0; i < toplistaN; i++)
                {
                    toplista[i].nev = bemenet.ReadLine();
                    toplista[i].min = Convert.ToInt32(bemenet.ReadLine());
                    toplista[i].sec = Convert.ToInt32(bemenet.ReadLine());
                    toplista[i].lepes = Convert.ToInt32(bemenet.ReadLine());
                    toplista[i].nxn = Convert.ToInt32(bemenet.ReadLine());
                }
                bemenet.Close();
            }
        }

        private void kiir()
        {
            ft.listView1.Items.Clear();

            for (int i = 0; i < toplistaN; i++)
            {
                ListViewItem sor = new ListViewItem(toplista[i].nev);
                sor.SubItems.Add(Convert.ToString(toplista[i].min) + ":" + Convert.ToString(toplista[i].sec));
                sor.SubItems.Add(Convert.ToString(toplista[i].lepes));
                sor.SubItems.Add(Convert.ToString(toplista[i].nxn) + " x " + Convert.ToString(toplista[i].nxn));
                ft.listView1.Items.Add(sor);
            }
        }

        private void rendezes()
        {
            bool voltcsere = true;
            while (voltcsere)
            {
                voltcsere = false;
                    for (int i = 1; i < toplistaN; i++)
                        if (toplista[i].lepes < toplista[i - 1].lepes)
                        {
                            lista temp;
                            temp = toplista[i];
                            toplista[i] = toplista[i - 1];
                            toplista[i - 1] = temp;
                            voltcsere = true;
                        }
            }
            mentes();
            kiir();
        }

        private void gomblathato(int n)
        {
          
                foreach (object x in this.Controls)
                {
                    if (x is Button)
                    {
                        if ((Convert.ToInt32(((Button)x).Tag) <= n) && ((Button)x).Text == "↓")
                        {
                            ((Button)x).Visible = true;
                            ((Button)x).Location = new Point(500/n*(Convert.ToInt32(((Button)x).Tag)) , 31);
                        }
                        else if ((Convert.ToInt32(((Button)x).Tag) <= n) && ((Button)x).Text == "🠔")
                        {
                            ((Button)x).Visible = true;
                            ((Button)x).Location = new Point(578 , 500 / n * (Convert.ToInt32(((Button)x).Tag)));
                        }
                        else if ((Convert.ToInt32(((Button)x).Tag) <= n) && ((Button)x).Text == "↑")
                        {
                            ((Button)x).Visible = true;
                            ((Button)x).Location = new Point(500 / n * (Convert.ToInt32(((Button)x).Tag)), 576);
                        }
                        else if ((Convert.ToInt32(((Button)x).Tag) <= n) && ((Button)x).Text == "➔")
                        {
                            ((Button)x).Visible = true;
                            ((Button)x).Location = new Point(12 , 500 / n * (Convert.ToInt32(((Button)x).Tag)));
                        }
                        else
                            ((Button)x).Visible = false;
                    }
                }
                
               
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            le(0); nyert(); lepesek++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            le(1); nyert(); lepesek++; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            le(2); nyert(); lepesek++; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
           le(3); nyert(); lepesek++; 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            le(4); nyert(); lepesek++;
        }
        private void button21_Click(object sender, EventArgs e)
        {
            le(5); nyert(); lepesek++; 
        }
        private void button22_Click(object sender, EventArgs e)
        {
            le(6); nyert(); lepesek++;
        }
        private void button23_Click(object sender, EventArgs e)
        {
            le(7); nyert(); lepesek++; 
        }

        private void button11_Click(object sender, EventArgs e)
        {
            fel(7); nyert(); lepesek++; 
        }

        private void button12_Click(object sender, EventArgs e)
        {
            fel(6); nyert(); lepesek++; 
        }

        private void button13_Click(object sender, EventArgs e)
        {
            fel(5); nyert(); lepesek++; 
        }

        private void button14_Click(object sender, EventArgs e)
        {
            fel(4); nyert(); lepesek++; 
        }

        private void button15_Click(object sender, EventArgs e)
        {
            fel(3); nyert(); lepesek++;
        }
        private void button27_Click(object sender, EventArgs e)
        {
            fel(2); nyert(); lepesek++;
        }
        private void button28_Click(object sender, EventArgs e)
        {
            fel(1); nyert(); lepesek++; 
        }
        private void button29_Click(object sender, EventArgs e)
        {
            fel(0); nyert(); lepesek++; 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bal(0); nyert(); lepesek++; 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bal(1); nyert(); lepesek++; 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bal(2); nyert(); lepesek++; 
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bal(3); nyert(); lepesek++; 
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bal(4); nyert(); lepesek++; 
        }
        private void button24_Click_1(object sender, EventArgs e)
        {
            bal(5); nyert(); lepesek++; 
        }

        private void button25_Click(object sender, EventArgs e)
        {
            bal(6); nyert(); lepesek++; 
        }
        private void button26_Click(object sender, EventArgs e)
        {
            bal(7); nyert(); lepesek++; 
        }
        private void button32_Click(object sender, EventArgs e)
        {
            jobb(0); nyert(); lepesek++; 
        }
        private void button31_Click(object sender, EventArgs e)
        {
            jobb(1); nyert(); lepesek++; 
        }
        private void button30_Click(object sender, EventArgs e)
        {
            jobb(2); nyert(); lepesek++; 
        }
        private void button20_Click(object sender, EventArgs e)
        {
            jobb(3); nyert(); lepesek++; 
        }
        private void button19_Click(object sender, EventArgs e)
        {
            jobb(4); nyert(); lepesek++; 
        }
        private void button18_Click(object sender, EventArgs e)
        {
            jobb(5); nyert(); lepesek++; 
        }
        private void button17_Click(object sender, EventArgs e)
        {
            jobb(6); nyert(); lepesek++; 
        }
        private void button16_Click(object sender, EventArgs e)
        {
            jobb(7); nyert(); lepesek++; 
        }
        
        public void keveres(int mennyi)
        {

            for (int i = 0; i < mennyi; i++)
            {
                int elso = rand.Next(1, 6);
                switch (elso)
                {
                    case 1:

                        break;
                    case 2:
                        le(rand.Next(0, 8));
                        break;
                    case 3:
                        fel(rand.Next(0, 8));
                        break;
                    case 4:
                        bal(rand.Next(0, 8));
                        break;
                    case 5:
                        jobb(rand.Next(0, 8));
                        break;
                    
                }
            }
        }

        private void kilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lepesek>0)
            {
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.Yes) kimentes();
            }
            Application.Exit();
        }

        private void aJátékrólToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Görgetős Puzzle Játék\n\nKulcsár Gerzson 2023");
        }

        private void hogyanKellJátszaniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Az oszlopok illetve sorok végén található nyilak segtségével mozgathatjuk a sorokat illetve oszlopokokat, a nyilaknak megfelelő irányban!\nA cél a kép kirakása!\nJó szórakozást!");
        }

        private void toplistaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rendezes();
            betolt();
            kiir();
            ft.ShowDialog();
            if (ft.DialogResult == DialogResult.OK) ;
            else if (ft.DialogResult == DialogResult.Abort) Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ido++;
            Text = "PUZZLE - " + Convert.ToString(ido / 60) + ":" + Convert.ToString(ido % 60) + " ------ " + Convert.ToString(lepesek) + " lepes";
        }

        private void kimentes()
        {
            StreamWriter kimentes = new StreamWriter("ment.txt");
            kimentes.WriteLine(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    kimentes.WriteLine(matrix[i, j]);
            kimentes.Close();
        }

        private void mentésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kimentes();
        }

        private void betöltésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int kimentesN;
            StreamReader betolt = new StreamReader("ment.txt");
            kimentesN = Convert.ToInt32(betolt.ReadLine());
            if(kimentesN == n)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        matrix[i, j] = Convert.ToInt32(betolt.ReadLine());
                betolt.Close();
                pictureBox1.Refresh();
                timer1.Enabled = true;
            }
            else
            {
                betolt.Close();
                if(kimentesN!=0)MessageBox.Show("Ez a kimentés "+ kimentesN+"x" + kimentesN + " Puzzle játékra vonatkozott!");
                else MessageBox.Show("Nem létezik kimentés!");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (lepesek>0)
            {
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.Yes) kimentes();
            } 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            betolt();
            rendezes();
        }

        private void matrixfeltoltes(int n)
        {
            nr = 0;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = ++nr;
        }

        private void tengerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            n = 4;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tenger.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(50);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void farmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            n = 4;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tehen.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(50);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void tengerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            n = 3;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tenger.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(50);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;

        }

        private void farmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            n = 3;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tehen.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(50);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void tengerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            n = 5;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tenger.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(100);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void farmToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            n = 5;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tehen.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(100);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void tengerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            n = 6;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tenger.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(100);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void farmToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            n = 6;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tehen.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(100);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void tengerToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            n = 7;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tenger.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(200);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void farmToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            n = 7;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tehen.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(200);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void tengerToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            n = 8;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tenger.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(300);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void farmToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            n = 8;
            gomblathato(n);
            matrixfeltoltes(n);
            kep = Image.FromFile("tehen.png");
            keptrue = 1;
            pictureBox1.Refresh();
            keveres(300);
            ido = 0;
            lepesek = 0;
            timer1.Enabled = true;
        }

        private void képElőnézetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmElonezet fe = new frmElonezet();
            fe.ShowDialog();
        }




       
    }
}
