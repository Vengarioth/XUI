using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering.TexturePacking;

namespace XUI.Rendering
{
    class BatchManager
    {
        private int maxTextureSize;
        private int maxArrayTextureLayers;

        public BatchManager()
        {
            GL.GetInteger(GetPName.MaxTextureSize, out maxTextureSize);
            GL.GetInteger(GetPName.MaxArrayTextureLayers, out maxArrayTextureLayers);
        }


    }
}
