
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    class TopDownConditionable : TopDownEntity
    {
        public bool isOn;

        protected TopDownTrigger trigger;

        public void AssignTrigger(string triggerName)
        {
            if (triggerName != null)
                trigger = (TopDownTrigger)SceneManager.CurrentScene.FindGameObject(triggerName);
        }
    }
}
