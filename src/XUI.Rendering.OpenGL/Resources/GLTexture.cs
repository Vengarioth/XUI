using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL.Resources
{
    public class GLTexture : IGraphicsResource
    {
        public int Handle { get; private set; }
        public bool Disposed { get; private set; }
        public TextureTarget TextureTarget { get; private set; }

        public GLTexture(int handle, TextureTarget textureTarget)
        {
            Handle = handle;
            TextureTarget = textureTarget;
        }

        public void SubImage2D(int x, int y, int width, int height, IntPtr data)
        {
            GL.BindTexture(TextureTarget, Handle);
            GL.TexSubImage2D(TextureTarget, 0, x, y, width, height, PixelFormat.Rgba, PixelType.UnsignedByte, data);
        }

        public void Dispose()
        {
            Disposed = true;
            GL.DeleteTexture(Handle);
        }
    }
}
