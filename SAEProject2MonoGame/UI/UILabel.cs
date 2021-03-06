﻿// Copyright (c) 2016 Daniel Bortfeld
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
        public float MaxLineWidth = 0;
        public Color Color = Color.Black;

        private UILabel shadow;

        public UILabel()
        {
        }

        public UILabel(SpriteFont font, string text, Vector2 position, Color color, float scale)
        {
            Font = font;
            Text = text;
            Position = position;
            Color = color;
            Scale = scale;
        }

        public UILabel(SpriteFont font, string text, Vector2 position, float maxLineWidth, Color color, float scale)
        {
            Font = font;
            Text = text;
            Position = position;
            MaxLineWidth = maxLineWidth;
            Color = color;
            Scale = scale;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (MaxLineWidth != 0)
                spriteBatch.DrawString(Font, TextWrapper.WrapText(Font, Text, MaxLineWidth / Scale), Position, Color, 0f /*Rotation*/, Vector2.Zero /*Origin*/, Scale, SpriteEffects.None /*Flip*/, 0 /*LayerDepth*/);
            else
                spriteBatch.DrawString(Font, Text, Position, Color, 0f /*Rotation*/, Vector2.Zero /*Origin*/, Scale, SpriteEffects.None /*Flip*/, 0 /*LayerDepth*/);
        }

        public void AddShadow(Color color, Vector2 offset)
        {
            if (shadow != null)
                UIManager.RemoveElement(shadow);

            UIManager.RemoveElement(this);
            if (MaxLineWidth != 0)
                shadow = new UILabel(Font, Text, Position + offset, MaxLineWidth, color, Scale);
            else
                shadow = new UILabel(Font, Text, Position + offset, MaxLineWidth, color, Scale);
            UIManager.AddElement(this);
        }
    }
}
