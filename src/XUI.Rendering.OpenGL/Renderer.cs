﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering.OpenGL.Resources;

namespace XUI.Rendering.OpenGL
{
    public class Renderer : IRenderer
    {
        private GLProgram spriteBatchShader;

        private SpriteBatchBufferGenerator spriteBatchBufferGenerator;
        private Dictionary<Batch, SpriteBatchBuffer> spriteBatchBufferList;
        private Dictionary<Texture, GLTexture> textureList;

        public Renderer()
        {
            spriteBatchBufferList = new Dictionary<Batch, SpriteBatchBuffer>();
            textureList = new Dictionary<Texture, GLTexture>();
        }

        public void Initialize()
        {
            spriteBatchShader = ProgramFactory.BuildShaderProgram(Shader.SpriteBatchShader.Source);
            spriteBatchBufferGenerator = new SpriteBatchBufferGenerator(0, 0, 0, 0);
        }
        
        public void AddSurface(Surface surface)
        {
            throw new NotImplementedException();
        }

        public void RemoveSurface(Surface surface)
        {
            throw new NotImplementedException();
        }

        public void AddTexture(Texture texture)
        {
            textureList.Add(texture, TextureFactory.Allocate(texture.Width, texture.Height));
        }

        public void RemoveTexture(Texture texture)
        {
            var tex = textureList[texture];
            tex.Dispose();
            textureList.Remove(texture);
        }

        public void AddBatch(Batch batch)
        {
            if (spriteBatchBufferList.ContainsKey(batch))
                throw new Exception("Batch already present!");

            spriteBatchBufferGenerator.Clear();
            foreach (var operation in batch.GetOperations())
            {
                spriteBatchBufferGenerator.AddRectangle(operation.SourceX, operation.SourceY, operation.SourceWidth, operation.SourceHeight,
                    operation.TargetX, operation.TargetY, operation.TargetWidth, operation.TargetHeight);
            }

            var spriteBatchBuffer = spriteBatchBufferGenerator.GetBuffer();
            spriteBatchBufferList.Add(batch, spriteBatchBuffer);
        }

        public void RemoveBatch(Batch batch)
        {
            if (!spriteBatchBufferList.ContainsKey(batch))
                throw new Exception("Batch not present!");

            spriteBatchBufferList[batch].Dispose();
            spriteBatchBufferList.Remove(batch);
        }

        public void RenderTextToSurface(string text, Surface surface)
        {
            throw new NotImplementedException();
        }

        public void RenderRectangleToSurface(Surface surface)
        {
            throw new NotImplementedException();
        }

        public void RenderImageToSurface(Surface surface)
        {
            throw new NotImplementedException();
        }

        public void RenderShapeToSurface(Surface surface)
        {
            throw new NotImplementedException();
        }

        public void RenderBatchToSurface(Batch batch, Surface surface)
        {
            if (!spriteBatchBufferList.ContainsKey(batch))
                throw new Exception("batch not found!");
            //if surface..

            var spriteBatchBuffer = spriteBatchBufferList[batch];
            spriteBatchBuffer.VertexBuffer.Bind();
            spriteBatchBuffer.IndexBuffer.Bind();
        }


        public void RenderTextToTexture(string text, Texture texture)
        {
            throw new NotImplementedException();
        }

        public void RenderRectangleToTexture(Texture texture)
        {
            throw new NotImplementedException();
        }

        public void RenderImageToTexture(Texture texture)
        {
            throw new NotImplementedException();
        }

        public void RenderShapeToTexture(Texture texture)
        {
            throw new NotImplementedException();
        }

        public void RenderBatchToTexture(Batch batch, Texture texture)
        {
            throw new NotImplementedException();
        }

    }
}
