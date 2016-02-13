using System;

namespace MonoGamePortal3Practise
{
    // TODO: animation for opening/closing door

    public enum DoorPosition { Top, Bottom, Left, Right }

    class TopDownDoor : TopDownConditionable
    {
        public TopDownDoor(int index, DoorPosition doorPosition) : base(index)
        {
            Name = "Door" + doorPosition;
        }
    }
}
