using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGamePortal3Practise
{
    // TODO: water-esk animation for grill

    public enum GrillDirection { Down, Left }

    class TopDownMaterialEmancipationGrill : TopDownConditionable
    {
        private GrillDirection direction;

        public TopDownMaterialEmancipationGrill(GrillDirection direction)
        {
            Name = "Grill" + direction;
            this.direction = direction;
        }

        public override void LoadContent()
        {
            trigger = (TopDownTrigger)SceneManager.CurrentScene.FindGameObject("Button");
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
