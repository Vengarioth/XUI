using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering.OpenGL;

namespace XUI.Rendering
{
    /// <summary>
    /// Terrible terrible renderer, does the job for now.
    /// </summary>
    public class CanvasRenderer
    {
        public Canvas Canvas { get; private set; }

        private GLProgram shader;
        GLBuffer vertexBuffer;
        GLBuffer indexBuffer;
        GLVertexArrayObject vao;
        int indexCount;

        public CanvasRenderer(Canvas canvas)
        {
            Canvas = canvas;

            var shaderSource = new ShaderProgramSource()
            {
                HasVertexStage = true,
                VertexSource = @"
in vec4 pos;

void main(void)
{
   gl_Position = pos;
}",
                HasFragmentStage = true,
                FragmentSource = @"
uniform vec4 color;

void main (void)
{
   gl_FragColor = color;
}"
            };

            shader = ProgramFactory.BuildShaderProgram(shaderSource);

            var vertices = new float[] {
                -1f, -1f, 0f, 1f,
                 1f, -1f, 0f, 1f,
                 1f,  1f, 0f, 1f,
                -1f,  1f, 0f, 1f
            };

            var indices = new uint[] {
                0, 1, 2,
                0, 2, 3
            };
            indexCount = indices.Length;

            vertexBuffer = BufferFactory.Allocate(BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw, sizeof(float) * vertices.Length);
            indexBuffer = BufferFactory.Allocate(BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw, sizeof(uint) * indices.Length);

            var verticesInBytes = new byte[vertices.Length * sizeof(float)];
            Buffer.BlockCopy(vertices, 0, verticesInBytes, 0, verticesInBytes.Length);
            vertexBuffer.SetData(ref verticesInBytes);

            var indicesInBytes = new byte[indices.Length * sizeof(uint)];
            Buffer.BlockCopy(indices, 0, indicesInBytes, 0, indicesInBytes.Length);
            indexBuffer.SetData(ref indicesInBytes);

            vao = shader.GetVAO(vertexBuffer, indexBuffer);

            GL.ClearDepth(1.0);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.StencilTest);
            GL.StencilFunc(StencilFunction.Always, 0x1, 0x1);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
        }

        public void Render()
        {
            if (Canvas.Child == null)
                return;

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            RenderElement(Canvas.Child, new Rectangle(0, 0, Canvas.Width, Canvas.Height));
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        }

        private void RenderElement(UIElement element, Rectangle targetSpace)
        {
            if(element is VisualElement)
            {
                var visualElement = element as VisualElement;
                
                shader.Use();
                shader.Uniforms[0].SetFloatComponentType(new float[] { 1f, 1f, 1f, 1f });

                var space = visualElement.Space;

                float x = (float)(space.X / Canvas.Width);
                float y = 1f - (float)(space.Y / Canvas.Height);
                float width = (float)((space.X + space.Width) / Canvas.Width);
                float height = y - (float)(space.Height / Canvas.Height);

                x = (x * 2f) - 1f;
                y = (y * 2f) - 1f;
                width = (width * 2f) - 1f;
                height = (height * 2f) - 1f;

                var vertices = new float[] {
                    x,     height,      0f, 1f,
                    width, height,      0f, 1f,
                    width, y, 0f, 1f,
                    x,     y, 0f, 1f
                };

                var verticesInBytes = new byte[vertices.Length * sizeof(float)];
                Buffer.BlockCopy(vertices, 0, verticesInBytes, 0, verticesInBytes.Length);
                vertexBuffer.SetData(ref verticesInBytes);

                GL.BindVertexArray(vao.Handle);
                GL.DrawElements(BeginMode.Triangles, indexCount, DrawElementsType.UnsignedInt, 0);
            }
            
            foreach(var child in element.GetChildren())
            {
                RenderElement(child as UIElement, targetSpace);
            }
        }
    }
}
