using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    public static class CollisionManager
    {
        private static List<BoxCollider> colliders = new List<BoxCollider>();
        private static List<BoxCollider> removedColliders = new List<BoxCollider>();

        public static void AddCollider(BoxCollider collider)
        {
            colliders.Add(collider);
        }

        public static void Clear()
        {
            colliders.Clear();
        }

        public static void RemoveCollider(BoxCollider collider)
        {
            removedColliders.Add(collider);
        }

        public static void UpdateColliders(GameTime gameTime)
        {
            colliders.ForEach(c => c.UpdatePosition(gameTime));
            CheckCollisions();

            removedColliders.ForEach(c => colliders.Remove(c));
            removedColliders.Clear();
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
