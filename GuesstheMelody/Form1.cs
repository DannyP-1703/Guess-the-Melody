using System;
using System.Windows.Forms;

namespace GuesstheMelody
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //btnPlay
        {
            fGame fg = new fGame();
            fg.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) //bntSettings
        {
            fSettings fs = new fSettings();
            fs.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e) //btnExit
        {
            this.Close();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            Victorina.ReadSettings();
            Victorina.ReadMusic();
        }
    }
}
