using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGamePortal3Practise
{
    public enum MouseButtons { None, LeftButton, RightButton, MiddleButton }

    public class InputEventArgs : EventArgs
    {
        private readonly Keys key;
        private readonly MouseButtons button;

        public Keys Key { get { return key; } }

        public MouseButtons MouseButton { get { return button; } }

        public InputEventArgs(Keys key)
        {
            this.key = key;
            button = MouseButtons.None;
        }

        public InputEventArgs(MouseButtons button)
        {
            this.button = button;
            key = Keys.None;
        }
    }
}
