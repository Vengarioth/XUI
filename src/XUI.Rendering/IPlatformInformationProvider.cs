using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering
{
    public interface IPlatformInformationProvider
    {
        int MaxTextureSize { get; }
        int MaxArrayTextureLayers { get; }
    }
}
