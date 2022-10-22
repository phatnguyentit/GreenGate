using System.IO;
using System.Media;

namespace GreenGate
{
    internal class SoundTrigger
    {
        public void Warning()
        {
            SoundPlayer simpleSound = new SoundPlayer(Path.Combine(Directory.GetCurrentDirectory(), "Media", "OneDose.wav"));
            simpleSound.Play();
        }

        public void Welcome()
        {
            SoundPlayer simpleSound = new SoundPlayer(Path.Combine(Directory.GetCurrentDirectory(), "Media", "TwoDose.wav"));
            simpleSound.Play();
        }
    }
}
