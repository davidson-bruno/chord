using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transposer.Util;

namespace Transposer.Enum
{
    public enum Chromatism
    {
        [Description("", "diatônico")]
        None,
        [Description("X", "dobrado sustenido")]
        FoldedSharp,
        [Description("#", "sustenido")]
        Sharp,
        [Description("b", "bemol")]
        Flat,
        [Description("bb", "dobrado bemol")]
        FoldedFlat
    }
}
