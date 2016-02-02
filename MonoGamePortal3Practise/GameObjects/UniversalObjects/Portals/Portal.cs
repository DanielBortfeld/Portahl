using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    public abstract class Portal : TopDownEntity
    {
        private BoxCollider collider;

        public override void LoadContent()
        {
            Rectangle spriteRect = GetSpriteRect();
            collider = new BoxCollider(this, spriteRect.Width, spriteRect.Height, true);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Position != Vector2.Zero)
                base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
