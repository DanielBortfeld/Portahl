using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    public class Entity : GameObject
    {
        public Vector2 StandartPosition;

        public BoxCollider Collider { get; protected set; }

        public Rectangle SpriteRect
        {
            get { return GetSpriteRect(); }
        }

        public Texture2D SpriteSheet
        {
            get { return GetSpriteSheet(); }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void LoadContent()
        {
        }

        public virtual void Move(Vector2 direction)
        {
            Position += direction;
        }

        public override void Destroy()
        {
            if (Collider != null)
                Collider.Remove();
            base.Destroy();
        }

        protected Rectangle GetSpriteRect()
        {
            return SceneManager.CurrentScene.GetSpriteRect(Name);
        }

        protected Texture2D GetSpriteSheet()
        {
            return SceneManager.CurrentScene.SpriteSheet;
        }
    }
}
