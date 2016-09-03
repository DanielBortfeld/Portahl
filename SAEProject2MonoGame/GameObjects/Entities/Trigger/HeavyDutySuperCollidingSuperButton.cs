 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGamePortal3Practise
{
    class HeavyDutySuperCollidingSuperButton : Trigger
    {
        Entity pressingEntity;

        public HeavyDutySuperCollidingSuperButton()
        {
            Name = "Button";
        }

        // with update is kaka
        public override void Update(GameTime gametime)
        {
            foreach (var item in SceneManager.CurrentScene.GameObjects)
            {
                if (item is HeavyDutySuperCollidingSuperButton)
                    continue;

                if (item is Entity && pressingEntity == null)
                {
                    if (Position == ((Entity)item).OffsetPosition)
                    {
                        IsPressed = true;
                        SceneManager.CurrentScene.RemoveGameObject(item);
                        SceneManager.CurrentScene.AddGameObject(item);
                        pressingEntity = (Entity)item;
                    }
                    else
                        IsPressed = false;
                }
                else if (item == pressingEntity)
                {
                    if (Position == pressingEntity.OffsetPosition)
                        IsPressed = true;
                    else
                    {
                        IsPressed = false;
                        pressingEntity = null;
                    }
                }
            }
        }
    }
}
