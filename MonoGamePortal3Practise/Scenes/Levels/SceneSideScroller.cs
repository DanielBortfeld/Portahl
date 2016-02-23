using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    class SceneSideScroller : Scene
    {
        private Camera camera;
        private SideScrollPlayer player;

        private VictoryTrigger victoryTrigger;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetSS");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetSS.xml");

            SideScrollMap sideScrollMap = new SideScrollMap("SideScrollMap");

            player = new SideScrollPlayer(new Vector2(20, sideScrollMap.Background.Height - 20));

            camera = new Camera(player);
            camera.SetBackgroundResolution(sideScrollMap.Background.Width, sideScrollMap.Background.Height);

            victoryTrigger = new VictoryTrigger(1);
            victoryTrigger.Position = new Vector2(0, 360);
            victoryTrigger.SetSize(200, 420);
            victoryTrigger.OnActivation += OnVictory;

            GameManager.SetPreferredBackBufferSize(1920, 1080);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            camera.UpdatePosition(SceneManager.graphicsDevice.Viewport);
            Matrix cameraTransform = Matrix.CreateTranslation(-camera.X, -camera.Y, 0);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, cameraTransform);
            gameObjects.ForEach(e => e.Draw(spriteBatch));
            UIManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void OnVictory()
        {
            victoryTrigger.OnActivation -= OnVictory;
            SceneManager.LoadScene<FinalScreen>();
        }
    }
}
