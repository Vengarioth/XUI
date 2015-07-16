using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL
{
    class GLUniformBlock
    {
        public int Location { get; private set; }
        public string Name { get; private set; }

        public GLUniformBlock(int location, string name)
        {
            Location = location;
            Name = name;
        }
    }
}
