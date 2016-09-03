// Copyright (c) 2016 Daniel Bortfeld
using System;

namespace MonoGamePortal3Practise
{
    public abstract class TopDownTriggerableObject : TopDownEntity
    {
        public bool isOn;

        protected TopDownTrigger trigger;

        public int ID { get; private set; }

        public TopDownTriggerableObject(int index)
        {
            ID = index;
        }

        public void AssignTrigger(string triggerName)
        {
            if (triggerName != null)
                trigger = (TopDownTrigger)SceneManager.CurrentScene.FindGameObject(triggerName);
        }

        public void AssignTrigger(TopDownTrigger trigger)
        {
            if (trigger != null)
                this.trigger = trigger;
        }
    }
}
