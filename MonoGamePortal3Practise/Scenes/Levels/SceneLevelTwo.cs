using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
	class SceneLevelTwo : Scene
	{
        private Camera camera = new Camera();
        private SideScrollPlayer player;

		private TopDownVictoryTrigger victoryTrigger;
		
        public override void LoadContent()
		{
			SpriteSheet = GameManager.LoadTexture2D("Chell_run");
            //load sprite frames
            //###
            SpriteFrame sprite = new SpriteFrame();  
            sprite.Name = "Chell_run";
            sprite.SourceRect.X = 0;
            sprite.SourceRect.Y = 0;
            sprite.SourceRect.Width = SpriteSheet.Width;
            sprite.SourceRect.Height = SpriteSheet.Height;
            sprites.Add(sprite);
            //###

            SideScrollMap chamberTwo = new SideScrollMap("ChamberTwo");

			player = new SideScrollPlayer(Vector2.Zero);

            camera.SetTarget(player);

			victoryTrigger = (TopDownVictoryTrigger)addedGameObjects.Find(g => g.Name.Contains("VictoryTrigger"));
            //victoryTrigger.OnVictory += OnVictory;

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
	}
}
