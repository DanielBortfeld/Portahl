using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class PortalGunShot : Entity
    {
        public PortalGunShot(Vector2 position)
        {
            Name = "PortalGunShot";

            Position = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Move(Vector2 direction)
        {
            Position += direction;
        }
    }
}
