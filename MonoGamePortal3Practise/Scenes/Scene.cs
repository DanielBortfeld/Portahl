using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    abstract class Scene
    {
        public Texture2D SpriteSheet;

        public PortalBlue PortalBlue;
        public PortalOrange PortalOrange;

        protected List<SpriteFrame> sprites = new List<SpriteFrame>();
        protected List<GameObject> gameObjects = new List<GameObject>();
        protected List<GameObject> addedGameObjects = new List<GameObject>();
        protected List<GameObject> removedGameObjects = new List<GameObject>();

        public List<GameObject> GameObjects { get { return gameObjects; } }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public Rectangle GetSpriteRect(string name)
        {
            if (sprites.Exists(s => s.Name.Contains(name)))
                return (sprites.Find(s => s.Name.Contains(name))).SourceRect;
            else return Rectangle.Empty;
        }

        public GameObject FindGameObject(string name)
        {
            return gameObjects.Find(g => g.Name.Contains(name));
        }

        public void AddGameObject(GameObject gameObject)
        {
            addedGameObjects.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            removedGameObjects.Add(gameObject);
        }

        public void RemoveGameObject(string name)
        {
            RemoveGameObject(FindGameObject(name));
        }

        public void ResetPortals()
        {
            if (PortalBlue != null)
                PortalBlue.Destroy();
            if (PortalOrange != null)
                PortalOrange.Destroy();
            PortalBlue = new PortalBlue(Vector2.Zero);
            PortalOrange = new PortalOrange(Vector2.Zero);
        }
    }
}
