using Microsoft.Xna.Framework;
using System;

namespace MonoGamePortal3Practise
{
    // TODO: water-esk animation for grill

    public enum GrillDirection { Down, Left }

    class TopDownMaterialEmancipationGrill : TopDownConditionable
    {
        private GrillDirection direction;

        public TopDownMaterialEmancipationGrill(int index, GrillDirection direction) : base(index)
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
