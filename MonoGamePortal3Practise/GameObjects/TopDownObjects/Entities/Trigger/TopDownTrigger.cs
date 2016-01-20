using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
	class TopDownTrigger : TopDownEntity
	{
		public bool IsPressed;
		protected TopDownEntity triggeringEntity;

		public TopDownTrigger()
		{
			GameManager.OnMove += Trigger_OnMove;
		}

		public virtual void Trigger_OnMove() { }
	}
}
