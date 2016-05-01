// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    public class UIImage : UIElement
    {
        public Texture2D Image;
        public Vector2 Position;

        public UIImage()
        {
        }

        public UIImage(Texture2D image, Vector2 position)
        {
            Image = image;
            Position = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, Color.White);
        }
    }
}
