using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL
{
    public class PlatformInformationProvider : IPlatformInformationProvider
    {
        public int MaxTextureSize { get { return maxTextureSize; } }
        public int MaxArrayTextureLayers { get { return maxArrayTextureLayers; } }

        private int maxTextureSize;
        private int maxArrayTextureLayers;

        public PlatformInformationProvider()
        {
            GL.GetInteger(GetPName.MaxTextureSize, out maxTextureSize);
            GL.GetInteger(GetPName.MaxArrayTextureLayers, out maxArrayTextureLayers);
        }
    }
}
