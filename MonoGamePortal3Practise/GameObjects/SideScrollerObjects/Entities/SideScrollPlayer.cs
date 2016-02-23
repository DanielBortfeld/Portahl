using Microsoft.Xna.Framework;
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
        bool canMoveRight = true;
        bool canMoveLeft = true;

        private bool isGrounded;
        private float isGroundedTimeStamp;
        private float jumpCooldown = 0.1f;

        private WeightedCompanionCube cubeInReach;
        private WeightedCompanionCube heldCube;
        private int colliderExtension;

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

            //ProcessInput();

            if (isGrounded)
                isGroundedTimeStamp += (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                accelerationMultipier += (float)gameTime.ElapsedGameTime.TotalSeconds;
                velocity.Y += gravityForce * accelerationMultipier;
            }

            base.Update(gameTime);
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

        public override void Destroy()
        {
            InputManager.OnKeyPressed -= OnKeyPressed;
            InputManager.OnKeyDown -= OnKeyDown;
            InputManager.OnKeyUp -= OnKeyUp;

            Collider.OnCollisionEnter -= OnCollisionEnter;
            Collider.OnCollisionStay -= OnCollisionStay;
            Collider.OnCollisionExit -= OnCollisionExit;

            heldCube.OnToggleHoldState -= ToggleHoldState;
            cubeInReach.OnToggleHoldState -= ToggleHoldState;

            base.Destroy();
        }

        private void Move()
        {
            Position += velocity + (2 * correction);
            CameraTranslationPivot = new Vector2(SpriteRect.Width / 2, SpriteRect.Height) + Position;

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
            }
            else if (viewDirection == SideDirections.Right)
            {
                shot.Velocity.X = 50;
                shot.ViewDirection = viewDirection;
            }
        }

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

            SetCollider();
        }

        private void SetCollider()
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
            //switch (eventArgs.Key)
            //{
            //    case Keys.D:
            //    case Keys.A:
            //        velocity.X = 0;
            //        break;
            //    default:
            //        break;
            //}
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
                        if (!canMoveLeft)
                            canMoveLeft = true;
                    }
                    break;
                case Keys.A:
                    if (canMoveLeft)
                    {
                        velocity.X = -moveForce;
                        viewDirection = SideDirections.Left;
                        if (!canMoveRight)
                            canMoveRight = true;
                    }
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
                else
                #region
                /*if (!(Collider.Right < other.Left) || !(Collider.Left > other.Right))*/
                #endregion
                {
                    //velocity.X = 0f;
                    //Position.X = lastPosition.X;
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
                    if (viewDirection == SideDirections.Right)
                        Collider.X = (int)Position.X;
                    else if (viewDirection == SideDirections.Left)
                        Collider.X = (int)Position.X - colliderExtension;
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
                if (!(Collider.Bottom <= other.Top) && lastPosition.Y < other.Bottom)
                {
                    if (isGrounded != true)
                        isGrounded = true;
                    if (Position.Y != other.GameObject.Position.Y - Collider.Height)
                        Position.Y = other.GameObject.Position.Y - Collider.Height;
                    if (velocity.Y != 0f)
                        velocity.Y = 0f;
                }
        }

        private void OnCollisionExit(BoxCollider other)
        {
            //http://go.colorize.net/xna/2d_collision_response_xna/index.html
            Console.WriteLine("chell left " + other.GameObject.Name);

            if (LeftAndRightCollisions.Contains(other))
            {
                if (correction.X != 0)
                    correction.X = 0f;
                LeftAndRightCollisions.Remove(other);
            }

            if (!other.IsTrigger)
                if ((Collider.Bottom < other.Top) || (!(Collider.Right < other.Left) || !(Collider.Left > other.Right)))
                    isGrounded = false;

            if (other.GameObject is WeightedCompanionCube)
            {
                if (heldCube == null)
                    cubeInReach.OnToggleHoldState -= ToggleHoldState;
                cubeInReach = null;
            }
        }

        #region Old ProcessInput
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
        #endregion
    }
}
