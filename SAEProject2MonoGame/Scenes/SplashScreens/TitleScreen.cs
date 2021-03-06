﻿// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class TitleScreen : Scene
    {
        private Texture2D splashScreen;
        private UIButton button;
        private UILabel title;

        public override void LoadContent()
        {
            splashScreen = GameManager.LoadTexture2D("titlescreen");

            button = new UIButton(splashScreen, SceneManager.graphicsDevice.Viewport.Bounds);
            button.OnLeftClick += OnClick;
            button.OnRightClick += OnClick;

            float widthScale = SceneManager.graphicsDevice.Viewport.Bounds.Width / 1920f;
            float heightScale = SceneManager.graphicsDevice.Viewport.Bounds.Height / 1080f;
            float scale = (widthScale + heightScale) / 2f;

            title = new UILabel(Fonts.MonkirtaPursuitNC, "PORTAHL", new Vector2(666, 666) * scale, Color.White, 1.3f * scale);

            //GameManager.SetPreferredBackBufferSize(1920, 1080);
            //if (!GameManager.Graphics.IsFullScreen)
            //    GameManager.ToggleFullScreen();
        }

        public override void UnloadContent()
        {
            button.OnLeftClick -= OnClick;
            button.OnRightClick -= OnClick;
        }

        private void OnClick()
        {
            button.OnLeftClick -= OnClick;
            button.OnRightClick -= OnClick;
            SceneManager.LoadScene<SceneTDTutorial>();
        }
    }
}
