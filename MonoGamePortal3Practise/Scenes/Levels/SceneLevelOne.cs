using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace MonoGamePortal3Practise
{
    class SceneLevelOne : Scene
    {
		private TopDownVictoryTrigger victoryTrigger;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetOne");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetOne.xml");

            TopDownMap chamberOne = new TopDownMap("ChamberOne");
            chamberOne.LoadMapFromImage(GameManager.LoadTexture2D("PortalChamberOneTilesDEBUG"));
            chamberOne.LoadSpritesFromImage(GameManager.LoadTexture2D("PortalChamberOneSprites"));

			TopDownPlayer player = new TopDownPlayer();

			victoryTrigger = (TopDownVictoryTrigger)addedGameObjects.Find(g => g.Name.Contains("VictoryTrigger"));
			victoryTrigger.OnVictory += OnVictory;

            GameManager.Graphics.PreferredBackBufferWidth = chamberOne.Width * chamberOne.TileWidth;
            GameManager.Graphics.PreferredBackBufferHeight = chamberOne.Height * chamberOne.TileHeight;
            GameManager.Graphics.ApplyChanges();
        }

		void OnVictory()
		{
			SceneManager.LoadScene<SceneLevelTwo>();
			victoryTrigger.OnVictory -= OnVictory;
		}

        public override void Update(GameTime gameTime)
        {
            gameObjects.AddRange(addedGameObjects);
            addedGameObjects.ForEach(e => e.LoadContent());
            addedGameObjects.Clear();

            gameObjects.ForEach(e => e.Update(gameTime));

            removedGameObjects.ForEach(e => gameObjects.Remove(e));
            removedGameObjects.Clear();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            gameObjects.ForEach(e => e.Draw(spriteBatch));
            spriteBatch.End();
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
    }
}
