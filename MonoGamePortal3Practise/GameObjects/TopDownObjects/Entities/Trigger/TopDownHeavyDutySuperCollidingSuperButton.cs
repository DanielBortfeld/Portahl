 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGamePortal3Practise
{
    class TopDownHeavyDutySuperCollidingSuperButton : TopDownTrigger
    {
        public TopDownHeavyDutySuperCollidingSuperButton()
        {
            Name = "Button";
        }

		public override void Trigger_OnMove()
        {
            foreach (var item in SceneManager.CurrentScene.GameObjects)
            {
                if (item is TopDownHeavyDutySuperCollidingSuperButton)
                    continue;

                if (item is TopDownEntity && triggeringEntity == null)
                {
                    if (Position == ((TopDownEntity)item).OffsetPosition)
                    {
                        IsPressed = true;
                        SceneManager.CurrentScene.RemoveGameObject(item);
                        SceneManager.CurrentScene.AddGameObject(item);
                        triggeringEntity = (TopDownEntity)item;
                    }
                    else
                        IsPressed = false;
                }
                else if (item == triggeringEntity)
                {
                    if (Position == triggeringEntity.OffsetPosition)
                        IsPressed = true;
                    else
                    {
                        IsPressed = false;
                        triggeringEntity = null;
                    }
                }
            }
        }
    }
}
