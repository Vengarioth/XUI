using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering
{
    public interface IRenderer
    {
        void Initialize();

        void AddSurface(Surface surface);
        void RemoveSurface(Surface surface);

        void AddTexture(Texture texture);
        void RemoveTexture(Texture texture);

        void AddBatch(Batch batch);
        void RemoveBatch(Batch batch);

        void RenderTextToSurface(string text, Surface surface);
        void RenderRectangleToSurface(Surface surface);
        void RenderImageToSurface(Surface surface);
        void RenderShapeToSurface(Surface surface);
        void RenderBatchToSurface(Batch batch, Surface surface);
        
        void RenderTextToTexture(string text, Texture texture);
        void RenderRectangleToTexture(Texture texture);
        void RenderImageToTexture(Texture texture);
        void RenderShapeToTexture(Texture texture);
        void RenderBatchToTexture(Batch batch, Texture texture);
    }
}
