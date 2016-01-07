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

            GameManager.Graphics = this.graphics;
            //graphics.IsFullScreen = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameManager.Content = this.Content;

            GameManager.SpriteSheet = GameManager.LoadTexture2D("SpriteSheet");
            GameManager.LoadSprites(Content.RootDirectory + "/spritesheet.xml");

            Map chamberOne = new Map("ChamberOne");
            chamberOne.LoadMapFromImage(GameManager.LoadTexture2D("PortalChamberOneTiles"));
            chamberOne.LoadSpritesFromImage(GameManager.LoadTexture2D("PortalChamberOneSprites"));

            Player player = new Player();

            graphics.PreferredBackBufferWidth = chamberOne.Width * chamberOne.TileWidth;
            graphics.PreferredBackBufferHeight = chamberOne.Height * chamberOne.TileHeight;
            graphics.ApplyChanges();
        }

        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
