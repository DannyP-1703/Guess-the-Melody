using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GuesstheMelody
{
    public partial class fSettings : Form
    {
        public fSettings()
        {
            InitializeComponent();
        }
        private List<string> music_list = new List<string>();
        private string path = "";
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
                music_list.AddRange(Directory.GetFiles(path, "*.mp3",
                    cbSubfolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
                lbSongs.Items.Clear();
                lbSongs.Items.AddRange(music_list.ToArray());
            }
        }

        private void button3_Click(object sender, EventArgs e)    //btnOK
        {
            Victorina.gameDuration = Convert.ToInt32(combGameDur.Text);
            Victorina.musicDuration = Convert.ToInt32(combMusicDur.Text);
            Victorina.subfolders = cbSubfolders.Checked;
            Victorina.randomStart = cbRandomStart.Checked;
            Victorina.music.Clear();
            Victorina.music.AddRange(music_list);
            Victorina.lastFolder = path;
            Victorina.WriteSettings();
            this.Hide();
        }

        private void SetSettings()
        {
            cbSubfolders.Checked = Victorina.subfolders;
            combGameDur.Text = Convert.ToString(Victorina.gameDuration);
            combMusicDur.Text = Convert.ToString(Victorina.musicDuration);
            cbRandomStart.Checked = Victorina.randomStart;
            path = Victorina.lastFolder;
            music_list.Clear();
            music_list.AddRange(Victorina.music);
        }
        private void button4_Click(object sender, EventArgs e)  //btnCancel
        {
            SetSettings();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbSongs.Items.Clear();
            music_list.Clear();
            path = "";
        }

        private void fSettings_Load(object sender, EventArgs e)
        {
            SetSettings();
            lbSongs.Items.Clear();
            lbSongs.Items.AddRange(Victorina.music.ToArray());
        }
    }
}
