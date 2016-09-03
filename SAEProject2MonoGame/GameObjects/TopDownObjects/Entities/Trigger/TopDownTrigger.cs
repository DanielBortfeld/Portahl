// Copyright (c) 2016 Daniel Bortfeld
using System;

namespace MonoGamePortal3Practise
{
    public abstract class TopDownTrigger : TopDownEntity
    {
        public delegate void ActivationEventhandler(GameObject activator);
        public event ActivationEventhandler OnActivation;

        public bool IsPressed;
        protected TopDownEntity triggeringEntity;

        public int ID { get; private set; }

        public TopDownTrigger(int index)
        {
            Tag = "Trigger";
            ID = index;
            GameManager.OnMove += Trigger_OnMove;
        }

        public void TriggerEvent(GameObject activator)
        {
            OnActivation?.Invoke(activator);
        }

        public abstract void Trigger_OnMove();

        public override void Destroy()
        {
            GameManager.OnMove -= Trigger_OnMove;
            base.Destroy();
        }
    }
}
