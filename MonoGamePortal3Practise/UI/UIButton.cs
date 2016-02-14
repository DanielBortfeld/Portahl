using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    public class UIButton : UIElement
    {
        public delegate void UIButtonEvent();
        public event UIButtonEvent OnLeftClick, OnRightClick, OnMiddleClick;

        private Rectangle bounds;

        public Texture2D Image { get; private set; }

        public Vector2 Position
        {
            get { return new Vector2(bounds.X, bounds.Y); }
            set { bounds.X = (int)value.X; bounds.Y = (int)value.Y; }
        }

        private Point mousePosition { get { return InputManager.MouseStateCurrent.Position; } }

        public UIButton(Texture2D image)
        {
            Image = image;
            bounds = image.Bounds;

            InputManager.OnKeyDown += OnClick;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, bounds, Color.White);
        }

        private void OnClick(InputEventArgs eventArgs)
        {
            switch (eventArgs.MouseButton)
            {
                case MouseButtons.None:
                    break;
                case MouseButtons.LeftButton:
                    if (bounds.Contains(mousePosition))
                        if (OnLeftClick != null)
                            OnLeftClick();
                    break;
                case MouseButtons.RightButton:
                    if (bounds.Contains(mousePosition))
                        if (OnRightClick != null)
                            OnRightClick();
                    break;
                case MouseButtons.MiddleButton:
                    if (bounds.Contains(mousePosition))
                        if (OnMiddleClick != null)
                            OnMiddleClick();
                    break;
                default:
                    break;
            }
        }
    }
}
