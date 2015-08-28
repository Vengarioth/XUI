using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering.OpenGL.Resources;

namespace XUI.Rendering.OpenGL
{
    class SpriteBatchBufferGenerator
    {
        public int SourceSurfaceWidth { get; set; }
        public int SourceSurfaceHeight { get; set; }
        public int TargetSurfaceWidth { get; set; }
        public int TargetSurfaceHeight { get; set; }
        public uint IndexCount { get; private set; }

        private BufferGenerator vertexBufferGenerator;
        private BufferGenerator indexBufferGenerator;

        public SpriteBatchBufferGenerator(int sourceSurfaceWidth, int sourceSurfaceHeight, int targetSurfaceWidth, int targetSurfaceHeight)
        {
            vertexBufferGenerator = new BufferGenerator();
            indexBufferGenerator = new BufferGenerator();

            SourceSurfaceWidth = sourceSurfaceWidth;
            SourceSurfaceHeight = sourceSurfaceHeight;
            TargetSurfaceWidth = targetSurfaceWidth;
            TargetSurfaceHeight = targetSurfaceHeight;
        }

        public void AddRectangle(int sourceX, int sourceY, int sourceWidth, int sourceHeight, int targetX, int targetY, int targetWidth, int targetHeight)
        {
            //position
            float vx1 = sourceX / SourceSurfaceWidth;
            float vy1 = 1f - (sourceY / SourceSurfaceHeight);
            float vx2 = (sourceX + sourceWidth) / SourceSurfaceWidth;
            float vy2 = vy1 - (sourceHeight / SourceSurfaceHeight);

            vx1 = (vx1 * 2f) - 1f;
            vy1 = (vy1 * 2f) - 1f;
            vx2 = (vx2 * 2f) - 1f;
            vy2 = (vy2 * 2f) - 1f;

            //uv
            float uvx1 = targetX / TargetSurfaceWidth;
            float uvy1 = 1f - (targetY / TargetSurfaceHeight);
            float uvx2 = (targetX + targetWidth) / TargetSurfaceWidth;
            float uvy2 = uvy1 - (targetHeight / TargetSurfaceHeight);

            uvx1 = (uvx1 * 2f) - 1f;
            uvy1 = (uvy1 * 2f) - 1f;
            uvx2 = (uvx2 * 2f) - 1f;
            uvy2 = (uvy2 * 2f) - 1f;

            vertexBufferGenerator.WriteFloat(uvx1, uvy2, vx1, vy2);
            vertexBufferGenerator.WriteFloat(uvx2, uvy2, vx2, vy2);
            vertexBufferGenerator.WriteFloat(uvx2, uvy1, vx2, vy1);
            vertexBufferGenerator.WriteFloat(uvx1, uvy1, vx1, vy1);

            indexBufferGenerator.WriteUInt(IndexCount, IndexCount + 1, IndexCount + 2);
            indexBufferGenerator.WriteUInt(IndexCount, IndexCount + 2, IndexCount + 3);

            IndexCount += 4;
        }

        public SpriteBatchBuffer GetBuffer()
        {
            var vertexBufferData = vertexBufferGenerator.GetBuffer();
            var indexBufferData = indexBufferGenerator.GetBuffer();

            var vertexBuffer = BufferFactory.Allocate(BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw, vertexBufferData);
            var indexBuffer = BufferFactory.Allocate(BufferTarget.ElementArrayBuffer, BufferUsageHint.StaticDraw, indexBufferData);

            return new SpriteBatchBuffer(vertexBuffer, indexBuffer, IndexCount);
        }

        public void Clear()
        {
            vertexBufferGenerator.Reset();
            indexBufferGenerator.Reset();
            IndexCount = 0;
        }
    }
}
