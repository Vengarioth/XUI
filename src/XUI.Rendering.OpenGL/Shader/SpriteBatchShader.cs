using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL.Shader
{
    static class SpriteBatchShader
    {
        private static string vertexSource = @"
#version 440
layout(location = 0)in vec4 in_xyuv;

layout(location = 0)out vec2 out_uv;

void main(void)
{
   gl_Position = vec4(in_xyuv.xy, 0.0, 1.0);
   out_uv = vec2(in_xyuv.zw);
}
";
        private static string fragmentSource = @"
#version 440
layout(binding=0) uniform sampler2D atlas;

layout(location = 0)in vec2 in_uv;

void main (void)
{
   gl_FragColor = texture(atlas, in_uv);
}
";
        public static Resources.ShaderProgramSource Source { get { return new Resources.ShaderProgramSource() { HasVertexStage = true, VertexSource = vertexSource, HasFragmentStage = true, FragmentSource = fragmentSource }; } }
    }
}
