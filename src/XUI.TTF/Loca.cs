using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF
{
    public class Loca
    {
        public uint[] Offsets { get; private set; }

        public Loca(uint[] offsets)
        {
            Offsets = offsets;
        }
    }
}
