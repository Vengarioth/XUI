using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL
{
    class GLUniform
    {
        public ActiveUniformType Type { get; private set; }
        public int Count { get; private set; }
        public int Index { get; private set; }
        public int Location { get; private set; }
        public string Name { get; private set; }

        public int ComponentCount { get; private set; }
        public int ComponentSize { get; private set; }

        public GLUniform(ActiveUniformType type, int index, int location, int count, string name, int componentCount, int componentSize)
        {
            Type = type;
            Index = index;
            Location = location;
            Count = count;
            Name = name;
            ComponentCount = componentCount;
            ComponentSize = componentSize;
        }

        public void SetIntComponentType(int value)
        {
            switch (Type)
            {
                case ActiveUniformType.Int:
                case ActiveUniformType.Sampler1D:
                case ActiveUniformType.Sampler2D:
                    GL.Uniform1(Location, value);
                    break;
            }
        }

        public void SetIntComponentType(int[] values)
        {
            switch (Type)
            {
                case ActiveUniformType.Int:
                case ActiveUniformType.Sampler1D:
                case ActiveUniformType.Sampler2D:
                    GL.Uniform1(Location, values[0]);
                    break;
            }
        }

        public void SetFloatComponentType(float value)
        {
            switch (Type)
            {
                case ActiveUniformType.Float:
                    GL.Uniform1(Location, value);
                    break;
            }
        }

        public void SetFloatComponentType(float[] values, bool transpose = false)
        {
            switch (Type)
            {
                case ActiveUniformType.Float:
                    GL.Uniform1(Location, values[0]);
                    break;
                case ActiveUniformType.FloatVec2:
                    GL.Uniform2(Location, 1, values);
                    break;
                case ActiveUniformType.FloatVec3:
                    GL.Uniform3(Location, 1, values);
                    break;
                case ActiveUniformType.FloatVec4:
                    GL.Uniform4(Location, 1, values);
                    break;
                case ActiveUniformType.FloatMat2:
                    GL.UniformMatrix2(Location, 4, transpose, values);
                    break;
                case ActiveUniformType.FloatMat2x3:
                    GL.UniformMatrix2x3(Location, 6, transpose, values);
                    break;
                case ActiveUniformType.FloatMat2x4:
                    GL.UniformMatrix2x4(Location, 8, transpose, values);
                    break;
                case ActiveUniformType.FloatMat3:
                    GL.UniformMatrix3(Location, 9, transpose, values);
                    break;
                case ActiveUniformType.FloatMat3x2:
                    GL.UniformMatrix3x2(Location, 6, transpose, values);
                    break;
                case ActiveUniformType.FloatMat3x4:
                    GL.UniformMatrix3x4(Location, 12, transpose, values);
                    break;
                case ActiveUniformType.FloatMat4:
                    GL.UniformMatrix4(Location, 16, transpose, values);
                    break;
                case ActiveUniformType.FloatMat4x2:
                    GL.UniformMatrix4x2(Location, 8, transpose, values);
                    break;
                case ActiveUniformType.FloatMat4x3:
                    GL.UniformMatrix4x3(Location, 12, transpose, values);
                    break;
            }
        }
    }
}
