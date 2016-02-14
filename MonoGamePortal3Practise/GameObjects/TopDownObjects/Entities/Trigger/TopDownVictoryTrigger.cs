using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class TopDownVictoryTrigger : TopDownTrigger
    {
<<<<<<< HEAD
        public TopDownVictoryTrigger(int index) : base(index)
        {
            Name = "VictoryTrigger";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // dont draw this
        }

        public override void Trigger_OnMove()
        {
            if (Position == player.OffsetPosition)
                TriggerEvent();
=======
        public delegate void VictoryEvent();
        public event VictoryEvent OnVictory;

        public TopDownVictoryTrigger(int index) : base(index)
        {
            Name = "VictoryTrigger";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // dont draw this
        }

        public override void Trigger_OnMove()
        {
            if (Position == player.OffsetPosition)
                if (OnVictory != null)
                    OnVictory();
>>>>>>> 8bb0c244afa36d2bc646a220d65ddd1690d4801d
        }
    }
}
