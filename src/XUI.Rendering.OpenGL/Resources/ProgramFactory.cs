using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL.Resources
{
    static class ProgramFactory
    {
        public static GLProgram BuildShaderProgram(ShaderProgramSource source)
        {
            int programHandle = GL.CreateProgram();
            int vertexShaderHandle = 0,
                tessControlShaderHandle = 0,
                tessEvalShaderHandle = 0,
                geometryShaderHandle = 0,
                fragmentShaderHandle = 0;

            #region build single shaders
            if (source.HasVertexStage)
            {
                vertexShaderHandle = BuildSingleShader(ShaderType.VertexShader, source.VertexSource);
                GL.AttachShader(programHandle, vertexShaderHandle);
            }

            if (source.HasTessControlStage)
            {
                tessControlShaderHandle = BuildSingleShader(ShaderType.TessControlShader, source.TessControlSource);
                GL.AttachShader(programHandle, tessControlShaderHandle);
            }

            if (source.HasTessEvalStage)
            {
                tessEvalShaderHandle = BuildSingleShader(ShaderType.TessEvaluationShader, source.TessEvalSource);
                GL.AttachShader(programHandle, tessEvalShaderHandle);
            }

            if (source.HasGeometryStage)
            {
                geometryShaderHandle = BuildSingleShader(ShaderType.GeometryShader, source.GeometrySource);
                GL.AttachShader(programHandle, geometryShaderHandle);
            }

            if (source.HasFragmentStage)
            {
                fragmentShaderHandle = BuildSingleShader(ShaderType.FragmentShader, source.FragmentSource);
                GL.AttachShader(programHandle, fragmentShaderHandle);
            }
            #endregion
            #region pre linking operations
            #endregion
            #region linking
            GL.LinkProgram(programHandle);
            int programStatus;
            GL.GetProgram(programHandle, GetProgramParameterName.ValidateStatus, out programStatus);
            string infoLog = GL.GetProgramInfoLog(programHandle);
            #endregion
            #region post linking operations
            bool programValid = ValidateShaderProgram(programHandle);
            var uniforms = ExtractUniforms(programHandle);
            var uniformBlocks = ExtractUniformBlocks(programHandle, uniforms);
            var attributes = ExtractAttributes(programHandle);
            #endregion
            #region clear single shaders
            if (source.HasVertexStage)
            {
                GL.DeleteShader(vertexShaderHandle);
            }

            if (source.HasTessControlStage)
            {
                GL.DeleteShader(tessControlShaderHandle);
            }

            if (source.HasTessEvalStage)
            {
                GL.DeleteShader(tessEvalShaderHandle);
            }

            if (source.HasGeometryStage)
            {
                GL.DeleteShader(geometryShaderHandle);
            }

            if (source.HasFragmentStage)
            {
                GL.DeleteShader(fragmentShaderHandle);
            }
            #endregion

            var signature = new GLVertexSignature(attributes);
            return new GLProgram(programValid, programHandle, signature, uniformBlocks, uniforms, infoLog);
        }

        private static bool ValidateShaderProgram(int programHandle)
        {
            string infoString = string.Empty;
            int statusCode = 0;
            GL.GetProgram(programHandle, GetProgramParameterName.ValidateStatus, out statusCode);

            return statusCode == 1;
        }

        private static GLUniformBlock[] ExtractUniformBlocks(int programHandle, GLUniform[] uniforms)
        {
            int uniformBlockCount;
            GL.GetProgram(programHandle, GetProgramParameterName.ActiveUniformBlocks, out uniformBlockCount);

            GLUniformBlock[] uniformBlocks = new GLUniformBlock[uniformBlockCount];

            for (int i = 0; i < uniformBlockCount; i++)
            {
                int nameLength;
                StringBuilder sb = new StringBuilder();
                GL.GetActiveUniformBlockName(programHandle, i, 100, out nameLength, sb);
                string uniformBlockName = sb.ToString();

                int activeUniforms;
                GL.GetActiveUniformBlock(programHandle, i, ActiveUniformBlockParameter.UniformBlockActiveUniforms, out activeUniforms);
                int[] activeUniformIndices = new int[activeUniforms];
                GL.GetActiveUniformBlock(programHandle, i, ActiveUniformBlockParameter.UniformBlockActiveUniformIndices, activeUniformIndices);
                int dataSize;
                GL.GetActiveUniformBlock(programHandle, i, ActiveUniformBlockParameter.UniformBlockDataSize, out dataSize);

                uniformBlocks[i] = new GLUniformBlock(i, uniformBlockName);
            }

            return uniformBlocks;
        }

        private static GLUniform[] ExtractUniforms(int programHandle)
        {
            int uniformCount;
            GL.GetProgram(programHandle, GetProgramParameterName.ActiveUniforms, out uniformCount);

            GLUniform[] uniforms = new GLUniform[uniformCount];

            for (int i = 0; i < uniformCount; ++i)
            {
                int count;
                ActiveUniformType type;
                string name = GL.GetActiveUniform(programHandle, i, out count, out type);
                int location = GL.GetUniformLocation(programHandle, name);

                int componentCount, componentSize;
                MeasureActiveUniform(type, out componentCount, out componentSize);

                uniforms[i] = new GLUniform(type, i, location, count, name, componentCount, componentSize);
            }

            return uniforms;
        }

        private static GLAttribute[] ExtractAttributes(int programHandle)
        {
            int attributeCount;
            GL.GetProgram(programHandle, GetProgramParameterName.ActiveAttributes, out attributeCount);

            GLAttribute[] attributes = new GLAttribute[attributeCount];

            for (int i = 0; i < attributeCount; ++i)
            {
                int size;
                ActiveAttribType type;
                string name = GL.GetActiveAttrib(programHandle, i, out size, out type);
                int location = GL.GetAttribLocation(programHandle, name);

                attributes[i] = new GLAttribute(type, location, name);
            }

            return attributes;
        }

        private static int BuildSingleShader(ShaderType shaderType, string shaderSource)
        {
            int shaderHandle = GL.CreateShader(shaderType);
            int status;
            GL.ShaderSource(shaderHandle, shaderSource);
            GL.CompileShader(shaderHandle);
            GL.GetShader(shaderHandle, ShaderParameter.CompileStatus, out status);
            string infoLog = GL.GetShaderInfoLog(shaderHandle);

            return shaderHandle;
        }

        private static void MeasureActiveUniform(ActiveUniformType type, out int componentCount, out int componentSize)
        {
            switch (type)
            {
                case ActiveUniformType.Float:
                    componentCount = 1;
                    componentSize = sizeof(float);
                    break;

                case ActiveUniformType.FloatVec2:
                    componentCount = 2;
                    componentSize = sizeof(float);
                    break;

                case ActiveUniformType.FloatVec4:
                    componentCount = 4;
                    componentSize = sizeof(float);
                    break;

                case ActiveUniformType.FloatMat4:
                    componentCount = 16;
                    componentSize = sizeof(float);
                    break;

                case ActiveUniformType.Image2D:
                    componentCount = 1;
                    componentSize = sizeof(int);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
