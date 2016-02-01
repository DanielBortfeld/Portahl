using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    class Wall : GameObject
    {
        private BoxCollider floor;
        private int width;
        private int height;

        public Wall(int x, int y, int height)
        {
            Position = new Vector2(x, y);
            this.height = height;
            this.width = 10;
            floor = new BoxCollider(this, width, height, false);
        }
    }
}
