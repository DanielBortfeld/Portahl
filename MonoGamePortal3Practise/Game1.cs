using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGamePortal3Practise
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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

            GameManager.Content = this.Content;
            GameManager.Graphics = this.graphics;
            SceneManager.graphicsDevice = this.GraphicsDevice;

<<<<<<< HEAD
            SceneManager.LoadScene<SceneTDTutorial>();
=======
            SceneManager.LoadScene<SceneLevelOneTD>();
>>>>>>> 8bb0c244afa36d2bc646a220d65ddd1690d4801d
        }

        protected override void UnloadContent()
        {
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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            SceneManager.DrawScene(spriteBatch);
            UIManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
