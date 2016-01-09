
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    class Conditionable : Entity
    {
        public bool isOn;

        protected Trigger trigger;

        public void AssignTrigger(string triggerName)
        {
            if (triggerName != null)
                trigger = (Trigger)GameManager.FindGameObject(triggerName);
        }
    }
}
