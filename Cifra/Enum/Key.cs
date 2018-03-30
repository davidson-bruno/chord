using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transposer.Util;

namespace Transposer.Enum
{
    public enum Key
    {
        [Description("", "Não definido")]
        Undefined,
        [Description("A", "Lá")]
        A,
        [Description("B", "Si")]
        B,
        [Description("C", "Dó")]
        C,
        [Description("D", "Ré")]
        D,
        [Description("E", "Mi")]
        E,
        [Description("F", "Fá")]
        F,
        [Description("G", "Sol")]
        G
    }
}
