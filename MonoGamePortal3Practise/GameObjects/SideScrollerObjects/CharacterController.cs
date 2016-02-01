using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    public class CharacterController
    {
        public float MoveForce = 10f;
        public float JumpForce = 30f;
        public float JumpCooldown = 0.5f;

        private Vector2 velocity;
        private SideScrollEntity entity;

        private float isGroundedTimeStamp;

        public CharacterController(SideScrollEntity entity)
        {
            this.entity = entity;
        }

        public void Update(GameTime gameTime)
        {
            Move();
        }

        public void ProcessInput()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.D))
                velocity.X = MoveForce;
            else if (keyState.IsKeyDown(Keys.A))
                velocity.X = -MoveForce;
            else
                velocity.X = 0;

            if (/*isGrounded*/ /*&&*/ keyState.IsKeyDown(Keys.Space) && isGroundedTimeStamp > JumpCooldown)
            {
                velocity.Y = -JumpForce;
                isGroundedTimeStamp = 0;
            }
        }

        public void Move()
        {
            entity.Position += velocity;
        }
    }
}
