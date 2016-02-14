using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    public class UILabel : UIElement
    {
        public SpriteFont Font;
        public Vector2 Position;
        public string Text;
        public float Scale;
        public Color Color = Color.Black;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color, 0f /*Rotation*/, Vector2.Zero /*Origin*/, Scale, SpriteEffects.None /*Flip*/, 0 /*LayerDepth*/);
        }
    }
}
