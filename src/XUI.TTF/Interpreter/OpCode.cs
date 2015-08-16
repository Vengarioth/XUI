using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF.Interpreter
{
    class OpCode : Attribute
    {
        public int Code { get; private set; }

        public OpCode(int code)
        {
            Code = code;
        }
    }
}
