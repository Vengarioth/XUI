using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF.Interpreter
{
    static class Instructions
    {
        [OpCode(0x40)]
        public static void NPUSHB(int flags, InstructionStream stream, InstructionStack stack)
        {
            var bytesToPush = (int)stream.ReadByte();
            
            for(var i = 0; i < bytesToPush; i++)
            {
                stack.PushAsInt32(stream.ReadByte());
            }
        }

        [OpCodeRange(0x2E, 0x2F)]
        public static void MDAP(int flags, InstructionStream stream, InstructionStack stack)
        {
            var pointNumber = stack.PopInt32();
        }

        [OpCodeRange(0xC0, 0xDF)]
        public static void MDRP(int flags, InstructionStream stream, InstructionStack stack)
        {
            var pointNumber = stack.PopInt32();
        }

        [OpCode(0x12)]
        public static void SRP2(int flags, InstructionStream stream, InstructionStack stack)
        {
            var pointNumber = stack.PopInt32();
        }

        [OpCode(0x39)]
        public static void IP(int flags, InstructionStream stream, InstructionStack stack)
        {

        }
        
        [OpCodeRange(0x00, 0x01)]
        public static void SVTCA(int flags, InstructionStream stream, InstructionStack stack)
        {

        }

        [OpCodeRange(0x3E, 0x3F)]
        public static void MIAP(int flags, InstructionStream stream, InstructionStack stack)
        {
            var cvtEntryNumber = stack.PopInt32();
            var pointNumber = stack.PopInt32();
        }

        [OpCodeRange(0x30, 0x31)]
        public static void IUP(int flags, InstructionStream stream, InstructionStack stack)
        {

        }

    }
}
