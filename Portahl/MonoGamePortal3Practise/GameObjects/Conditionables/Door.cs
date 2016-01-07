using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    // TODO: animation for opening/closing door

    public enum DoorPosition { Top, Bottom, Left, Right }

    class Door : Conditionable
    {
        public Door(DoorPosition doorPosition) : base()
        {
            Name = "Door" + doorPosition;
        }
    }
}
