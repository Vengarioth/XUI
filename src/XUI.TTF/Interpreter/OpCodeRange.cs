using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF.Interpreter
{
    class OpCodeRange : Attribute
    {
        public int From { get; private set; }
        public int To { get; private set; }

        public OpCodeRange(int from, int to)
        {
            From = from;
            To = to;
        }
    }
}
