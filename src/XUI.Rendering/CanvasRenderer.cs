using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering.OpenGL;
using XUI.Rendering.VectorGraphics;
using XUI.Vector;

namespace XUI.Rendering
{
    /// <summary>
    /// Terrible terrible renderer, does the job for now.
    /// </summary>
    public class CanvasRenderer
    {
        public Canvas Canvas { get; private set; }

        private GLProgram shader;
        VectorGraphics.VectorMesh vectorMesh;
        GLVertexArrayObject vao;

        public CanvasRenderer(Canvas canvas)
        {
            Canvas = canvas;

            var shaderSource = new ShaderProgramSource()
            {
                HasVertexStage = true,
                VertexSource = @"
#version 410

layout(location = 0)in vec4 in_pos_uv;
layout(location = 2)in vec4 in_color_sign;

layout(location = 0)out vec2 out_uv;
layout(location = 1)out vec3 out_color;
layout(location = 2)out float out_sign;

void main(void)
{
   gl_Position = vec4(in_pos_uv.x, in_pos_uv.y, 0f, 1f);
   out_uv = vec2(in_pos_uv.z, in_pos_uv.w);
   out_color = in_color_sign.xyz;
   out_sign = in_color_sign.w;
}",
                HasFragmentStage = true,
                FragmentSource = @"
#version 410

layout(location = 0)in vec2 uv;
layout(location = 1)in vec3 color;
layout(location = 2)in float sign;

layout(location = 0)out vec4 frag_color;

void main (void)
{
    vec4 c = vec4(color, 1.0);

    vec2 px = dFdx(uv);
    vec2 py = dFdy(uv);

    float fx = (2 * uv.x) * px.x - px.y;
    float fy = (2 * uv.x) * py.x - py.y;

    float sd = (uv.x * uv.x - uv.y) / sqrt(fx * fx + fy * fy);

    float alpha = 0.5 - sd;

    if(sign > 0)
    {
        if(alpha > 1)
            c.a = 1;
        else if(alpha < 0)
            discard;
        else
            c.a = alpha;
    }else{
        if(alpha > 1)
            discard;
        else if(alpha < 0)
            c.a = 1;
        else
            c.a = 1.0 - alpha;
    }

    frag_color = c;
}"
            };

            shader = ProgramFactory.BuildShaderProgram(shaderSource);

            var shape = new Shape();

            var outerPath = new Path();
            outerPath.AddPathSegment(new QuadraticCurveSegment(new Point(0, -0.9), new Point(-0.9, -0.9), new Point(-0.9, 0)) { Convex = true });
            outerPath.AddPathSegment(new QuadraticCurveSegment(new Point(-0.9, 0), new Point(-0.9, 0.9), new Point(0, 0.9)) { Convex = true });
            outerPath.AddPathSegment(new QuadraticCurveSegment(new Point(0, 0.9), new Point(0.9, 0.9), new Point(0.9, 0)) { Convex = true });
            outerPath.AddPathSegment(new QuadraticCurveSegment(new Point(0.9, 0), new Point(0.9, -0.9), new Point(0, -0.9)) { Convex = true });
            
            var innerPath = new Path();
            innerPath.CompositMode = CompositMode.Subtract;
            double offset = 0.3;
            innerPath.AddPathSegment(new QuadraticCurveSegment(new Point(0 + offset, -0.1 + offset), new Point(-0.1 + offset, -0.1 + offset), new Point(-0.1 + offset, 0 + offset)));
            innerPath.AddPathSegment(new QuadraticCurveSegment(new Point(-0.1 + offset, 0 + offset), new Point(-0.1 + offset, 0.1 + offset), new Point(0 + offset, 0.1 + offset)));
            innerPath.AddPathSegment(new QuadraticCurveSegment(new Point(0 + offset, 0.1 + offset), new Point(0.1 + offset, 0.1 + offset), new Point(0.1 + offset, 0 + offset)));
            innerPath.AddPathSegment(new QuadraticCurveSegment(new Point(0.1 + offset, 0 + offset), new Point(0.1 + offset, -0.1 + offset), new Point(0 + offset, -0.1 + offset)));

            var innerPath2 = new Path();
            innerPath2.CompositMode = CompositMode.Subtract;
            offset = -0.3;
            innerPath2.AddPathSegment(new QuadraticCurveSegment(new Point(0 + offset, -0.1 + offset), new Point(-0.1 + offset, -0.1 + offset), new Point(-0.1 + offset, 0 + offset)));
            innerPath2.AddPathSegment(new QuadraticCurveSegment(new Point(-0.1 + offset, 0 + offset), new Point(-0.1 + offset, 0.1 + offset), new Point(0 + offset, 0.1 + offset)));
            innerPath2.AddPathSegment(new QuadraticCurveSegment(new Point(0 + offset, 0.1 + offset), new Point(0.1 + offset, 0.1 + offset), new Point(0.1 + offset, 0 + offset)));
            innerPath2.AddPathSegment(new QuadraticCurveSegment(new Point(0.1 + offset, 0 + offset), new Point(0.1 + offset, -0.1 + offset), new Point(0 + offset, -0.1 + offset)));
            
            shape.AddPath(outerPath);
            shape.AddPath(innerPath);
            shape.AddPath(innerPath2);

            vectorMesh = Poly2TriTessellator.TriangulateShape(shape);
            vao = shader.GetVAO(vectorMesh.VertexBuffer, vectorMesh.IndexBuffer);

            GL.ClearColor(0f, 0f, 0f, 0f);
            GL.ClearDepth(1.0);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.StencilTest);
            GL.StencilFunc(StencilFunction.Always, 0x1, 0x1);
            GL.Enable(EnableCap.Blend);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        public void Render()
        {
            RenderPath();
        }

        public void RenderShape(Shape shape)
        {
            if (vectorMesh != null)
                vectorMesh.Dispose();

            if (vao != null)
                vao.Dispose();

            GL.Disable(EnableCap.CullFace);
            shader.Use();

            vectorMesh = Poly2TriTessellator.TriangulateShape(shape);
            vao = shader.GetVAO(vectorMesh.VertexBuffer, vectorMesh.IndexBuffer);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.BindVertexArray(vao.Handle);
            GL.DrawElements(BeginMode.Triangles, vectorMesh.IndexCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
            GL.Enable(EnableCap.CullFace);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        }

        private void RenderPath()
        {
            GL.Disable(EnableCap.CullFace);
            shader.Use();
            
            GL.BindVertexArray(vao.Handle);
            GL.DrawElements(BeginMode.Triangles, vectorMesh.IndexCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
            GL.Enable(EnableCap.CullFace);
        }

        /**
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
                GL.BindVertexArray(0);
            }
            
            foreach(var child in element.GetChildren())
            {
                RenderElement(child as UIElement, targetSpace);
            }
        }
        */
    }
}
