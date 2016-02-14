using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    public static class UIManager
    {
        private static List<UIElement> uiElements = new List<UIElement>();

        public static void AddElement(UIElement element)
        {
            uiElements.Add(element);
        }

        public static void RemoveElement(UIElement element)
        {
            uiElements.Remove(element);
        }

        public static void Clear()
        {
            uiElements.Clear();
        }

        public static void Update(GameTime gameTime)
        {
            uiElements.ForEach(e => e.Update(gameTime));
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            uiElements.ForEach(e => e.Draw(spriteBatch));
        }
    }
}
