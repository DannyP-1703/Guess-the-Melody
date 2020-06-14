using System;
using System.Windows.Forms;

namespace GuesstheMelody
{
    public partial class fGame : Form
    {
        public fGame()
        {
            InitializeComponent();
        }

        Random rnd = new Random();

        int musicDuration = Victorina.musicDuration;

        private void NextSong()
        {
            if (Victorina.music.Count == 0) StopGame();
            else
            {
                musicDuration = Victorina.musicDuration;
                lblMusDur.Text = Convert.ToString(musicDuration);
                int num = rnd.Next(0, Victorina.music.Count);
                WMP.URL = Victorina.music[num];
                Victorina.music.RemoveAt(num);
                lblSongsAmount.Text = Victorina.music.Count.ToString();
            }
        }
        private void PauseGame()
        {
            WMP.Ctlcontrols.pause();
            timer1.Stop();
        }

        private void ContinueGame()
        {
            WMP.Ctlcontrols.play();
            timer1.Start();
        }

        private void StopGame()
        {
            WMP.Ctlcontrols.stop();
            timer1.Stop();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            NextSong();
            timer1.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            PauseGame();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            ContinueGame();
        }

        private void fGame_Load(object sender, EventArgs e)
        {
            lblSongsAmount.Text = Victorina.music.Count.ToString();
            progbar.Minimum = 0;
            progbar.Value = 0;
            progbar.Maximum = Victorina.gameDuration;
        }
        private void fGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopGame();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ++progbar.Value;
            --musicDuration;
            lblMusDur.Text = Convert.ToString(musicDuration);
            if (musicDuration == 0) NextSong();
            if (progbar.Value == progbar.Maximum)
            {
                StopGame();
                return;
            }
        }

        private void fGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.D)
            {
                PauseGame();
                if (MessageBox.Show("Правильный ответ?", "Игрок 1", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lblPlayer1.Text = Convert.ToString(Convert.ToInt32(lblPlayer1.Text) + 1);
                    NextSong();
                }
                ContinueGame();
            }
            if (e.KeyData == Keys.K)
            {
                PauseGame();
                if (MessageBox.Show("Правильный ответ?", "Игрок 2", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lblPlayer2.Text = Convert.ToString(Convert.ToInt32(lblPlayer2.Text) + 1);
                    NextSong();
                }
                ContinueGame();
            }
        }

        private void WMP_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (Victorina.randomStart)
                if (WMP.openState == WMPLib.WMPOpenState.wmposMediaOpen)
                {
                    WMP.Ctlcontrols.currentPosition = rnd.Next(0, (int)WMP.Ctlcontrols.currentItem.duration / 2);
                }
        }
    }
}
