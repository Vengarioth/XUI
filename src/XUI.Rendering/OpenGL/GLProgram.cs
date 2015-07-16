using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL
{
    class GLProgram : IGraphicsResource
    {
        public bool Valid { get; private set; }
        public int Handle { get; private set; }
        public GLVertexSignature Signature { get; private set; }
        public GLUniformBlock[] UniformBlocks { get; private set; }
        public GLUniform[] Uniforms { get; private set; }
        public string InfoLog { get; private set; }

        internal GLProgram(bool valid, int programHandle, GLVertexSignature signature, GLUniformBlock[] uniformBlocks, GLUniform[] uniforms, string infoLog = null)
        {
            Valid = valid;
            Handle = programHandle;
            Signature = signature;
            UniformBlocks = uniformBlocks;
            Uniforms = uniforms;
            InfoLog = infoLog;
        }

        public GLVertexArrayObject GetVAO(GLBuffer vertexBuffer, GLBuffer indexBuffer)
        {
            int vaoHandle = GL.GenVertexArray();
            GL.BindVertexArray(vaoHandle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer.Handle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer.Handle);

            var stride = Signature.Attributes.Sum(e => e.SizeInBytes());

            var attributeOffset = 0;
            for (int i = 0; i < Signature.Attributes.Length; i++)
            {
                var attribute = Signature.Attributes[i];

                GL.VertexAttribPointer(attribute.Location, attribute.GetComponentCount(), attribute.GetComponentType(),
                            false, stride, attributeOffset);
                GL.EnableVertexAttribArray(attribute.Location);
                GL.BindAttribLocation(Handle, attribute.Location, attribute.Name);

                attributeOffset += attribute.SizeInBytes();
            }

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            return new GLVertexArrayObject(vaoHandle, this, vertexBuffer, indexBuffer);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public void Dispose()
        {
            GL.DeleteProgram(Handle);
        }

        public override string ToString()
        {
            return "GLProgram " + Handle;
        }
    }
}
