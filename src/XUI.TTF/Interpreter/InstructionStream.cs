using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF.Interpreter
{
    class InstructionStream
    {
        private MemoryStream internalStream;

        public long Position { get { return internalStream.Position; } }
        public long Length { get { return internalStream.Length; } }

        public InstructionStream(byte[] instructions)
        {
            internalStream = new MemoryStream(instructions);
        }

        public byte ReadByte()
        {
            var value = internalStream.ReadByte();
            if (value < 0)
                throw new Exception("Trying to read after end of Instruction Stream");
            return (byte)value;
        }
    }
}
