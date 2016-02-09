using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    public enum SideDirections { None, Left, Right }

    public class Movement
    {
        public float MoveForce = 10f;
        public float JumpForce = 30f;
        public float JumpCooldown = 0.1f;

        public float GravityForce = 2f;
        public float AccelerationMultipier;

        private float isGroundedTimeStamp;

        private Vector2 velocity;

        public Vector2 Velocity { get { return velocity; } }
        public GameObject GameObject { get; private set; }

        public Movement(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public void UpdatePosition()
        {
            GameObject.Position += velocity;
        }

        // ProcessInput Options:
        public void Move(SideDirections direction)
        {
            switch (direction)
            {
                case SideDirections.Right:
                    velocity.X = MoveForce;
                    break;
                case SideDirections.Left:
                    velocity.X = -MoveForce;
                    break;
                case SideDirections.None:
                    velocity.X = 0f;
                    break;
                default:
                    velocity.X = 0f;
                    break;
            }
        }

        public void Jump()
        {
            if (isGroundedTimeStamp > JumpCooldown)
            {
                velocity.Y = -JumpForce;
                isGroundedTimeStamp = 0;
            }
        }

        public void ApplyGravity(GameTime gameTime)
        {
            AccelerationMultipier += (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity.Y += GravityForce * AccelerationMultipier;
        }

        public void SetIsGroundedTimeStamp(GameTime gameTime)
        {
            isGroundedTimeStamp += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void ResetVelocityY()
        {
            velocity.Y = 0f;
        }

        public void ResetVelocityX()
        {
            velocity.X = 0f;
        }

        public void ResetVelocity()
        {
            velocity = Vector2.Zero;
        }
    }
}
