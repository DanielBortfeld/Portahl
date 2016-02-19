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

            GameManager.Content = Content;
            GameManager.Graphics = graphics;
            GameManager.SetMouseVisibility(IsMouseVisible);
            GameManager.OnToggleMouseVisibilitiy += OnToggleMouseVisibilitiy;
            GameManager.OnSetMouseVisibility += OnSetMouseVisibility;
            SceneManager.graphicsDevice = GraphicsDevice;

            SceneManager.LoadScene<TitleScreen>();
        }

        protected override void UnloadContent()
        {
            GameManager.OnToggleMouseVisibilitiy -= OnToggleMouseVisibilitiy;
            GameManager.OnSetMouseVisibility -= OnSetMouseVisibility;
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

        private void OnSetMouseVisibility()
        {
            IsMouseVisible = GameManager.IsMouseVisible;
        }

        private void OnToggleMouseVisibilitiy()
        {
            IsMouseVisible = !IsMouseVisible;
        }
    }
}
