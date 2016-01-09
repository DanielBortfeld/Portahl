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
        public Portal() : base()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Position != Vector2.Zero)
                base.Draw(spriteBatch);
        }
    }
}
