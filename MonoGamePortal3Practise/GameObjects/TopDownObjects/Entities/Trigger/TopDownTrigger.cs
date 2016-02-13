using System;

namespace MonoGamePortal3Practise
{
    public abstract class TopDownTrigger : TopDownEntity
    {
        public bool IsPressed;
        protected TopDownEntity triggeringEntity;

        public int ID { get; private set; }

        public TopDownTrigger(int index)
        {
            ID = index;
            GameManager.OnMove += Trigger_OnMove;
        }

        public abstract void Trigger_OnMove();

        public override void Destroy()
        {
            GameManager.OnMove -= Trigger_OnMove;

            base.Destroy();
        }
    }
}
