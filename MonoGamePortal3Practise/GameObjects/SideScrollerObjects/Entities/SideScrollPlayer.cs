using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    class SideScrollPlayer : SideScrollEntity
    {
        private bool isGrounded;

        Vector2 velocity;

        private int moveForce = 10;
        private int jumpForce = 25;
        private float gravityForce = 1f;
        private float timer;

        public SideScrollPlayer(Vector2 position)
        {
            Name = "Chell";
            StandartPosition = position;
            Position = position;
            collider = new BoxCollider(this, SceneManager.CurrentScene.SpriteSheet.Width, SceneManager.CurrentScene.SpriteSheet.Height);
            collider.OnCollisionEnter += OnCollisionEnter;
            collider.OnCollisionStay += OnCollisionStay;
            collider.OnCollisionExit += OnCollisionExit;
        }

        public override void Update(GameTime gameTime)
        {
            Position += velocity;

            ProcessInput();

            if (isGrounded)
            {
                velocity.Y = 0f;
                timer = 0;
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

            if (keyState.IsKeyDown(Keys.Space) && isGrounded)
            {
                velocity.Y = -jumpForce;
                isGrounded = false;
            }

        }

        private void Move(Vector2 velocity)
        {
        }

        private void Jump()
        {
            if (isGrounded)
                Position.Y = 1080 - SceneManager.CurrentScene.SpriteSheet.Height - 1;
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject is Floor)
                isGrounded = true;
            if (other.GameObject is Wall) ;
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
