using System;

namespace MonoGamePortal3Practise
{
    public abstract class TopDownTrigger : TopDownEntity
    {
<<<<<<< HEAD
        public delegate void TriggerEventhandler();
        public event TriggerEventhandler OnActivation;

        public bool IsPressed;
        protected TopDownEntity triggeringEntity;

        public int ID { get; private set; }

=======
        public bool IsPressed;
        protected TopDownEntity triggeringEntity;

        public int ID { get; private set; }

>>>>>>> 8bb0c244afa36d2bc646a220d65ddd1690d4801d
        public TopDownTrigger(int index)
        {
            ID = index;
            GameManager.OnMove += Trigger_OnMove;
        }

<<<<<<< HEAD
        public void TriggerEvent()
        {
            if (OnActivation != null)
                OnActivation();
        }

=======
>>>>>>> 8bb0c244afa36d2bc646a220d65ddd1690d4801d
        public abstract void Trigger_OnMove();

        public override void Destroy()
        {
            GameManager.OnMove -= Trigger_OnMove;

            base.Destroy();
        }
    }
}
