using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class TopDownVictoryTrigger : TopDownTrigger
    {
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
        }
    }
}
