using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering
{
    /// <summary>
    /// Terrible terrible renderer, does the job for now.
    /// </summary>
    public class CanvasRenderer
    {
        public Canvas Canvas { get; private set; }
        public IRenderer Renderer { get; private set; }

        public CanvasRenderer(Canvas canvas, IRenderer renderer)
        {
            Canvas = canvas;
            Renderer = renderer;
        }

        public void Render()
        {

        }
    }
}
