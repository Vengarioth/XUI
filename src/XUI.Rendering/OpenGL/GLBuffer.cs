using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL
{
    class GLBuffer : IGraphicsResource
    {
        public bool Disposed { get; private set; }
        public int Handle { get; private set; }
        public BufferTarget Target { get; private set; }
        public BufferUsageHint UsageHint { get; private set; }
        public int Size { get; private set; }
        
        internal GLBuffer(int bufferHandle, BufferTarget bufferTarget, BufferUsageHint bufferUsageHint, int size)
        {
            Handle = bufferHandle;
            Target = bufferTarget;
            UsageHint = bufferUsageHint;
            Size = size;
        }

        public void Bind()
        {
            GL.BindBuffer(Target, Handle);
        }

        public void SetData(ref byte[] data)
        {
            if (Disposed)
                throw new ObjectDisposedException(ToString());

            GL.BindBuffer(Target, Handle);
            GL.BufferData(Target, (IntPtr)data.Length, data, UsageHint);
            GL.BindBuffer(Target, 0);
        }

        public void SetData(ref byte[] data, BufferUsageHint usageHint)
        {
            if (Disposed)
                throw new ObjectDisposedException(ToString());

            UsageHint = usageHint;
            GL.BindBuffer(Target, Handle);
            GL.BufferData(Target, (IntPtr)data.Length, data, UsageHint);
            GL.BindBuffer(Target, 0);
        }

        public void SetDataSubset(ref byte[] data, int offset)
        {
            if (Disposed)
                throw new ObjectDisposedException(ToString());

            GL.BindBuffer(Target, Handle);
            GL.BufferSubData(Target, (IntPtr)offset, (IntPtr)data.Length, data);
            GL.BindBuffer(Target, 0);
        }

        public void Dispose()
        {
            Disposed = true;
            GL.DeleteBuffer(Handle);
        }

        public override string ToString()
        {
            return "GLBuffer " + Handle;
        }
    }
}
