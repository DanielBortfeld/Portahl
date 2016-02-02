using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    class SideScrollPlayer : SideScrollEntity
    {
        public Vector2 Pivot;
        public Vector2 lastPosition;

        private Rectangle spriteRect;

        private Vector2 velocity;
        private int moveForce = 10;
        private int jumpForce = 20;

        private float gravityForce = 2f;
        private float timer;

        private bool isGrounded;
        private float isGroundedTimeStamp;
        private float jumpCooldown = 0.1f;

        public SideScrollPlayer(Vector2 position)
        {
            Name = "Chell";
            spriteRect = GetSpriteRect();

            StandartPosition = position;
            Position = position;
        }

        public override void LoadContent()
        {
            collider = new BoxCollider(this, spriteRect.Width, spriteRect.Height, false);
            collider.OnCollisionEnter += OnCollisionEnter;
            collider.OnCollisionStay += OnCollisionStay;
            collider.OnCollisionExit += OnCollisionExit;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            lastPosition = Position;
            Move();

            ProcessInput();

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

        public void ProcessInput()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.D))
                velocity.X = moveForce;
            else if (keyState.IsKeyDown(Keys.A))
                velocity.X = -moveForce;
            else
                velocity.X = 0;

            if (isGrounded && keyState.IsKeyDown(Keys.Space) && isGroundedTimeStamp > jumpCooldown)
            {
                velocity.Y = -jumpForce;
                isGrounded = false;
                isGroundedTimeStamp = 0;
            }
        }

        private void Move()
        {
            Position += velocity;
            Pivot = new Vector2(spriteRect.Width / 2, spriteRect.Height) + Position;
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (!other.IsTrigger)
            {
                if (!(collider.Bottom < other.Top) && lastPosition.Y + collider.Height <= other.Top)
                {
                    isGrounded = true;
                    timer = 0;
                    if (Position.Y != other.GameObject.Position.Y - spriteRect.Height)
                        Position.Y = other.GameObject.Position.Y - spriteRect.Height;
                    velocity.Y = 0f;
                }
                else if (!(collider.Right < other.Left) || !(collider.Left > other.Right))
                {
                    velocity.X = 0f;
                    Position = lastPosition;
                }
            }
        }

        private void OnCollisionStay(BoxCollider other)
        {
            if (!(collider.Bottom <= other.Top))
            {
                if (isGrounded != true)
                    isGrounded = true;
                if (Position.Y != other.GameObject.Position.Y - spriteRect.Height)
                    Position.Y = other.GameObject.Position.Y - spriteRect.Height;
                if (velocity.Y != 0f)
                    velocity.Y = 0f;
            }
        }

        private void OnCollisionExit(BoxCollider other)
        {
            if (!other.IsTrigger)
            {
                if ((collider.Bottom < other.Top) || (!(collider.Right < other.Left) || !(collider.Left > other.Right)))
                {
                    isGrounded = false;
                }
            }
        }
    }
}
