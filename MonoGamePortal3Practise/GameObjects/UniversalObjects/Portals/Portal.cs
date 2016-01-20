using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class Portal : Entity
    {
        private BoxCollider collider;

        public Portal() : base()
        {
            collider = new BoxCollider((int)Position.X, (int)Position.Y, 30, 40); ////
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Position != Vector2.Zero)
                base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            collider.X = (int)Position.X;
            collider.Y = (int)Position.Y;
        }
    }
}
