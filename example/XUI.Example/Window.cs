using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Example
{
    public class Window : GameWindow
    {
        private Canvas canvas;

        public Window()
            : base(1280, 720, GraphicsMode.Default, "XUI Example", GameWindowFlags.Default, DisplayDevice.Default, 4, 0, GraphicsContextFlags.Debug)
        {
            canvas = new Canvas(1280, 720);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }
    }
}
