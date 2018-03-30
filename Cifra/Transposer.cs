using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Transposer;
using Transposer.Enum;
using Transposer.Exceptions;
using Transposer.Util;

namespace Transposer
{
    public class Transposer
    {
        private const int maxSemitones = 12;
        private const int maxTones = 7;
        private const int maxLines = 15;
        private const int maxColumns = 7;

        public static Note[,] map = new Note[maxLines, maxColumns];

        static Transposer()
        {
            map = new Note[maxLines, maxColumns];

            map[0, 6] = new Note(Key.F, Chromatism.FoldedSharp);
            map[1, 5] = new Note(Key.E, Chromatism.FoldedSharp);
            map[1, 6] = new Note(Key.F, Chromatism.Sharp);
            map[2, 5] = new Note(Key.E, Chromatism.Sharp);
            map[2, 6] = new Note(Key.F);
            map[3, 4] = new Note(Key.D, Chromatism.FoldedSharp);
            map[3, 5] = new Note(Key.E);
            map[3, 6] = new Note(Key.F, Chromatism.Flat);
            map[4, 4] = new Note(Key.D, Chromatism.Sharp);
            map[4, 5] = new Note(Key.E, Chromatism.Flat);
            map[4, 6] = new Note(Key.F, Chromatism.FoldedFlat);
            map[5, 3] = new Note(Key.C, Chromatism.FoldedSharp);
            map[5, 4] = new Note(Key.D);
            map[5, 5] = new Note(Key.E, Chromatism.FoldedFlat);
            map[6, 2] = new Note(Key.B, Chromatism.FoldedSharp);
            map[6, 3] = new Note(Key.C, Chromatism.Sharp);
            map[6, 4] = new Note(Key.D, Chromatism.Flat);
            map[7, 2] = new Note(Key.B, Chromatism.Sharp);
            map[7, 3] = new Note(Key.C);
            map[7, 4] = new Note(Key.D, Chromatism.FoldedFlat);
            map[8, 1] = new Note(Key.A, Chromatism.FoldedSharp);
            map[8, 2] = new Note(Key.B);
            map[8, 3] = new Note(Key.C, Chromatism.Flat);
            map[9, 1] = new Note(Key.A, Chromatism.Sharp);
            map[9, 2] = new Note(Key.B, Chromatism.Flat);
            map[9, 3] = new Note(Key.C, Chromatism.FoldedFlat);;
            map[10, 0] = new Note(Key.G, Chromatism.FoldedSharp);
            map[10, 1] = new Note(Key.A);
            map[10, 2] = new Note(Key.B, Chromatism.FoldedFlat);
            map[11, 0] = new Note(Key.G, Chromatism.Sharp);
            map[11, 1] = new Note(Key.A, Chromatism.Flat);
            map[12, 0] = new Note(Key.G);
            map[12, 1] = new Note(Key.A, Chromatism.FoldedFlat);
            map[13, 0] = new Note(Key.G, Chromatism.Flat);
            map[14, 0] = new Note(Key.G, Chromatism.FoldedFlat);
        }

        private static Position PositionAdjustment(Position position)
        {
            if (position.I < 0)
            {
                position.I += maxSemitones;
            }
            else if (position.I >= maxLines)
            {
                position.I -= maxSemitones;
            }

            if (position.J < 0)
            {
                position.J += maxTones;
            }
            else if (position.J >= maxColumns)
            {
                position.J -= maxTones;
            }

            return position;
        }

        public static Note Transpose(Note note, int semitones, Direction direction = Direction.Above)
        {
            while (semitones >= maxSemitones)
            {
                semitones -= maxSemitones;
            }

            Position start = GetPosition(note);
            Position target = new Position(start.I + semitones * -(int)direction, start.J + (semitones / 2) * (int)direction);

            target = PositionAdjustment(target);

            if (direction == Direction.Above)
            {
                if (3 > start.J && 3 <= target.J)
                {
                    target.J++;
                }
                
                if (6 > start.J && 6 <= target.J)
                {
                    target.J++;
                }

                target = PositionAdjustment(target);

                if (map[target.I, target.J] == null)
                {
                    target.J++;
                }
            }
            else if (direction == Direction.Bellow)
            {
                if (2 > start.J && 2 <= target.J)
                {
                    target.J--;
                }

                if (5 > start.J && 5 <= target.J)
                {
                    target.J--;
                }

                target = PositionAdjustment(target);

                if (map[target.I, target.J] == null)
                {
                    target.J--;
                }
            }

            return map[target.I, target.J];
        }

        public static Position GetPosition(Note note)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] != null)
                    {
                        if (note.Equals(map[i,j]))
                        {
                            return new Position(i, j);
                        }
                    }
                }
            }

            throw new NullReferenceException("Note is not on the map.");
        }

        public static Chord CreateChord(string text)
        {
            Regex regex = new Regex(@"(([A-G])(bb|[Xb#])?)(º|((m?)((([245679]|11|13)[+-]?)(\((([245679]|11|13)[+-]?)\))?)?|(sus[249]?))?(\/(([A-G])(bb|[Xb#])?))?)");

            if (!regex.IsMatch(text))
            {
                throw new NotAKeyException("O texto informado não corresponde a nenhum acorde.");
            }

            Match match = regex.Match(text);

            Note tonic = GetNote(match.Groups[2].Value, match.Groups[3].Value);
            Tonality tonality = GetTonality(match.Groups[5].Value);
            Addition addition1 = GetAddition(match.Groups[8].Value);
            Addition addition2 = GetAddition(match.Groups[11].Value);
            Note bass = GetNote(match.Groups[16].Value, match.Groups[17].Value);

            return new Chord(tonic, tonality, addition1, addition2, bass);
        }

        private static Note GetNote(string keySymbol, string chromatismSymbol)
        {
            Console.WriteLine(keySymbol);

            Key key = Key.Undefined;
            Chromatism chromatism = Chromatism.None;

            foreach (Key k in System.Enum.GetValues(typeof(Key)))
            {
                if (k != Key.Undefined)
                {
                    if (keySymbol.Equals(k.Symbol()))
                    {
                        key = k;
                        break;
                    }
                }
            }

            foreach (Chromatism c in System.Enum.GetValues(typeof(Chromatism)))
            {
                if (c != Chromatism.None)
                {
                    if (chromatismSymbol.Equals(c.Symbol()))
                    {
                        chromatism = c;
                        break;
                    }
                }
            }

            return new Note(key, chromatism);
        }

        private static Tonality GetTonality(string tonalitySymbol)
        {
            Tonality tonality = Tonality.Major;

            foreach (Tonality t in System.Enum.GetValues(typeof(Tonality)))
            {
                if (tonalitySymbol.Contains(t.Symbol()))
                {
                    tonality = t;
                }
            }

            return tonality;
        }

        private static Addition GetAddition(string additionText)
        {
            Addition addition = Addition.None;

            foreach (Addition a in System.Enum.GetValues(typeof(Addition)))
            {
                if (additionText.Equals(a.Symbol()))
                {
                    addition = a;
                }
            }

            return addition;
        }
    }
}
