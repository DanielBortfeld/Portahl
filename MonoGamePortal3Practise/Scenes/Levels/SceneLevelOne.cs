using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace MonoGamePortal3Practise
{
    class SceneLevelOne : Scene
    {
        private List<SpriteFrame> sprites = new List<SpriteFrame>();
        private List<GameObject> gameObjects = new List<GameObject>();

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheet");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheet.xml");

            Map chamberOne = new Map("ChamberOne");
            chamberOne.LoadMapFromImage(GameManager.LoadTexture2D("PortalChamberOneTiles"));
            chamberOne.LoadSpritesFromImage(GameManager.LoadTexture2D("PortalChamberOneSprites"));

            Player player = new Player();

            GameManager.Graphics.PreferredBackBufferWidth = chamberOne.Width * chamberOne.TileWidth;
            GameManager.Graphics.PreferredBackBufferHeight = chamberOne.Height * chamberOne.TileHeight;
            GameManager.Graphics.ApplyChanges();
        }

        public override void Update(GameTime gameTime)
        {
            gameObjects.AddRange(SceneManager.AddedGameObjects);
            SceneManager.AddedGameObjects.ForEach(e => e.LoadContent());
            SceneManager.AddedGameObjects.Clear();

            gameObjects.ForEach(e => e.Update(gameTime));

            SceneManager.RemovedGameObjects.ForEach(e => gameObjects.Remove(e));
            SceneManager.RemovedGameObjects.Clear();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            gameObjects.ForEach(e => e.Draw(spriteBatch));
        }

        public override Rectangle GetSpriteRect(string name)
        {
            return ((SpriteFrame)sprites.Find(s => s.Name.Contains(name))).SourceRect;
        }

        public void LoadSprites(string dataPath)
        {
            XmlReader xmlReader = XmlReader.Create(dataPath);

            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement("SubTexture"))
                {
                    SpriteFrame sprite = new SpriteFrame();

                    sprite.Name = xmlReader.GetAttribute("name"); ;
                    sprite.SourceRect.X = Convert.ToInt32(xmlReader.GetAttribute("x"));
                    sprite.SourceRect.Y = Convert.ToInt32(xmlReader.GetAttribute("y"));
                    sprite.SourceRect.Width = Convert.ToInt32(xmlReader.GetAttribute("width"));
                    sprite.SourceRect.Height = Convert.ToInt32(xmlReader.GetAttribute("height"));
                    sprites.Add(sprite);
                }
            }
        }

        //public static void LoadTriggerConnections(string mapName, string dataPath)
        //{
        //    XmlReader xmlReader = XmlReader.Create(dataPath);

        //    while (xmlReader.Read())
        //    {
        //        if (xmlReader.IsStartElement(mapName))
        //        {

        //        }
        //    }
        //}

        public override GameObject FindGameObject(string name)
        {
            return gameObjects.Find(g => g.Name.Contains(name));
        }
    }
}
