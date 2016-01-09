﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MonoGamePortal3Practise
{
    static class GameManager
    {
        public static ContentManager Content;
        public static GraphicsDeviceManager Graphics;

        public static Texture2D SpriteSheet;

        private static List<Sprite> sprites = new List<Sprite>();
        private static List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> addedGameObjects = new List<GameObject>();
        private static List<GameObject> removedGameObjects = new List<GameObject>();

        public static List<GameObject> GameObjects
        {
            get { return gameObjects; }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var gameObject in gameObjects)
                gameObject.Draw(spriteBatch);

            spriteBatch.End();
        }

        public static void Update(GameTime gameTime)
        {
            gameObjects.AddRange(addedGameObjects);
            foreach (var gameObject in addedGameObjects)
                gameObject.LoadContent();
            addedGameObjects.Clear();

            foreach (var gameObject in gameObjects)
                gameObject.Update(gameTime);

            foreach (var gameObject in removedGameObjects)
                gameObjects.Remove(gameObject);
            removedGameObjects.Clear();
        }

        public static Texture2D LoadTexture2D(string name)
        {
            return Content.Load<Texture2D>(name);
        }

        public static Rectangle GetSpriteRect(string name)
        {
            return ((Sprite)sprites.Find(s => s.Name.Contains(name))).SourceRect;
        }

        public static void LoadSprites(string dataPath)
        {
            XmlReader xmlReader = XmlReader.Create(dataPath);

            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement("SubTexture"))
                {
                    Sprite sprite = new Sprite();

                    sprite.Name = xmlReader.GetAttribute("name"); ;
                    sprite.SourceRect.X = Convert.ToInt32(xmlReader.GetAttribute("x"));
                    sprite.SourceRect.Y = Convert.ToInt32(xmlReader.GetAttribute("y"));
                    sprite.SourceRect.Width = Convert.ToInt32(xmlReader.GetAttribute("width"));
                    sprite.SourceRect.Height = Convert.ToInt32(xmlReader.GetAttribute("height"));
                    sprites.Add(sprite);
                }
            }
        }

        public static void LoadTriggerConnections(string mapName, string dataPath)
        {
            XmlReader xmlReader = XmlReader.Create(dataPath);

            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement(mapName))
                {

                }
            }
        }

        public static GameObject FindGameObject(string name)
        {
            return gameObjects.Find(g => g.Name.Contains(name));
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