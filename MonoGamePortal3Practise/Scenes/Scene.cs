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
            return (sprites.Find(s => s.Name.Contains(name))).SourceRect;
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

    }
}
