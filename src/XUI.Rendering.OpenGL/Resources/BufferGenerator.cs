using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL.Resources
{
    public class BufferGenerator
    {
        private byte[] buffer;
        private int pointer;

        public BufferGenerator()
        {
            buffer = new byte[1024];
        }

        public void Reset()
        {
            buffer = new byte[1024];
            pointer = 0;
        }
        
        public byte[] GetBuffer()
        {
            return buffer.Take(pointer).ToArray();
        }

        public void WriteBool(params bool[] values)
        {
            CopyBytesToBuffer(values, sizeof(bool));
        }

        public void WriteDouble(params double[] values)
        {
            CopyBytesToBuffer(values, sizeof(double));
        }

        public void WriteFloat(params float[] values)
        {
            CopyBytesToBuffer(values, sizeof(float));
        }

        public void WriteInt(params int[] values)
        {
            CopyBytesToBuffer(values, sizeof(int));
        }

        public void WriteUInt(params uint[] values)
        {
            CopyBytesToBuffer(values, sizeof(uint));
        }

        private void CopyBytesToBuffer(Array src, int srcStride)
        {
            var length = src.Length * srcStride;

            if (pointer + length > buffer.Length)
                Array.Resize(ref buffer, buffer.Length * 2);

            Buffer.BlockCopy(src, 0, buffer, pointer, length);
            pointer += length;
        }
    }
}
