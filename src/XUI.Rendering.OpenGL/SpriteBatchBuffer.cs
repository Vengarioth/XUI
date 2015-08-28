using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering.OpenGL.Resources;

namespace XUI.Rendering.OpenGL
{
    class SpriteBatchBuffer : IDisposable
    {
        public GLBuffer VertexBuffer { get; private set; }
        public GLBuffer IndexBuffer { get; private set; }
        public uint IndexCount { get; private set; }
        
        public SpriteBatchBuffer(GLBuffer vertexBuffer, GLBuffer indexBuffer, uint indexCount)
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
