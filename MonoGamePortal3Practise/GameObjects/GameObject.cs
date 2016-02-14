using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    public abstract class GameObject
    {
        public static Random Random = new Random();
        public static Color White = Color.White;

        public string Name = "GameObject";
        public string Tag = "default";
        public Vector2 Position = Vector2.Zero;

        public GameObject()
        {
            SceneManager.CurrentScene.AddGameObject(this);
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();

        public virtual void Destroy()
        {
            if (this != null)
                SceneManager.CurrentScene.RemoveGameObject(this);
        }
    }
}
