using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering.OpenGL;
using XUI.Rendering.OpenGL.Resources;

namespace XUI.Rendering.VectorGraphics
{
    class VectorMesh : IDisposable
    {
        public GLBuffer VertexBuffer { get; private set; }
        public GLBuffer IndexBuffer { get; private set; }
        public int IndexCount { get; private set; }

        public VectorMesh(GLBuffer vertexBuffer, GLBuffer indexBuffer, int indexCount)
        {
            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;
            IndexCount = indexCount;
        }

        public void Dispose()
        {
            VertexBuffer.Dispose();
            IndexBuffer.Dispose();
        }
    }
}
