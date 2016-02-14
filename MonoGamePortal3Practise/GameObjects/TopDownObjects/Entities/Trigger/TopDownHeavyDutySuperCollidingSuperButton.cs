namespace MonoGamePortal3Practise
{
    class TopDownHeavyDutySuperCollidingSuperButton : TopDownTrigger
    {
        public TopDownHeavyDutySuperCollidingSuperButton(int index) : base(index)
        {
            Name = "Button";
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
<<<<<<< HEAD
                        TriggerEvent();
=======
>>>>>>> 8bb0c244afa36d2bc646a220d65ddd1690d4801d
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
