using libmusic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Generators
{
    class AdvancedRandomization : IMusicGenerator
    {
        float takt = 0;
        public float Deadline()
        {
            return takt;
        }

        public ICollection<LibNoteMessage> GenerateMusic()
        {
            List<LibNoteMessage> music = new List<LibNoteMessage>();

            //music.Add(new NoteOnOff(Midi.Pitch.A4, ))


            return music;
        }

        public void Setup(InfoObject infoObject)
        {
            //throw new NotImplementedException();
        }
    }
}
