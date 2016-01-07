using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    class GameObject
    {
        public static Random Random = new Random();

        public string Name = "GameObject";

        public GameObject()
        {
            GameManager.AddGameObject(this);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void LoadContent()
        {
        }
    }
}
