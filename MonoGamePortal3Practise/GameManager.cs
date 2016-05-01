// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    static class GameManager
    {
        public delegate void MoveEvent();
        public static event MoveEvent OnMove;

        public delegate void GameEvent();
        public static event GameEvent OnToggleMouseVisibilitiy, OnSetMouseVisibility, OnGameCompletion;

        public static ContentManager Content;
        public static GraphicsDeviceManager Graphics;

        public static bool IsMouseVisible { get; private set; }

        public static Texture2D LoadTexture2D(string name)
        {
            return Content.Load<Texture2D>(name);
        }

        public static void SetPreferredBackBufferSize(int width, int height)
        {
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.ApplyChanges();
        }

        public static void ReportMove()
        {
            if (OnMove != null)
                OnMove();
        }

        public static void ToggleFullScreen()
        {
            Graphics.ToggleFullScreen();
        }

        public static void SetMouseVisibility(bool isVisible)
        {
            IsMouseVisible = isVisible;
            if (OnSetMouseVisibility != null)
                OnSetMouseVisibility();
        }

        public static void ToggleMouseVisibility()
        {
            if (OnToggleMouseVisibilitiy != null)
                OnToggleMouseVisibilitiy();
        }

        public static void Exit()
        {
            if (OnGameCompletion != null)
                OnGameCompletion();
        }
    }
}
