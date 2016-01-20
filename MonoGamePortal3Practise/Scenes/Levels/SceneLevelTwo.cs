using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
	class SceneLevelTwo : Scene
	{
        private Camera camera = new Camera();
        private SideScrollPlayer player;

		private TopDownVictoryTrigger victoryTrigger;
		
        public override void LoadContent()
		{
			SpriteSheet = GameManager.LoadTexture2D("SpriteSheetOne");
			//load sprite frames

			SideScrollMap chamberTwo = new SideScrollMap("ChamberTwo");

			player = new SideScrollPlayer(Vector2.Zero);

            camera.SetTarget(player);

			victoryTrigger = (TopDownVictoryTrigger)addedGameObjects.Find(g => g.Name.Contains("VictoryTrigger"));
			//victoryTrigger.OnVictory += OnVictory;

			//GameManager.Graphics.PreferredBackBufferWidth = 
			//GameManager.Graphics.PreferredBackBufferHeight = 
			//GameManager.Graphics.ApplyChanges();
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
