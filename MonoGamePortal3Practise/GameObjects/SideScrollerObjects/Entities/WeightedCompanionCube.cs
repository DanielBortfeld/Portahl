using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGamePortal3Practise
{
    class WeightedCompanionCube : SideScrollEntity
    {
        private Rectangle spriteRect;

        private Vector2 lastPosition;

        private Vector2 velocity;
        private float gravityForce = 2f;

        private bool isGrounded;
        private float timer;
        private float isGroundedTimeStamp;

        public WeightedCompanionCube():base()
        {
            Name = "Cube";
            Position = new Vector2(400, 0);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            spriteRect = GetSpriteRect();
            collider = new BoxCollider(this, spriteRect.Width, spriteRect.Height, false); 
            collider.OnCollisionEnter += OnCollisionEnter;
            collider.OnCollisionStay += OnCollisionStay;
            collider.OnCollisionExit += OnCollisionExit;
        }

        public override void Update(GameTime gameTime)
        {
            lastPosition = Position;
            Move();

            if (isGrounded)
            {
                isGroundedTimeStamp += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                velocity.Y += gravityForce * timer;
            }

            base.Update(gameTime);
        }

        private void Move()
        {
            Position += velocity;
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject is Floor)
            {
                isGrounded = true;
                timer = 0;
                if (Position.Y != other.Y - spriteRect.Height)
                    Position.Y = other.Y - spriteRect.Height;
                velocity.Y = 0f;
            }
            if (other.GameObject is Wall)
            {
                velocity.X = 0f;
                Position = lastPosition;
            }
            if (!other.IsTrigger)
            {
            }
        }

        private void OnCollisionStay(BoxCollider other)
        {
        }

        private void OnCollisionExit(BoxCollider other)
        {
            if (other.GameObject is Floor)
                isGrounded = false;
        }
    }
}
