﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
   public class GameObject
    {
        public static Random Random = new Random();

        public string Name = "GameObject";
        public Vector2 Position = Vector2.Zero;

        public GameObject()
        {
            SceneManager.CurrentScene.AddGameObject(this);
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
