﻿// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class Floor : GameObject
    {
        private BoxCollider floor;
        private int width;
        private int height;

        public Floor(int x, int y, int width)
        {
            Name = "Floor";
            Tag = "Ground";
            Position = new Vector2(x, y);
            this.width = width;
            height = 50;
            floor = new BoxCollider(this, width, height, false);
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
