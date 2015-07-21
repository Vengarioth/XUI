using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering;

namespace XUI.Example
{
    public class Window : GameWindow
    {
        private Canvas canvas;
        private Grid grid;
        private Button button1;
        private Button button2;

        private CanvasRenderer canvasRenderer;

        public Window()
            : base(1280, 720, GraphicsMode.Default, "XUI Example", GameWindowFlags.Default, DisplayDevice.Default, 4, 0, GraphicsContextFlags.Debug)
        {
            canvas = new Canvas(1280, 720);
            grid = new Grid();
            button1 = new Button();
            button2 = new Button();
            canvasRenderer = new CanvasRenderer(canvas);

            button1.Width = 100;
            button1.Height = 30;

            button2.Width = 200;
            button2.Height = 70;

            canvas.Child = grid;
            grid.Add(button1);
            grid.Add(button2);

            button1.Background = new ColorBrush() { Color = new Color() { R = 0f, G = 1f, B = 1f, A = 1f } };
            button2.Background = new ColorBrush() { Color = new Color() { R = 1f, G = 0f, B = 1f, A = 1f } };

#if DEBUG
            OpenTK.Graphics.GraphicsContext.CurrentContext.ErrorChecking = true;
            GL.DebugMessageCallback((source, type, id, severity, length, message, userParam) =>
            {
                var msg = Marshal.PtrToStringAnsi(message, length);
                System.Diagnostics.Debug.WriteLine("{0} {1} {2}", severity, type, msg);
            }, IntPtr.Zero);
            GL.Enable(EnableCap.DebugOutput);
#endif
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            canvas.Width = ClientRectangle.Width;
            canvas.Height = ClientRectangle.Height;
            GL.Viewport(ClientRectangle);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            canvas.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            canvasRenderer.Render();

            SwapBuffers();
        }
    }
}
