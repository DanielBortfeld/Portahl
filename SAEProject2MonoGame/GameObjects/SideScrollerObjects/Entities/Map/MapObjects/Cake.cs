// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using System;

namespace MonoGamePortal3Practise
{
    class Cake : SideScrollEntity
    {
        public Cake(int x, int y)
        {
            Name = "Cake";
            Tag = "Lie";
            Position = new Vector2(x, y);
        }
    }
}
