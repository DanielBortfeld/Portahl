using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    class SceneSideScroller : Scene
    {
        private Camera camera;
        private SideScrollPlayer player;
        private WeightedCompanionCube cube;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetTwo");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetTwo.xml");

            SideScrollMap chamberTwo = new SideScrollMap("ChamberTwo");

            player = new SideScrollPlayer(new Vector2(500, chamberTwo.Background.Height - 10));
            cube = new WeightedCompanionCube(1000, 0);

            camera = new Camera(player);
            camera.SetBackgroundResolution(chamberTwo.Background.Width, chamberTwo.Background.Height);

            GameManager.SetPreferredBackBufferSize(1920, 1080);
            //GameManager.ToggleFullScreen();
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
            SceneManager.LoadScene<FinalScreen>();
        }
    }
}
