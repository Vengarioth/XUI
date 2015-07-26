using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF
{
    [Flags]
    public enum GlyphFlag
    {
        OnCurve = 1,
        XByte = 2,
        YByte = 4,
        Repeat = 8,
        XSignOrSame = 16,
        YSignOrSame = 32
    }
}
