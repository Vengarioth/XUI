using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL
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

        public void Dispose()
        {
            Disposed = true;
            GL.DeleteTexture(Handle);
        }
    }
}
