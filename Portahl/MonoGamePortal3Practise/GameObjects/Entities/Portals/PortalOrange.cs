using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    class PortalOrange :Portal
    {
        public PortalOrange(Vector2 position) :base()
        {
            Name = "PortalOrange";
            Position = position;
        }
    }
}
