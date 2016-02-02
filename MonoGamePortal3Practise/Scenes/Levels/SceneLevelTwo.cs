using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;

namespace MonoGamePortal3Practise
{
    class SceneLevelTwo : Scene
    {
        private Camera camera;
        private SideScrollPlayer player;
        private WeightedCompanionCube cube;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetTwo");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetTwo.xml");

            SideScrollMap chamberTwo = new SideScrollMap("ChamberTwo");

            player = new SideScrollPlayer(new Vector2(10, 1000));
            cube = new WeightedCompanionCube();

            camera = new Camera(player);
            camera.SetBackgroundResolution(chamberTwo.background.Width, chamberTwo.background.Height);

            GameManager.Graphics.PreferredBackBufferWidth = 1920;
            GameManager.Graphics.PreferredBackBufferHeight = 1080;
            GameManager.Graphics.IsFullScreen = true;

            GameManager.Graphics.ApplyChanges();
        }

        private void OnVictory()
        {
            SceneManager.LoadScene<FinalScreen>();
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
            camera.UpdatePosition(SceneManager.graphicsDevice.Viewport);
            Matrix cameraTransform = Matrix.CreateTranslation(-camera.X, -camera.Y, 0);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, cameraTransform);
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

                    sprite.Name = xmlReader.GetAttribute("name");
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
