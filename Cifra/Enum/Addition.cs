using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transposer.Util;

namespace Transposer.Enum
{
    public enum Addition
    {
        [Description("", "")]
        None = 0,
        [Description("5-", "quinta diminuta")]
        Flat5 = 6,
        [Description("6-", "sexta menor")]
        Flat6 = 8,
        [Description("9-", "nona menor")]
        Flat9 = 1,
        [Description("11-", "décima primeira diminuta")]
        Flat11 = 4,
        [Description("13-", "décima terceira menor")]
        Flat13 = 8,
        [Description("2", "segunda")]
        Deg2 = 2,
        [Description("4", "quarta")]
        Deg4 = 5,
        [Description("5", "quinta")]
        Deg5 = 7,
        [Description("6", "sexta")]
        Deg6 = 9,
        [Description("7", "sétima")]
        Deg7 = 10,
        [Description("9", "nona")]
        Deg9 = 2,
        [Description("11", "décima primeira")]
        Deg11 = 5,
        [Description("13", "décima terceira")]
        Deg13 = 9,
        [Description("5+", "quinta aumentada")]
        Sharp5 = 8,
        [Description("7+", "sétima maior")]
        Sharp7 = 11,
    }
}
