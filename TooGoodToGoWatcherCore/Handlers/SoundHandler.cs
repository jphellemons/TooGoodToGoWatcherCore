using System;
using System.Media;
using TooGoodToGoWatcherCore.Models;

namespace TooGoodToGoWatcherCore.Handlers
{
    public class SoundHandler : ISoundHandler
    {
        private Settings settings;
        private SoundPlayer player;

        public SoundHandler(Settings settings)
        {
            this.settings = settings;

            if (System.Runtime.InteropServices.RuntimeInformation.OSDescription.Contains("Windows", StringComparison.OrdinalIgnoreCase))
            {
                player = new SoundPlayer(settings.SoundFile);
            }
        }

        public void PlaySound()
        {
            if (settings.ConsoleBeepOnNotification)
            {
                Console.Beep();
            }

            if (settings.SoundOnNotification)
            {
                if (System.Runtime.InteropServices.RuntimeInformation.OSDescription.Contains("Windows", StringComparison.OrdinalIgnoreCase))
                {
                    player.Play();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Only sound on Windows at the moment, or enable the PC speaker");
                }
            }
        }
    }
}
