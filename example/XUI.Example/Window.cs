﻿using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering;
using XUI.Vector;

namespace XUI.Example
{
    public class Window : GameWindow
    {
        private Canvas canvas;
        private Grid grid;

        private CanvasRenderer canvasRenderer;

        private int i = 0;
        private TTF.TrueTypeFont ttf;

        public Window()
            : base(1280, 720, GraphicsMode.Default, "XUI Example", GameWindowFlags.Default, DisplayDevice.Default, 4, 0, GraphicsContextFlags.Debug)
        {
            canvas = new Canvas(1280, 720);
            grid = new Grid();
            canvasRenderer = new CanvasRenderer(canvas);

            canvas.Child = grid;

            var rnd = new Random(DateTime.Now.Second);
            for(int i = 0; i < 10; i++)
            {
                var button = new Button();
                button.Width = Math.Max(50, rnd.NextDouble() * 400);
                button.Height = 80;
                button.Background = new ColorBrush() { Color = new Color() { R = 1f, G = 0f, B = 1f, A = 1f } };
                grid.Add(button);
            }

            Keyboard.KeyDown += Keyboard_KeyDown;

            var reader = new XUI.TTF.TTFReader();
            ttf = reader.Read(System.IO.File.OpenRead("./segoeui.ttf"));

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
            canvas.UpdateLayout();
            GL.Viewport(ClientRectangle);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        private void Keyboard_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == OpenTK.Input.Key.Right)
                i = ++i % ttf.Glyf.Glyphs.Length;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            var shape = ttf.Glyf.Glyphs[i].GetAsShape();

            canvasRenderer.RenderShape(shape);

            SwapBuffers();
        }
    }
}
