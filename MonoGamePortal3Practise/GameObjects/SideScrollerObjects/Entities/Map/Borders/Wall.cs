using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    class Wall : GameObject
    {
        private BoxCollider wall;
        private int width;
        private int height;

        public Wall(int x, int y, int height)
        {
            Name = "BorderWall";
            Tag = "Wall";
            Position = new Vector2(x, y);
            this.height = height;
            this.width = 10;
            wall = new BoxCollider(this, width, height, false);
        }

        public override void LoadContent()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
