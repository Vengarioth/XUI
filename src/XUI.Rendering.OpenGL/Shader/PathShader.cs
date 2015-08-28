using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL.Shader
{
    static class PathShader
    {
        private static string vertexSource = "";
        private static string fragmentSource = "";
        public static Resources.ShaderProgramSource Source { get { return new Resources.ShaderProgramSource() { HasVertexStage = true, VertexSource = vertexSource, HasFragmentStage = true, FragmentSource = fragmentSource }; } }
    }
}
