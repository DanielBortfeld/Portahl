using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
  public static  class Fonts
    {
        public static SpriteFont Arial;
        public static SpriteFont Planewalker;
        public static SpriteFont MonkirtaPursuitNC;

        static Fonts()
        {
            Arial = GameManager.Content.Load<SpriteFont>("arial72");
            Planewalker = GameManager.Content.Load<SpriteFont>("planewalker72");
            MonkirtaPursuitNC = GameManager.Content.Load<SpriteFont>("monkirtaPursuitNC72");
        }
    }
}
