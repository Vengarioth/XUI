using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF
{
    public class TrueTypeFont
    {
        public Header Header { get; private set; }
        public Maxp Maxp { get; private set; }
        public Loca Loca { get; private set; }
        public Glyf Glyf { get; private set; }

        public TrueTypeFont(Header header, Maxp maxp, Loca loca, Glyf glyf)
        {
            Header = header;
            Maxp = maxp;
            Loca = loca;
            Glyf = glyf;
        }
    }
}
