// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    class SideScrollPlayer : SideScrollEntity
    {
        private Vector2 lastPosition;

        private Vector2 velocity;
        private int moveForce = 10;
        private int jumpForce = 15;
        private float gravityForce = 2f;
        private float accelerationMultipier;

        private Vector2 correction;
        private List<BoxCollider> LeftAndRightCollisions = new List<BoxCollider>();
        private bool canMoveRight = true;
        private bool canMoveLeft = true;

        private bool isJumping;
        private bool isGrounded;
        private float isGroundedTimer;
        private float jumpCooldown = 0.1f;

        private bool hasShot;
        private float shootTimer;
        private float shootCooldown = 0.15f;

        private WeightedCompanionCube cubeInReach;
        private WeightedCompanionCube heldCube;
        private int colliderExtension;

        private SpriteAnimation spriteAnimation;
        private States state = States.Idle;

        private SideDirections viewDirection = SideDirections.None;

        public SideDirections ViewDirection { get { return viewDirection; } }
        public Vector2 CameraTranslationPivot { get; private set; }

        public SideScrollPlayer(Vector2 position)
        {
            Name = "Chell";
            Tag = "Player";

            StandartPosition = position;
            Position = position;
        }

        public override void LoadContent()
        {
            spriteAnimation = new SpriteAnimation(Name, GameManager.LoadTexture2D("ChellAnimationSheet"), GameManager.Content.RootDirectory + "/ChellAnimationSheet.xml", string.Format("{0}_{1}_{2}", Name, state, viewDirection));

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

            UpdateColliderPosition();

            ShootingCooldown(gameTime);
            if (hasShot)
                state = States.Shoot;

            if (isGrounded)
                isGroundedTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                accelerationMultipier += (float)gameTime.ElapsedGameTime.TotalSeconds;
                velocity.Y += gravityForce * accelerationMultipier;
                state = States.Jump;
            }

            if (spriteAnimation != null)
                spriteAnimation.Update(gameTime, string.Format("{0}_{1}_{2}", Name, state, viewDirection));

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (spriteAnimation != null)
                spriteAnimation.Draw(spriteBatch, Position, White);
            else
                base.Draw(spriteBatch);
        }

        public override void Destroy()
        {
            InputManager.OnKeyPressed -= OnKeyPressed;
            InputManager.OnKeyDown -= OnKeyDown;
            InputManager.OnKeyUp -= OnKeyUp;

            if (Collider != null)
            {
                Collider.OnCollisionEnter -= OnCollisionEnter;
                Collider.OnCollisionStay -= OnCollisionStay;
                Collider.OnCollisionExit -= OnCollisionExit;
            }

            if (heldCube != null)
                heldCube.OnToggleHoldState -= ToggleHoldState;
            if (cubeInReach != null)
                cubeInReach.OnToggleHoldState -= ToggleHoldState;

            base.Destroy();
        }

        private void UpdateColliderPosition()
        {
            if (!Collider.AutoPositionUpdateIsEnabled)
            {
                if (viewDirection == SideDirections.Right)
                    Collider.X = (int)Position.X;
                else if (viewDirection == SideDirections.Left)
                    Collider.X = (int)Position.X - colliderExtension;

                Collider.Y = (int)Position.Y;
            }
        }

        private void Move()
        {
            Position += velocity;
            CameraTranslationPivot = new Vector2(SpriteRect.Width / 2, SpriteRect.Height) + Position;

            if (velocity.X == 0)
                state = States.Idle;
            else
                velocity.X = 0;

            if (heldCube != null)
                heldCube.Move(viewDirection);
        }

        private void Shoot(Portal portal)
        {
            PortalGunShot shot = new PortalGunShot(Position + new Vector2(SpriteRect.Width / 2, SpriteRect.Height / 2), portal);

            if (viewDirection == SideDirections.Left)
            {
                shot.Velocity.X = -50;
                shot.ViewDirection = viewDirection;
                hasShot = true;
            }
            else if (viewDirection == SideDirections.Right)
            {
                shot.Velocity.X = 50;
                shot.ViewDirection = viewDirection;
                hasShot = true;
            }
        }

        private void ShootingCooldown(GameTime gameTime)
        {
            if (hasShot)
                shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shootTimer >= shootCooldown)
            {
                hasShot = false;
                shootTimer = 0f;
            }
        }

        /// <summary>
        /// If you collide with a cube, you
        /// can use this method to pick it up.
        /// If you picked up a cube, you can also put it
        /// down again with this method.
        /// </summary>
        private void ToggleHoldState()
        {
            if (heldCube != null)
            {
                heldCube.OnToggleHoldState -= ToggleHoldState;
                heldCube.Collider.IsActive = true;
                heldCube = null;
            }
            else if (cubeInReach != null)
            {
                heldCube = cubeInReach;

                cubeInReach.OnToggleHoldState -= ToggleHoldState;
                heldCube.OnToggleHoldState += ToggleHoldState;
            }

            AdjustCollider();
        }

        /// <summary>
        /// If the player picks up a cube, the collider of the cube
        /// gets extendet to the size of playerCollider.X + cubeCollider.X .
        /// If you put down the cube, the playerCollider becomes
        /// adjusted to its original size.
        /// </summary>
        private void AdjustCollider()
        {
            if (heldCube != null)
            {
                heldCube.Collider.IsActive = false;

                colliderExtension = heldCube.Collider.Width + (int)heldCube.DistanceToPlayer;
                Collider.Width += colliderExtension;
                Collider.AutoPositionUpdateIsEnabled = false;
            }
            else
            {
                Collider.Width = SpriteRect.Width;
                Collider.AutoPositionUpdateIsEnabled = true;
            }
        }

        private void OnKeyUp(InputEventArgs eventArgs)
        {
            switch (eventArgs.Key)
            {
                case Keys.D:
                    if (!canMoveLeft)
                        canMoveLeft = true;
                    break;
                case Keys.A:
                    if (!canMoveRight)
                        canMoveRight = true;
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
                    if (canMoveRight)
                    {
                        velocity.X = moveForce;
                        viewDirection = SideDirections.Right;
                        state = States.Walk;
                        canMoveLeft = false;
                    }
                    break;
                case Keys.A:
                    if (canMoveLeft)
                    {
                        velocity.X = -moveForce;
                        viewDirection = SideDirections.Left;
                        state = States.Walk;
                        canMoveRight = false;
                    }
                    break;
                default:
                    state = States.Idle;
                    break;
            }

            if (isGrounded && eventArgs.Key == (Keys.Space) && isGroundedTimer > jumpCooldown)
            {
                velocity.Y = -jumpForce;
                isJumping = true;
                isGrounded = false;
                isGroundedTimer = 0;

                state = States.Jump;
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
                    if (heldCube != null)
                        heldCube.ToggleHoldState(this);
                    else if (cubeInReach != null)
                        cubeInReach.ToggleHoldState(this);
                    break;
                default:
                    break;
            }
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            Console.WriteLine("chell hit " + other.GameObject.Name);

            if (!other.IsTrigger)
            {
                //colliding from above
                if (!(Collider.Bottom < other.Top) && lastPosition.Y + Collider.Height <= other.Top)
                {
                    isGrounded = true;
                    isJumping = false;
                    state = States.Idle;
                    accelerationMultipier = 0;
                    if (Position.Y != other.GameObject.Position.Y - Collider.Height)
                        Position.Y = other.GameObject.Position.Y - Collider.Height;
                    velocity.Y = 0f;
                }
                // colliding from beneigh
                else if (!(Collider.Top >= other.Bottom) && lastPosition.Y > other.Bottom)
                {
                    velocity.Y = 0f;
                    Position.Y = other.Bottom + 1;
                }
                // colliding from left or right
                else if (Collider.CollidesWithLeftOf(other) || Collider.CollidesWithRightOf(other))
                #region
                /*if (!(Collider.Right < other.Left) || !(Collider.Left > other.Right))*/
                #endregion
                {
                    velocity.X = 0f;
                    correction.X = lastPosition.X - Position.X;
                    if (correction.X < 0)
                        canMoveRight = false;
                    else if (correction.X > 0)
                        canMoveLeft = false;
                    LeftAndRightCollisions.Add(other);
                }
            }

            if (other.GameObject is Portal)
            {
                Portal destinationportal = Teleport(other, viewDirection, velocity);
                if (destinationportal != null)
                {
                    viewDirection = destinationportal.ViewDirection;
                    if (!Collider.AutoPositionUpdateIsEnabled)
                    {
                        if (viewDirection == SideDirections.Right)
                            Collider.X = (int)Position.X;
                        else if (viewDirection == SideDirections.Left)
                            Collider.X = (int)Position.X - colliderExtension;
                    }
                }
            }

            if (other.GameObject is WeightedCompanionCube)
            {
                cubeInReach = (WeightedCompanionCube)other.GameObject;
                cubeInReach.OnToggleHoldState += ToggleHoldState;
            }
        }

        private void OnCollisionStay(BoxCollider other)
        {
            if (!other.IsTrigger)
            {
                if (!(Collider.Bottom < other.Top) && (lastPosition.Y + Collider.Height <= other.Top) && isJumping == false)
                {
                    if (isGrounded != true)
                        isGrounded = true;
                    if (Position.Y > other.GameObject.Position.Y - Collider.Height)
                        Position.Y = other.GameObject.Position.Y - Collider.Height;
                    if (velocity.Y != 0f)
                        velocity.Y = 0f;
                }
                if (Position.Y > other.GameObject.Position.Y - Collider.Height)
                    if (other.Contains(Collider) || Collider.CollidesWithTopOf(other))
                        Position.Y = other.GameObject.Position.Y - Collider.Height;

                if (Collider.Bottom > other.Top && Collider.CollidesWithTopOf(other))
                {
                    //falling
                    if (velocity.Y > 0f)
                        if (Position.Y != other.GameObject.Position.Y - Collider.Height)
                        {
                            velocity.Y = -velocity.Y;
                        }
                }
                if (Collider.Top < other.Bottom && Collider.CollidesWithBottomOf(other))
                {
                    //rising
                    if (Position.Y != other.GameObject.Position.Y - Collider.Height)
                        Position.Y = other.GameObject.Position.Y - Collider.Height;
                }
            }
        }

        private void OnCollisionExit(BoxCollider other)
        {
            Console.WriteLine("chell left " + other.GameObject.Name);

            if (LeftAndRightCollisions.Contains(other))
            {
                if (correction.X != 0)
                    correction.X = 0f;
                LeftAndRightCollisions.Remove(other);
                if (LeftAndRightCollisions.Count == 0)
                {
                    canMoveLeft = true;
                    canMoveRight = true;
                }
            }

            if (!other.IsTrigger)
                if ((Collider.Bottom < other.Top) || (!(Collider.Right < other.Left) || !(Collider.Left > other.Right)))
                    isGrounded = false;

            if (other.GameObject is WeightedCompanionCube)
            {
                if (heldCube == null && cubeInReach != null)
                    cubeInReach.OnToggleHoldState -= ToggleHoldState;
                cubeInReach = null;
            }
        }
    }
}
