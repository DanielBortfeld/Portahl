using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
	class SideScrollMap : GameObject
	{
		private Texture2D background;
        private Floor floor;
        private Wall leftWall;
        private Wall rightWall;

		public SideScrollMap(string name)
		{
			Name = name;
		}

        public override void LoadContent()
        {
            background = GameManager.LoadTexture2D("background");

            floor = new Floor(0, background.Height, background.Width);
            leftWall = new Wall(-10, 0, background.Height);
            rightWall = new Wall(0, background.Width, background.Height);

            base.LoadContent();
        }

		public override void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.Draw(background, background.Bounds, Color.White);
		}
	}
}
