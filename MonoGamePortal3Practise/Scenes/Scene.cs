using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    abstract class Scene
    {
        public Texture2D SpriteSheet;

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public GameObject FindGameObject(string name)
        {

        }
        public Rectangle GetSpriteRect(string name)
        {

        }
    }
}
