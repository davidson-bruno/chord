using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transposer.Enum;
using Transposer.Util;

namespace Transposer
{
    public class Chord
    {
        public Note Tonic { get; set; }
        public Tonality Tonality { get; set; }
        public Addition Addition1 { get; set; }
        public Addition Addition2 { get; set; }
        public Note Bass { get; set; }

        public Chord(Note tonic, Tonality tonality = Tonality.Major, Addition addition1 = Addition.None, Addition addition2 = Addition.None, Note bass = null)
        {
            Tonic = tonic;
            Tonality = tonality;
            Addition1 = addition1;
            Addition2 = addition2;
            Bass = bass;
        }

        public override string ToString()
        {
            string bass = string.Empty;
            string addition2 = string.Empty;

            if (Bass != null)
            {
                bass = string.Format("/{0}", Bass.ToString());
            }

            if (Addition2 != Addition.None)
            {
                addition2 = string.Format("({0})", Addition2.Symbol());
            }

            return Tonic.ToString() + Tonality.Symbol() + Addition1.Symbol() + addition2 + bass;
        }

        public string Info
        {
            get
            {
                string addition1 = string.Empty;
                string addition2 = string.Empty;

                if (Addition1 != Addition.None)
                {
                    addition1 = string.Format("com {0}", Addition1.Name());
                }

                if (Addition2 != Addition.None)
                {
                    addition2 = string.Format("e {0}", Addition2.Name());
                }

                string name = string.Format("{0} {1} {2} {3} {4}");

                return string.Format(
                    "Cifra: {0}\n" +
                    "Nome: {1}\n" +
                    "Tônica: {2}\n" +
                    "Tonalidade: {3}\n" +
                    "Adição: {4}\n" +
                    "Baixo: {5}",
                    this.ToString(), Tonic.ToString(), Tonality.Name(), Addition1.Name() + addition2, Bass.ToString());
            }
        }

        public Note[] GetChordNotes
        {
            get
            {

                List<Note> notes = new List<Note>();

                notes.Add(Tonic);
                notes.Add(Transposer.Transpose(Tonic, (int)Tonality));
                notes.Add(Transposer.Transpose(Tonic, 7));

                return notes.ToArray();
            }
        }
    }
}
