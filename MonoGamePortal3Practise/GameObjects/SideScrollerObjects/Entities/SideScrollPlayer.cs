﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGamePortal3Practise
{
    class SideScrollPlayer : SideScrollEntity
    {
        public Vector2 Pivot;
        public Vector2 lastPosition;

        private Vector2 velocity;
        private int moveForce = 10;
        private int jumpForce = 15;

        private float gravityForce = 2f;
        private float timer;

        private bool isGrounded;
        private float isGroundedTimeStamp;
        private float jumpCooldown = 0.1f;

        private SideDirections viewDirection = SideDirections.None;

        public SideDirections ViewDirection { get { return viewDirection; } }

        public SideScrollPlayer(Vector2 position)
        {
            Name = "Chell";
            Tag = "Player";

            StandartPosition = position;
            Position = position;
        }

        public override void LoadContent()
        {
            InputManager.OnKeyPressed += OnKeyPressed;
            InputManager.OnKeyDown += OnKeyDown;
            InputManager.OnKeyUp += OnKeyUp;

            SceneManager.CurrentScene.ResetPortals();

            Collider = new BoxCollider(this, SpriteRect.Width, SpriteRect.Height, false);
            Collider.OnCollisionEnter += OnCollisionEnter;
            Collider.OnCollisionStay += OnCollisionStay;
            Collider.OnCollisionExit += OnCollisionExit;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            lastPosition = Position;
            Move();

            //ProcessInput();

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

        public override void Destroy()
        {
            InputManager.OnKeyPressed -= OnKeyPressed;
            InputManager.OnKeyDown -= OnKeyDown;
            InputManager.OnKeyUp -= OnKeyUp;

            Collider.OnCollisionEnter -= OnCollisionEnter;
            Collider.OnCollisionStay -= OnCollisionStay;
            Collider.OnCollisionExit -= OnCollisionExit;

            base.Destroy();
        }

        private void OnKeyUp(InputEventArgs eventArgs)
        {
            switch (eventArgs.Key)
            {
                case Keys.D:
                case Keys.A:
                    velocity.X = 0;
                    break;
                default:
                    break;
            }
        }

        private void OnKeyDown(InputEventArgs eventArgs)
        {
            switch (eventArgs.Key)
            {
                case Keys.D:
                    velocity.X = moveForce;
                    viewDirection = SideDirections.Right;
                    break;
                case Keys.A:
                    velocity.X = -moveForce;
                    viewDirection = SideDirections.Left;
                    break;
                default:
                    break;
            }

            if (isGrounded && eventArgs.Key == (Keys.Space) && isGroundedTimeStamp > jumpCooldown)
            {
                velocity.Y = -jumpForce;
                isGrounded = false;
                isGroundedTimeStamp = 0;
            }
        }

        private void OnKeyPressed(InputEventArgs eventArgs)
        {
            switch (eventArgs.MouseButton)
            {
                case MouseButtons.LeftButton:
                    Shoot(SceneManager.CurrentScene.PortalBlue);
                    break;
                case MouseButtons.RightButton:
                    Shoot(SceneManager.CurrentScene.PortalOrange);
                    break;
                case MouseButtons.MiddleButton:
                    break;
                default:
                    break;
            }
        }

        private void Move()
        {
            Position += velocity;
            Pivot = new Vector2(SpriteRect.Width / 2, SpriteRect.Height) + Position;
        }

        private void Shoot(Portal portal)
        {
            PortalGunShot shot = new PortalGunShot(Position + new Vector2(SpriteRect.Width / 2, SpriteRect.Height / 2), portal);

            if (viewDirection == SideDirections.Left)
            {
                shot.Velocity.X = -50;
                shot.ViewDirection = viewDirection;
            }
            else if (viewDirection == SideDirections.Right)
            {
                shot.Velocity.X = 50;
                shot.ViewDirection = viewDirection;
            }
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            Console.WriteLine("chell hit " + other.GameObject.Name);

            if (!other.IsTrigger)
            {
                if (!(Collider.Bottom < other.Top) && lastPosition.Y + Collider.Height <= other.Top)
                {
                    isGrounded = true;
                    timer = 0;
                    if (Position.Y != other.GameObject.Position.Y - SpriteRect.Height)
                        Position.Y = other.GameObject.Position.Y - SpriteRect.Height;
                    velocity.Y = 0f;
                }
                // colliding from beneigh
                else if (!(Collider.Top >= other.Bottom) && lastPosition.Y > other.Bottom)
                {
                    velocity.Y = 0f;
                    Position.Y = other.Bottom + 1;
                }
                else /*if (!(Collider.Right < other.Left) || !(Collider.Left > other.Right))*/
                {
                    velocity.X = 0f;
                    Position = lastPosition;
                }
            }

            if (other.GameObject is Portal)
                Teleport(other, viewDirection, velocity);
        }

        private void OnCollisionStay(BoxCollider other)
        {
            if (!other.IsTrigger)
                if (!(Collider.Bottom <= other.Top) && lastPosition.Y < other.Bottom)
                {
                    if (isGrounded != true)
                        isGrounded = true;
                    if (Position.Y != other.GameObject.Position.Y - SpriteRect.Height)
                        Position.Y = other.GameObject.Position.Y - SpriteRect.Height;
                    if (velocity.Y != 0f)
                        velocity.Y = 0f;
                }
        }

        private void OnCollisionExit(BoxCollider other)
        {
            if (!other.IsTrigger)
                if ((Collider.Bottom < other.Top) || (!(Collider.Right < other.Left) || !(Collider.Left > other.Right)))
                    isGrounded = false;
        }

        //private MouseState previousState;

        //public void ProcessInput()
        //{
        //    KeyboardState keyState = Keyboard.GetState();
        //    MouseState mouseState = Mouse.GetState();

        //    if (keyState.IsKeyDown(Keys.D))
        //        velocity.X = moveForce;
        //    else if (keyState.IsKeyDown(Keys.A))
        //        velocity.X = -moveForce;
        //    else
        //        velocity.X = 0;

        //    if (mouseState.LeftButton == ButtonState.Pressed && previousState.LeftButton != ButtonState.Pressed)
        //        Shoot(SceneManager.CurrentScene.PortalBlue);
        //    else if (mouseState.RightButton == ButtonState.Pressed && previousState.RightButton != ButtonState.Pressed)
        //        Shoot(SceneManager.CurrentScene.PortalOrange);

        //    previousState = mouseState;

        //    if (isGrounded && keyState.IsKeyDown(Keys.Space) && isGroundedTimeStamp > jumpCooldown)
        //    {
        //        velocity.Y = -jumpForce;
        //        isGrounded = false;
        //        isGroundedTimeStamp = 0;
        //    }
        //}
    }
}
