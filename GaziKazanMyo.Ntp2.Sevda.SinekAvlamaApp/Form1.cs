using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GaziKazanMyo.Ntp2.Sevda.SinekAvlamaApp
{
    public partial class FrmSinek : Form
    {
        Button sinekbtn = new Button();
        Random rdm = new Random();
        static byte sure = 90;
        static int toplam;
        public FrmSinek()
        {
            InitializeComponent();
        }
        private void sinekuretme_Tick(object sender, EventArgs e)
        {
            sinekbtn = new Button();
            sinekbtn.Size = new Size(70, 70);
            sinekbtn.Location = new Point(rdm.Next(this.ClientSize.Width - sinekbtn.Width),rdm.Next(panel1.Height, this.ClientSize.Height - sinekbtn.Height));
            sinekbtn.Text = rdm.Next(1, 100).ToString();
            sinekbtn.BackgroundImage = imagelist.Images[0];
            sinekbtn.BackgroundImageLayout = ImageLayout.Stretch;
            sinekbtn.TextAlign = ContentAlignment.TopLeft;
            sinekbtn.Font = new Font(sinekbtn.Font.Name, sinekbtn.Font.Size, FontStyle.Bold);
            this.Controls.Add(sinekbtn);
            sinekbtn.Click += Sinek_Click;
        }

        private void Sinek_Click(object sender, EventArgs e)
        {
            sinekbtn = (Button)sender;
            sinekbtn.Dispose();
            toplam += int.Parse(sinekbtn.Text);
            labelskor.Text = toplam.ToString();
        }
        private void FrmSinek_Load(object sender, EventArgs e)
        {
            labelsure.Text = sure.ToString();
            sinekuretme.Start();
            gerisayim.Start();
        }

        private void gerisayim_Tick(object sender, EventArgs e)
        {
            sure--;
            labelsure.Text = sure.ToString();
            if (sure == 0)
            {
                gerisayim.Stop();
                sinekuretme.Stop();
                Skor_Kaydetme(labelskor.Text);
                DialogResult soru = new DialogResult();
                soru = MessageBox.Show($"Skorunuz:{labelskor.Text}\nYeniden Oynamak istermisiniz?", "Oyun Bitti", MessageBoxButtons.YesNo);
                if (soru == DialogResult.Yes) Application.Restart();
                else Application.Exit();
            }

        }

        private void Skor_Kaydetme(string skorkayit) 
        {
            FileStream fs = new FileStream("skorkayit.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(DateTime.Now + " " + skorkayit + "\n");
            fs.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
