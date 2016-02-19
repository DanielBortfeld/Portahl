using Microsoft.Xna.Framework;
using System;

namespace MonoGamePortal3Practise
{
    // TODO: water-esk animation for grill

    public enum GrillDirection { Down, Left }

    class TopDownMaterialEmancipationGrill : TopDownTriggerableObject
    {
        private GrillDirection direction;

        public TopDownMaterialEmancipationGrill(int index, GrillDirection direction) : base(index)
        {
            Name = "Grill" + direction;
            Tag = "Grill";
            this.direction = direction;
        }

        public override void Update(GameTime gameTime)
        {
            if (trigger != null)
                isOn = !trigger.IsPressed;
            else
                isOn = true;

            if (isOn)
                Name = "Grill" + direction;
            else Name = "Grill" + direction + "Offline";

            base.Update(gameTime);
        }
    }
}
