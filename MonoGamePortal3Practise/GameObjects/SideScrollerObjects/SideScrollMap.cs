using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
	class SideScrollMap : GameObject
	{
		private Texture2D background;
		private BoxCollider ground;
		private BoxCollider leftBorder;
		private BoxCollider rightBorder;

		public SideScrollMap(string name)
		{
			Name = name;

			background = GameManager.LoadTexture2D("background");
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
		}

	}
}
