using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    class Floor : GameObject
    {
        private BoxCollider floor;
        private int width;
        private int height;

        public Floor(int x, int y, int width)
        {
            Position = new Vector2(x, y);
            this.width = width;
            height = 10;
            floor = new BoxCollider(this, width, height);
        }
    }
}
