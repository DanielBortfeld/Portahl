using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    class SideScrollPlayer : SideScrollEntity
    {
        private bool isGrounded;

        private float timer;
        private int speed = 10;

        public SideScrollPlayer(Vector2 position)
        {
            Name = "Chell";
            StandartPosition = position;
            Position = position;
            collider = new BoxCollider(this, SceneManager.CurrentScene.SpriteSheet.Width, SceneManager.CurrentScene.SpriteSheet.Height);
            collider.OnCollisionEnter += OnCollisionEnter;
            collider.OnCollisionStay += OnCollisionEnter;
        }

        private void OnCollisionStay(BoxCollider other)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            ProcessInput();

            if (!isGrounded)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position.Y += speed * (timer / 2);
            }
            else
                timer = 0;

            base.Update(gameTime);
        }

        public void ProcessInput()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.D))
                Run(speed);

            if (keyState.IsKeyDown(Keys.A))
                Run(-speed);
        }

        private void Run(int speedX)
        {
            Position.X += speedX;
        }

        private void Jump()
        {
            if (isGrounded)
            {
                //jump
            }

        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject is Floor)
                isGrounded = true;
            if (other.GameObject is Wall) ;
        }
    }
}
