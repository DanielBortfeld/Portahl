using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    static class SceneManager
    {
        public static Scene CurrentScene { get; private set; }

        private static List<GameObject> addedGameObjects = new List<GameObject>();
        private static List<GameObject> removedGameObjects = new List<GameObject>();

        public static List<GameObject> AddedGameObjects { get { return addedGameObjects; } }
        public static List<GameObject> RemovedGameObjects { get { return removedGameObjects; } }

        public static void LoadScene<T>() where T : Scene, new()
        {
            CurrentScene = new T();
            CurrentScene.LoadContent();
        }

        public static void UpdateScene(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        public static void DrawScene(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            CurrentScene.Draw(spriteBatch);

            spriteBatch.End();
        }

        public static void AddGameObject(GameObject gameObject)
        {
            addedGameObjects.Add(gameObject);
        }

        public static void RemoveGameObject(GameObject gameObject)
        {
            removedGameObjects.Add(gameObject);
        }
    }
}
