﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    public class BoxCollider
    {
        public delegate void CollisionEvent(BoxCollider other);
        public event CollisionEvent OnCollisionEnter, OnCollisionStay, OnCollisionExit;

        public bool IsTrigger;

        // rectangle:
        public int X;
        public int Y;
        public int Width;
        public int Height;

        private List<BoxCollider> collidingColliders = new List<BoxCollider>();

        public int Top { get { return Y; } }
        public int Bottom { get { return Y + Height; } }
        public int Left { get { return X; } }
        public int Right { get { return X + Width; } }

        public GameObject GameObject { get; private set; }

        public delegate void CollisionEvent(BoxCollider other);
        public event CollisionEvent OnCollisionEnter, OnCollisionStay, OnCollisionExit;

        private List<BoxCollider> collidingColliders = new List<BoxCollider>();

        public BoxCollider(GameObject gameObject, int width, int height, bool isTrigger)
        {
            GameObject = gameObject;
            X = (int)GameObject.Position.X;
            Y = (int)GameObject.Position.Y;
            Width = width;
            Height = height;
            IsTrigger = isTrigger;

            CollisionManager.AddCollider(this);
        }

        public void UpdatePosition(GameTime gameTime)
        {
            if (GameObject is Entity && ((Entity)GameObject).SpriteRect != Rectangle.Empty)
            {
                /// center the collider to the middle of the <"SpriteRect">
                X = (int)GameObject.Position.X + (((Entity)GameObject).SpriteRect.Width / 2) - (Width / 2);
                Y = (int)GameObject.Position.Y + (((Entity)GameObject).SpriteRect.Height / 2) - (Height / 2);
            }
            else
            {
                X = (int)GameObject.Position.X;
                Y = (int)GameObject.Position.Y;
            }
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
            if (Right < other.Left || other.Right < Left || Bottom < other.Top || other.Bottom < Top)
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

        public void Remove()
        {
            CollisionManager.RemoveCollider(this);
        }
    }
}