using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGamePortal3Practise
{
    // TODO: water-esk animation for grill

    public enum GrillDirection { Down, Left }

    class MaterialEmancipationGrill : Conditionable
    {
        private GrillDirection direction;

        public MaterialEmancipationGrill(GrillDirection direction)
        {
            Name = "Grill" + direction;
            this.direction = direction;
        }

        public override void Update(GameTime gameTime)
        {
            isOn = !trigger.IsPressed;

            if (isOn)
                Name = "Grill" + direction;

            else Name = "Grill" + direction + "Offline";

            base.Update(gameTime);
        }
    }
}
