// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    public static class Fonts
    {
        public static SpriteFont Arial;
        public static SpriteFont Verdana;
        public static SpriteFont Planewalker;
        public static SpriteFont MonkirtaPursuitNC;

        static Fonts()
        {
            Arial = GameManager.Content.Load<SpriteFont>("arial72");
            Verdana = GameManager.Content.Load<SpriteFont>("verdana72");
            Planewalker = GameManager.Content.Load<SpriteFont>("planewalker72");
            MonkirtaPursuitNC = GameManager.Content.Load<SpriteFont>("monkirtaPursuitNC72");
        }
    }
}
