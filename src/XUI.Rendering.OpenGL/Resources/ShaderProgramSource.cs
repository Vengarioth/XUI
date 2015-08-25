using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.OpenGL.Resources
{
    public class ShaderProgramSource
    {
        public bool HasVertexStage { get; set; }
        public string VertexSource { get; set; }

        public bool HasTessControlStage { get; set; }
        public string TessControlSource { get; set; }

        public bool HasTessEvalStage { get; set; }
        public string TessEvalSource { get; set; }

        public bool HasGeometryStage { get; set; }
        public string GeometrySource { get; set; }

        public bool HasFragmentStage { get; set; }
        public string FragmentSource { get; set; }
    }
}
