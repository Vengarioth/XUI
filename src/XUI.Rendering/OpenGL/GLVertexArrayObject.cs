using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL
{
    class GLVertexArrayObject : IGraphicsResource
    {
        public int Handle { get; private set; }
        public GLProgram Program { get; private set; }
        public GLBuffer VertexBuffer { get; private set; }
        public GLBuffer IndexBuffer { get; private set; }

        public GLVertexArrayObject(int handle, GLProgram program, GLBuffer vertexBuffer, GLBuffer indexBuffer)
        {
            Handle = handle;
            Program = program;
            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;
        }

        public void Draw()
        {

        }

        public void Dispose()
        {
            GL.DeleteVertexArray(Handle);
        }
    }
}
