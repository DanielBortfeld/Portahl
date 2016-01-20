using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    static class SceneManager
    {
        public static GraphicsDevice graphicsDevice;

        public static Scene CurrentScene { get; private set; }

        public static void LoadScene<T>() where T : Scene, new()
        {
            CurrentScene = new T();
            CurrentScene.LoadContent();
        }

        public static void UpdateScene(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        public static void DrawScene(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            CurrentScene.Draw(spriteBatch);
        }

    }
}
