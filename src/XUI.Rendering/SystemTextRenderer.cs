using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering.OpenGL;

namespace XUI.Rendering
{
    class SystemTextRenderer
    {
        public void RenderText(GLTexture target, string text, int width, int height)
        {
            var image = new Bitmap(width, height);
            var g = Graphics.FromImage(image);
            g.Clear(System.Drawing.Color.Transparent);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString(text, new Font("Tahoma", 8), Brushes.Black, new RectangleF(0, 0, width, height));
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            g.Flush();

            GL.BindTexture(TextureTarget.Texture2D, target.Handle);

            var data = image.LockBits(new System.Drawing.Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, width, height, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            image.UnlockBits(data);
        }
    }
}
