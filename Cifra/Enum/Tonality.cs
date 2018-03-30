using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transposer.Util;

namespace Transposer.Enum
{
    public enum Tonality
    {
        [Description("", "maior")]
        Major = 4,
        [Description("m", "menor")]
        Minor = 3,
        [Description("sus", "suspenso (adição do 4º grau)")]
        Sus = 5,
        [Description("sus2", "suspenso (adição do 2º grau)")]
        Sus2 = 2,
        [Description("sus4", "suspenso (adição do 4º grau)")]
        Sus4 = 5,
        [Description("sus9", "suspenso (adição do 9º (ou 2º) grau)")]
        Sus9 = 2
    }
}
