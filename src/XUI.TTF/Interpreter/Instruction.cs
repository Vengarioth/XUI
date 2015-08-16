using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF.Interpreter
{
    class Instruction
    {
        public Action<int, InstructionStream, InstructionStack> Action { get; private set; }
        public int OpCode { get; private set; }

        public Instruction(int opCode, Action<int, InstructionStream, InstructionStack> action)
        {
            OpCode = opCode;
            Action = action;
        }

        public void Execute(int callingOpCode, InstructionStream stream, InstructionStack stack)
        {
            int flags = callingOpCode - OpCode;
            Action(flags, stream, stack);
        }
    }
}
