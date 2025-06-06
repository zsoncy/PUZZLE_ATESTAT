namespace WindowsFormsApplication1
{
    partial class frmToplista
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.Nev = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lepesszam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Jatektipus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Nev,
            this.Ido,
            this.Lepesszam,
            this.Jatektipus});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(25, 23);
            this.listView1.Margin = new System.Windows.Forms.Padding(2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(432, 370);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Nev
            // 
            this.Nev.Text = "Név";
            this.Nev.Width = 146;
            // 
            // Ido
            // 
            this.Ido.Text = "Idő";
            this.Ido.Width = 116;
            // 
            // Lepesszam
            // 
            this.Lepesszam.Text = "Lépésszám";
            this.Lepesszam.Width = 91;
            // 
            // Jatektipus
            // 
            this.Jatektipus.Text = "Játéktípus";
            this.Jatektipus.Width = 70;
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.button3.Location = new System.Drawing.Point(475, 299);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 41);
            this.button3.TabIndex = 5;
            this.button3.Text = "Kilépés";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(475, 235);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 41);
            this.button2.TabIndex = 4;
            this.button2.Text = "Új Játék";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // frmToplista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 410);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmToplista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Toplista";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader Nev;
        private System.Windows.Forms.ColumnHeader Lepesszam;
        private System.Windows.Forms.ColumnHeader Ido;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ColumnHeader Jatektipus;


    }
}