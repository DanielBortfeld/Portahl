using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
	class SideScrollMap : GameObject
	{
        private Floor floor;
        private Wall leftWall;
        private Wall rightWall;

		public Texture2D background { get; private set; }

		public SideScrollMap(string name)
		{
			Name = name;
            background = GameManager.LoadTexture2D("backgroundForest");
        }

        public override void LoadContent()
        {
            floor = new Floor(0, background.Height, background.Width);
            leftWall = new Wall(-10, 0, background.Height);
            rightWall = new Wall(background.Width, 0, background.Height);

            base.LoadContent();
        }

		public override void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.Draw(background, background.Bounds, Color.White);
		}
	}
}
