using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL
{
    class GLVertexSignature
    {
        public GLAttribute[] Attributes { get; private set; }

        public GLVertexSignature(GLAttribute[] attributes)
        {
            Attributes = attributes.OrderBy(e => e.Location).ToArray();
        }
    }
}
