using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI
{
    public abstract class Brush
    {
        public bool IsVisible { get { return OnIsVisible(); } }

        protected abstract bool OnIsVisible();
    }
}
