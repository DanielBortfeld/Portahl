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
        private Movement movement;

        private bool isGrounded;

        public WeightedCompanionCube():base()
        {
            Name = "Cube";
            Position = new Vector2(400, 0);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            movement = new Movement(this);

            spriteRect = GetSpriteRect();
            collider = new BoxCollider(this, spriteRect.Width, spriteRect.Height, false); 
            collider.OnCollisionEnter += OnCollisionEnter;
            collider.OnCollisionStay += OnCollisionStay;
            collider.OnCollisionExit += OnCollisionExit;
        }

        public override void Update(GameTime gameTime)
        {
            lastPosition = Position;
            movement.UpdatePosition();

            if (isGrounded)
                movement.SetIsGroundedTimeStamp(gameTime);
            else
                movement.ApplyGravity(gameTime);

            base.Update(gameTime);
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject is Floor)
            {
                isGrounded = true;
                movement.AccelerationMultipier = 0;
                if (Position.Y != other.Y - spriteRect.Height)
                    Position.Y = other.Y - spriteRect.Height;
                movement.ResetVelocityY();
            }
            if (other.GameObject is Wall)
            {
                movement.ResetVelocityX();
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
