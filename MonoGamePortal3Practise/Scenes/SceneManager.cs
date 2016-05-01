// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    static class SceneManager
    {
        public static GraphicsDevice graphicsDevice;

        public static Scene CurrentScene { get; private set; }

        public static void LoadScene<T>() where T : Scene, new()
        {
            Clear();
            CurrentScene = new T();
            CurrentScene.LoadContent();
            CurrentScene.ResetPortals();
        }

        public static void UpdateScene(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        public static void DrawScene(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            CurrentScene.Draw(spriteBatch);
        }

        public static Portal GetDestinationPortal(Portal enteredPortal)
        {
            if (enteredPortal is PortalOrange)
                return CurrentScene.PortalBlue;
            else
                return CurrentScene.PortalOrange;
        }

        private static void Clear()
        {
            ClearCurrentScene();
            ClearManagers();
        }

        private static void ClearCurrentScene()
        {
            if (CurrentScene != null)
            {
                CurrentScene.Clear();
                CurrentScene.UnloadContent();
            }
        }

        private static void ClearManagers()
        {
            UIManager.Clear();
            CollisionManager.Clear();
        }
    }
}
