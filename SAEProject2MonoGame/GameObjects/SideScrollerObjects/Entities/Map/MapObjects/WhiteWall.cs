// Copyright (c) 2016 Daniel Bortfeld
using System;

namespace MonoGamePortal3Practise
{
    class WhiteWall : SideScrollEntity
    {
        public WhiteWall(int x, int y)
        {
            Name = "WhiteWall";
            Tag = "Wall";
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
