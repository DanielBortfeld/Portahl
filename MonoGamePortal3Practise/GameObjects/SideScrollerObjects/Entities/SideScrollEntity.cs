using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGamePortal3Practise
{
    public class SideScrollEntity : Entity
    {
        protected BoxCollider collider;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SceneManager.CurrentScene.SpriteSheet, Position, SceneManager.CurrentScene.GetSpriteRect(Name), Color.White);
        }

        private void Teleport()
        {

        }
    }
}
