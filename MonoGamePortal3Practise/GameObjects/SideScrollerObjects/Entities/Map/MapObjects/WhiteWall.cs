using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    class WhiteWall : SideScrollEntity
    {
        public WhiteWall(int x, int y)
        {
            Name = "WhiteWall";
            Position.X = x;
            Position.Y = y;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Collider = new BoxCollider(this, SpriteRect.Width, SpriteRect.Height, false);
        }
    }
}
