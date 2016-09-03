// Copyright (c) 2016 Daniel Bortfeld
namespace MonoGamePortal3Practise
{
    /// <summary>
    /// It's the red button in the TopDown part of the game.
    /// </summary>
    class TopDownHeavyDutySuperCollidingSuperButton : TopDownTrigger
    {
        public TopDownHeavyDutySuperCollidingSuperButton(int index) : base(index)
        {
            Name = "Button";
            Tag = "Button";
        }

        public override void Trigger_OnMove()
        {
            // check if trigger is pressed
            foreach (var item in SceneManager.CurrentScene.GameObjects)
            {
                if (item is TopDownHeavyDutySuperCollidingSuperButton)
                    continue;

                if (item is TopDownEntity && triggeringEntity == null)
                {
                    if (Position == ((TopDownEntity)item).OffsetPosition)
                    {
                        IsPressed = true;
                        TriggerEvent(item);
                        item.Destroy();
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
