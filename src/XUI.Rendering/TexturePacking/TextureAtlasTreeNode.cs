using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering.TexturePacking
{
    class TextureAtlasTreeNode
    {
        public bool IsLeaf { get { return ChildA == null && ChildB == null; } }

        public TextureAtlasTreeNode ChildA { get; private set; }
        public TextureAtlasTreeNode ChildB { get; private set; }
        public TextureAtlasArea Area { get; private set; }
        public int ID { get; private set; }

        public TextureAtlasTreeNode(TextureAtlasArea area)
        {
            Area = area;
            ID = -1;
        }

        public TextureAtlasTreeNode Insert(int width, int height, int id)
        {
            if(IsLeaf)
            {
                if (ID > 0)
                    return null;

                if (width > Area.Width || height > Area.Height)
                    return null;

                if (width == Area.Width && height == Area.Height)
                {
                    ID = id;
                    return this;
                }

                var dw = Area.Width - width;
                var dh = Area.Height - height;

                TextureAtlasArea areaA, areaB;

                if (dw > dh)
                {
                    areaA = new TextureAtlasArea(Area.X, Area.Y, width, Area.Height);
                    areaB = new TextureAtlasArea(Area.X + width, Area.Y, Area.Width - width, Area.Height);
                }
                else
                {
                    areaA = new TextureAtlasArea(Area.X, Area.Y, Area.Width, height);
                    areaB = new TextureAtlasArea(Area.X, Area.Y + height, Area.Width, Area.Height - height);
                }

                ChildA = new TextureAtlasTreeNode(areaA);
                ChildB = new TextureAtlasTreeNode(areaB);

                return ChildA.Insert(width, height, id);
            }
            else
            {
                var newNode = ChildA.Insert(width, height, id);
                if (newNode != null)
                    return newNode;

                return ChildB.Insert(width, height, id);
            }
        }
    }
}
