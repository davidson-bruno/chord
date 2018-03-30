using System;
using Transposer.Exceptions;
using Transposer.Enum;
using Transposer.Util;
using System.Text.RegularExpressions;

namespace Transposer
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex regex = new Regex(@"([A-G](bb|[Xb#])?)(º|((m?)((([245679]|11|13)[+-]?)(\((([245679]|11|13)[+-]?)\))?)?|(sus[249]?))?(\/([A-G](bb|[Xb#])?))?)");

            try
            {
                Chord chord = Transposer.CreateChord("E");

                foreach (Note note in chord.GetChordNotes)
                {
                    Console.WriteLine(note);
                }
            }
            catch(NotAKeyException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
