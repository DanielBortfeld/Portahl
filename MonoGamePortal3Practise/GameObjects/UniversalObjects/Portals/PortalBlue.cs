using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
   public class PortalBlue : Portal
    {
        public PortalBlue(Vector2 position) :base()
        {
            Name = "PortalBlue";
            Position = position;
        }
    }
}
