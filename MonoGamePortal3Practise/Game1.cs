// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGamePortal3Practise
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            if (GraphicsDevice.DisplayMode.Width < 1920 && GraphicsDevice.DisplayMode.Height < 1080)
            {
                graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                graphics.ApplyChanges();
            }
            else
            {
                graphics.PreferredBackBufferWidth = 1920;
                graphics.PreferredBackBufferHeight = 1080;
                graphics.ApplyChanges();
            }

            if (!graphics.IsFullScreen)
                graphics.ToggleFullScreen();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadGameManager();
            SceneManager.graphicsDevice = GraphicsDevice;

            SceneManager.LoadScene<TitleScreen>();

            InputManager.OnKeyPressed += OnKeyPressed;
        }

        protected override void UnloadContent()
        {
            GameManager.OnToggleMouseVisibilitiy -= OnToggleMouseVisibilitiy;
            GameManager.OnSetMouseVisibility -= OnSetMouseVisibility;
            GameManager.OnGameCompletion -= OnGameCompletion;
            InputManager.OnKeyPressed -= OnKeyPressed;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SceneManager.UpdateScene(gameTime);
            UIManager.Update(gameTime);
            InputManager.Update();
            CollisionManager.UpdateColliders(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SceneManager.DrawScene(spriteBatch);

            base.Draw(gameTime);
        }

        private void LoadGameManager()
        {
            GameManager.Content = Content;
            GameManager.Graphics = graphics;
            GameManager.SetMouseVisibility(IsMouseVisible);
            GameManager.OnToggleMouseVisibilitiy += OnToggleMouseVisibilitiy;
            GameManager.OnSetMouseVisibility += OnSetMouseVisibility;
            GameManager.OnGameCompletion += OnGameCompletion;
        }

        private void OnKeyPressed(InputEventArgs eventArgs)
        {
            switch (eventArgs.Key)
            {
                case Keys.F12:
                    ShowcaseCommands.Toggle();
                    break;
                default:
                    break;
            }
        }

        private void OnSetMouseVisibility()
        {
            IsMouseVisible = GameManager.IsMouseVisible;
        }

        private void OnToggleMouseVisibilitiy()
        {
            IsMouseVisible = !IsMouseVisible;
        }

        private void OnGameCompletion()
        {
            Exit();
        }
    }
}
