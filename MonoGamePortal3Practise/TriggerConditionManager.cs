using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGamePortal3Practise
{
   static class TriggerConditionManager
    {
        public delegate void TriggerEventHandler();
        public static event TriggerEventHandler OnTriggerIsPressed, OnTriggerIsReleased;

        public static void TriggerWasPressed()
        {
            if (OnTriggerIsPressed != null)
                OnTriggerIsPressed();
        }

        public static void TriggerWasReleased()
        {
            if (OnTriggerIsReleased != null)
                OnTriggerIsReleased();
        }
    }
}
