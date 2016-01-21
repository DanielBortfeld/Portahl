using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGamePortal3Practise
{
    class SideScrollEntity : Entity
    {
        protected BoxCollider collider;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SceneManager.CurrentScene.SpriteSheet, Position, SceneManager.CurrentScene.GetSpriteRect(Name), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            collider.X = (int)Position.X;
            collider.Y = (int)Position.Y;
        }

        private void Teleport()
        {

        }
    }
}
