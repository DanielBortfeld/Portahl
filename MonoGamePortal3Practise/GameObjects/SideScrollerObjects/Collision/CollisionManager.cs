﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
	public static class CollisionManager
	{
		private static List<BoxCollider> colliders = new List<BoxCollider>();

		public static void AddCollider(BoxCollider collider)
		{
			colliders.Add(collider);
		}

		public static void Update()
		{
			CheckCollisions();
		}

		private static void CheckCollisions()
		{
			foreach (var colliderA in colliders)
				foreach (var colliderB in colliders)
					if (!colliderA.Equals(colliderB))
						colliderA.CheckCollision(colliderB);
		}
	}
}