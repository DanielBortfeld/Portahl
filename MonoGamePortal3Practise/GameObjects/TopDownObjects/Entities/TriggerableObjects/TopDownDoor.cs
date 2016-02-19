using System;

namespace MonoGamePortal3Practise
{
    // TODO: animation for opening/closing door

    public enum DoorPosition { Top, Bottom, Left, Right }

    class TopDownDoor : TopDownTriggerableObject
    {
        public TopDownDoor(int index, DoorPosition doorPosition) : base(index)
        {
            Name = "Door" + doorPosition;
            Tag = "Door";
        }
    }
}
