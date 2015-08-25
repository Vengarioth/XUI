using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUI.Rendering.OpenGL;

namespace XUI.Rendering.TexturePacking
{
    class TextureAtlas
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public IEnumerable<TextureAtlasTreeNode> Nodes { get { return searchTable.Values.AsEnumerable(); } }

        private TextureAtlasTreeNode root;
        private Dictionary<int, TextureAtlasTreeNode> searchTable;

        public TextureAtlas(int width, int height)
        {
            Width = width;
            Height = height;

            root = new TextureAtlasTreeNode(new TextureAtlasArea(0, 0, width, height));
            searchTable = new Dictionary<int, TextureAtlasTreeNode>();
        }

        public TextureAtlasTreeNode ReserveArea(int width, int height, int id)
        {
            var node = root.Insert(width, height, id);
            if (node == null)
                return null;

            searchTable.Add(id, node);
            return node;
        }
    }
}
