using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF
{
    public class Glyf
    {
        public Glyph[] Glyphs { get; private set; }

        public Glyf(Glyph[] glyphs)
        {
            Glyphs = glyphs;
        }
    }
}
