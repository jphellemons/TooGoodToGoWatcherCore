using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
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

            player = new SoundPlayer(settings.SoundFile);
        }

        public void PlaySound()
        {
            if (settings.ConsoleBeepOnNotification)
            {
                Console.Beep();
            }

            if (settings.SoundOnNotification)
            {
                player.Play();
            }
        }
    }
}
