using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL.Resources
{
    static class TextureFactory
    {
        public static GLTexture Allocate(int width, int height)
        {
            var handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, handle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            return new GLTexture(handle, TextureTarget.Texture2D);
        }

    }
}
