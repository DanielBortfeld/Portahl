using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    class Cake : SideScrollEntity
    {
        public Cake(int x, int y)
        {
            Name = "Cake";
            Tag = "Lie";
            Position = new Vector2(x, y);
        }
    }
}
