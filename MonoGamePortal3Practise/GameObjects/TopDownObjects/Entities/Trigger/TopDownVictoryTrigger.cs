using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
	class TopDownVictoryTrigger : TopDownTrigger
	{
		public delegate void VictoryEvent();
		public event VictoryEvent OnVictory;

		public TopDownVictoryTrigger()
		{
			Name = "VictoryTrigger";
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
		}

		public override void Trigger_OnMove()
		{
			if (Position == player.OffsetPosition)
				if (OnVictory != null)
					OnVictory();
		}
	}
}