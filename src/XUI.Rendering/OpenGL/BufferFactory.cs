using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL
{
    static class BufferFactory
    {

        public static GLBuffer Allocate(BufferTarget bufferTarget, BufferUsageHint bufferUsageHint, int size)
        {
            int handle = GL.GenBuffer();
            GL.BindBuffer(bufferTarget, handle);
            GL.BufferData(bufferTarget, (IntPtr)size, IntPtr.Zero, bufferUsageHint);
            GL.BindBuffer(bufferTarget, 0);

            return new GLBuffer(handle, bufferTarget, bufferUsageHint, size);
        }

        public static GLBuffer Allocate(BufferTarget bufferTarget, BufferUsageHint bufferUsageHint, byte[] data)
        {
            int handle = GL.GenBuffer();
            GL.BindBuffer(bufferTarget, handle);
            GL.BufferData(bufferTarget, (IntPtr)data.Length, data, bufferUsageHint);
            GL.BindBuffer(bufferTarget, 0);

            return new GLBuffer(handle, bufferTarget, bufferUsageHint, data.Length);
        }

        public static GLBuffer Allocate(BufferTarget bufferTarget, BufferUsageHint bufferUsageHint, uint[] data)
        {
            int length = data.Length * sizeof(uint);

            int handle = GL.GenBuffer();
            GL.BindBuffer(bufferTarget, handle);
            GL.BufferData(bufferTarget, (IntPtr)length, data, bufferUsageHint);
            GL.BindBuffer(bufferTarget, 0);

            return new GLBuffer(handle, bufferTarget, bufferUsageHint, length);
        }
    }
}
