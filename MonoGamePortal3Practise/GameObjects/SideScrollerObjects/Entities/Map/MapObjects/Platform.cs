using System;

namespace MonoGamePortal3Practise
{
    class Platform : SideScrollEntity
    {
        public Platform(int x, int y, string name)
        {
            Name = name;
            Tag = "Ground";
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
