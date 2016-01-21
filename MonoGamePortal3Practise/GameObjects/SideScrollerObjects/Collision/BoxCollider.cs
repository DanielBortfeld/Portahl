using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
	public class BoxCollider
	{
		// rectangle:
		public int X;
		public int Y;
		public int Width;
		public int Height;

        public GameObject GameObject;

		public delegate void CollisionEvent(BoxCollider other);
		public event CollisionEvent OnCollisionEnter, OnCollisionStay, OnCollisionExit;

		private List<BoxCollider> collidingColliders = new List<BoxCollider>();

		public BoxCollider(int x, int y, int width, int height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;

			CollisionManager.AddCollider(this);
		}

        public BoxCollider(GameObject gameObject, int width, int height)
        {
            GameObject = gameObject;
            X = (int)GameObject.Position.X;
            Y = (int)GameObject.Position.Y;
            Width = width;
            Height = height;

            CollisionManager.AddCollider(this);
        }

        /// <summary>
        /// GER:
        /// wenn (diese rechte grenze weiter links als die andere linke grenze ist ||
        /// die andere rechte grenze weiter links als diese linke grenze ist ||
        /// diese untere grenze weiter oben als die andere obere grenze ist ||
        /// die andere untere grenze weiter oben als diese obere grenze ist)
        /// dann keine collision;
        /// </summary>
        public void CheckCollision(BoxCollider other)
		{
			if (X + Width < other.X || other.X + other.Width < X || Y + Height < other.Y || other.Y + other.Height < Y)
			{
				// no collision
				if (collidingColliders.Contains(other))
				{
					// no more colliding
					if (OnCollisionExit != null)
						OnCollisionExit(other);
					collidingColliders.Remove(other);
				}
				return;
			}

			if (collidingColliders.Contains(other))
			{
				// still colliding
				if (OnCollisionStay != null)
					OnCollisionStay(other);
				return;
			}

			// new collison
			collidingColliders.Add(other);
			if (OnCollisionEnter != null)
				OnCollisionEnter(other);
		}
	}
}
