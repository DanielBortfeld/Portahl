// Copyright (c) 2016 Daniel Bortfeld
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
        private DeathTrigger deathTrigger;
        private Cake cake;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetSS");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetSS.xml");

            SideScrollMap sideScrollMap = new SideScrollMap("SideScrollMap");

            player = new SideScrollPlayer(new Vector2(20, sideScrollMap.Background.Height - 400));

            camera = new Camera(player);
            camera.SetBackgroundResolution(sideScrollMap.Background.Width, sideScrollMap.Background.Height);

            victoryTrigger = new VictoryTrigger(1);
            victoryTrigger.Position = new Vector2(0, 360);
            victoryTrigger.SetSize(200, 420);
            victoryTrigger.OnActivation += OnVictory;

            deathTrigger = new DeathTrigger(1);
            deathTrigger.Position = new Vector2(-500, sideScrollMap.Background.Height + 500);
            deathTrigger.SetSize(sideScrollMap.Background.Width + 1000, 512);
            deathTrigger.OnActivation += DeathTrigger_OnActivation;

            cake = new Cake(0, 360);

            GameManager.SetPreferredBackBufferSize(1920, 1080);
        }

        private void DeathTrigger_OnActivation(GameObject activator)
        {
            if (activator == player)
            {
                player.Respawn();
            }
            else if (activator is WeightedCompanionCube)
            {
                ((WeightedCompanionCube)activator).Respawn();
            }
        }

        public override void UnloadContent()
        {
            victoryTrigger.OnActivation -= OnVictory;
            deathTrigger.OnActivation -= DeathTrigger_OnActivation;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            camera.UpdatePosition(SceneManager.graphicsDevice.Viewport);
            Matrix cameraTransform = Matrix.CreateTranslation(-camera.X, -camera.Y, 0);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, null, null, null, cameraTransform);
            gameObjects.ForEach(e => e.Draw(spriteBatch));
            UIManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void OnVictory(GameObject activator)
        {
            victoryTrigger.OnActivation -= OnVictory;
            deathTrigger.OnActivation -= DeathTrigger_OnActivation;
            SceneManager.LoadScene<FinalScreen>();
        }
    }
}
