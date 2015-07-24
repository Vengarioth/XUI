using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL
{
    class GLAttribute
    {
        public ActiveAttribType Type { get; private set; }
        public int Location { get; private set; }
        public string Name { get; private set; }

        public GLAttribute(ActiveAttribType type, int location, string name)
        {
            Type = type;
            Location = location;
            Name = name;
        }

        public int SizeInBytes()
        {
            switch (Type)
            {
                case ActiveAttribType.Float:
                    return sizeof(float);
                case ActiveAttribType.FloatVec2:
                    return sizeof(float) * 2;
                case ActiveAttribType.FloatVec3:
                    return sizeof(float) * 3;
                case ActiveAttribType.FloatVec4:
                    return sizeof(float) * 4;
            }

            throw new NotImplementedException();
        }

        public VertexAttribPointerType GetComponentType()
        {
            switch (Type)
            {
                case ActiveAttribType.Float:
                case ActiveAttribType.FloatVec2:
                case ActiveAttribType.FloatVec3:
                case ActiveAttribType.FloatVec4:
                    return VertexAttribPointerType.Float;
            }

            throw new NotImplementedException();
        }

        public int GetComponentCount()
        {
            switch (Type)
            {
                case ActiveAttribType.Float:
                    return 1;
                case ActiveAttribType.FloatVec2:
                    return 2;
                case ActiveAttribType.FloatVec3:
                    return 3;
                case ActiveAttribType.FloatVec4:
                    return 4;
            }

            throw new NotImplementedException();
        }
    }
}
