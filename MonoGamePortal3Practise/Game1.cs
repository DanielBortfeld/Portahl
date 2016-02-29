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
