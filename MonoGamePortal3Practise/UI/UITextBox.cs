using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    public class UITextBox : UIElement
    {
        public string Title;
        public string Text;
        public float MaxLineWidth = 0;
        public Texture2D Button;
        public Texture2D Background;

        public Vector2 Position;

        public SpriteFont TitleFont = Fonts.MonkirtaPursuitNC;
        public SpriteFont TextFont = Fonts.Verdana;

        private UIImage background;
        private UIButton okButton;
        private UILabel title;
        private UILabel text;

        public UITextBox()
        {
        }

        public UITextBox(Vector2 position, string title, string text, Texture2D background, Texture2D button)
        {
            Position = position;
            Title = title;
            Text = text;
            Background = background;
            Button = button;
        }

        public UITextBox(Vector2 position, string title, string text, float maxLineWidth, Texture2D background, Texture2D button)
        {
            Position = position;
            Title = title;
            Text = text;
            MaxLineWidth = maxLineWidth;
            Background = background;
            Button = button;
        }

        public void Show()
        {
            GameManager.SetMouseVisibility(true);

            background = new UIImage(Background, Position);

            title = new UILabel(TitleFont, Title, Position + new Vector2(380, 25), Color.White, 0.2f);

            text = new UILabel(TextFont, Text, Position + new Vector2(215, 100), MaxLineWidth, Color.White, 0.225f);

            okButton = new UIButton(Button, Position + new Vector2(465, 340));
            okButton.OnLeftClick += OnLeftClick;
        }

        private void OnLeftClick()
        {
            UIManager.RemoveElement(this);
            UIManager.RemoveElement(background);
            UIManager.RemoveElement(title);
            UIManager.RemoveElement(text);
            UIManager.RemoveElement(okButton);
            okButton.OnLeftClick -= OnLeftClick;
            GameManager.SetMouseVisibility(false);
        }
    }
}
