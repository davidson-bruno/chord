using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transposer.Enum;
using Transposer.Util;

namespace Transposer.Util
{
    public class Note
    {
        public Key Key { get; set; }
        public Chromatism Chromatism { get; set; }

        public Note(Key key, Chromatism chromatism = Chromatism.None)
        {
            Key = key;
            Chromatism = chromatism;
        }

        public override string ToString()
        {
            return Key.Symbol() + Chromatism.Symbol();
        }

        public static Note operator ++(Note note)
        {
            return Transposer.Transpose(note, 1, Direction.Above);
        }

        public static Note operator --(Note note)
        {
            return Transposer.Transpose(note, 1, Direction.Bellow);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Note))
            {
                Note note = (Note)obj;

                if (this.Key == note.Key && this.Chromatism == note.Chromatism)
                {
                    return true;
                }
            }

            return false;
        }

        public Note Switch
        {
            get
            {
                Position position = null; ;

                try
                {
                    position = Transposer.GetPosition(this);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }

                if (this.Chromatism == Chromatism.FoldedFlat || this.Chromatism == Chromatism.Flat)
                {
                    position.J--;
                }
                else if (this.Chromatism == Chromatism.FoldedSharp || this.Chromatism == Chromatism.Sharp)
                {
                    position.J++;
                }

                return Transposer.map[position.I, position.J];
            }
        }
    }
}
