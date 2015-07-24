using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Vector
{
    public class Shape
    {
        public IEnumerable<Path> Paths { get { return paths; } }

        private List<Path> paths;

        public Shape()
        {
            paths = new List<Path>();
        }

        public void AddPath(Path path)
        {
            paths.Add(path);
        }

        public void RemovePath(Path path)
        {
            paths.Remove(path);
        }
    }
}
