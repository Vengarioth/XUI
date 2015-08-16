using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.TTF.Interpreter
{
    class InstructionStack
    {
        private Stack<byte> internalStack;
        
        public InstructionStack()
        {
            internalStack = new Stack<byte>();
        }

        public void Push(byte item)
        {
            internalStack.Push(item);
        }

        public void Push(params byte[] items)
        {
            for(int i = 0; i < items.Length; i++)
                internalStack.Push(items[i]);
        }
        
        public byte Peek()
        {
            return internalStack.Peek();
        }

        public byte Pop()
        {
            return internalStack.Pop();
        }

        #region UInt32
        /// <summary>
        /// Pops 4 bytes from the Stack and returns them as UInt32
        /// </summary>
        /// <returns></returns>
        public UInt32 PopUInt32()
        {
            var data = new byte[]
            {
                internalStack.Pop(),
                internalStack.Pop(),
                internalStack.Pop(),
                internalStack.Pop()
            };
            Array.Reverse(data);

            EnsureSystemEndianessFromBigEndianess(ref data);

            return BitConverter.ToUInt32(data, 0);
        }

        /// <summary>
        /// Extends a byte to UInt32 (Big Endian) and pushes it to the Stack
        /// </summary>
        /// <param name="item"></param>
        public void PushAsUInt32(byte item)
        {
            var intVal = (UInt32)item;
            var bytes = BitConverter.GetBytes(intVal);
            EnsureBigEndianness(ref bytes);

            Push(bytes);
        }
        #endregion

        #region Int32
        /// <summary>
        /// Pops 4 bytes from the Stack and returns them as Int32
        /// </summary>
        /// <returns></returns>
        public Int32 PopInt32()
        {
            var data = new byte[]
            {
                internalStack.Pop(),
                internalStack.Pop(),
                internalStack.Pop(),
                internalStack.Pop()
            };
            Array.Reverse(data);

            EnsureSystemEndianessFromBigEndianess(ref data);

            return BitConverter.ToInt32(data, 0);
        }
        
        /// <summary>
        /// Extends a byte to Int32 (Big Endian) and pushes it to the Stack
        /// </summary>
        /// <param name="item"></param>
        public void PushAsInt32(byte item)
        {
            var intVal = (Int32)item;
            var bytes = BitConverter.GetBytes(intVal);
            EnsureBigEndianness(ref bytes);

            Push(bytes);
        }
        #endregion

        private void EnsureBigEndianness(ref byte[] data)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(data);
        }

        private void EnsureSystemEndianessFromBigEndianess(ref byte[] data)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(data);
        }
    }
}
