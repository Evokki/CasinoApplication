using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Resources;

namespace GambleAssetsLibrary
{
    public static class AudioManager
    {
        private static SoundPlayer player;
        public const string ClickSoundPath = @"C:/Users/Veeti/Desktop/HAMK/Moduuli3 - Ohjelmointi/Ohjelmistokehitys/Ohjelmistokehitys/OhjelmistokehitysProjekti/Resources/Audio/click.wav";
        public const string WinSoundPath = @"C:/Users/Veeti/Desktop/HAMK/Moduuli3 - Ohjelmointi/Ohjelmistokehitys/Ohjelmistokehitys/OhjelmistokehitysProjekti/Resources/Audio/win.wav";
        public static void PlayAudio(string AudioPath)
        {
            player = new SoundPlayer(AudioPath);
            player.Load();
            player.Play();

        }
    }
}
