using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MonoGamePortal3Practise
{
	class TitleScreen : Scene
	{
		Texture2D splashScreen;

        public override void LoadContent()
        {
            splashScreen = GameManager.LoadTexture2D("background");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(splashScreen, splashScreen.Bounds, Color.White);
            spriteBatch.End();
        }
    }
}
