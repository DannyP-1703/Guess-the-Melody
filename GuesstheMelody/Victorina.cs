using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace GuesstheMelody
{
    static class Victorina
    {
        static public List<string> music = new List<string>();
        static public int gameDuration = 60;
        static public int musicDuration = 10;
        static public bool randomStart = false;
        static public string lastFolder = "";
        static public bool subfolders = false;

        static public void ReadMusic()
        {
            try
            {
                string[] music_files = Directory.GetFiles(lastFolder, "*.mp3",
                      subfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                music.Clear();
                music.AddRange(music_files);
            }
            catch
            {
            }
        }

        static readonly string regKeyName = "Software\\Danny_boy\\GuessTheMelody";

        static public void WriteSettings()
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regKeyName);
                if (rk == null) return;
                rk.SetValue("Last Folder", lastFolder);
                rk.SetValue("Random Start", randomStart);
                rk.SetValue("Game Duration", gameDuration);
                rk.SetValue("Music Duration", musicDuration);
                rk.SetValue("All Directories", subfolders);
            }
            finally
            {
                if (rk != null) rk.Close();
            }
        }

        public static void ReadSettings()
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(regKeyName);
                if (rk != null)
                {
                    gameDuration = Convert.ToInt32(rk.GetValue("Game Duration"));
                    musicDuration = Convert.ToInt32(rk.GetValue("Music Duration"));
                    randomStart = Convert.ToBoolean(rk.GetValue("Random Start"));
                    lastFolder = Convert.ToString(rk.GetValue("Last Folder"));
                    subfolders = Convert.ToBoolean(rk.GetValue("All Directories"));
                }
                
            }
            finally
            {
                if (rk != null) rk.Close();
            }
        }
    }
}
