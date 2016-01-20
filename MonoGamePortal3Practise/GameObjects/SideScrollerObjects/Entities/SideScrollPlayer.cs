using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
	class SideScrollPlayer : SideScrollEntity
	{
		private BoxCollider collider;
		private bool isGrounded;

		public SideScrollPlayer(Vector2 position)
		{
			Name = "Chell";
			StandartPosition = position;
			Position = position;
			collider = new BoxCollider(1,1,1,1);
			collider.OnCollisionEnter += OnCollisionEnter;

		}

		private void OnCollisionEnter()
		{
			throw new NotImplementedException();
		}
	}
}
