using Microsoft.Xna.Framework;
using System;

namespace MonoGamePortal3Practise
{
    class WeightedCompanionCube : SideScrollEntity
    {
        public delegate void CubeEventHandler();
        public event CubeEventHandler OnToggleHoldState;

        private Vector2 lastPosition;

        private Movement movement;

        private SideScrollPlayer player;

        private bool isGrounded;

        private float gap = 20f;

        public float DistanceToPlayer
        {
            get { return gap; }
            private set { gap = value; }
        }

        public WeightedCompanionCube(int x, int y)
        {
            Name = "Cube";
            Tag = "Cube";
            Position = new Vector2(x, y);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            movement = new Movement(this);

            Collider = new BoxCollider(this, SpriteRect.Width, SpriteRect.Height, false);
            Collider.OnCollisionEnter += OnCollisionEnter;
            Collider.OnCollisionStay += OnCollisionStay;
            Collider.OnCollisionExit += OnCollisionExit;
        }

        public override void Update(GameTime gameTime)
        {
            lastPosition = Position;
            movement.UpdatePosition();

            if (isGrounded)
                movement.SetIsGroundedTimeStamp(gameTime);
            else if (player == null)
                movement.ApplyGravity(gameTime);

            base.Update(gameTime);
        }

        public void Move(SideDirections direction)
        {
            if (player != null)
            {
                if (direction == SideDirections.Right)
                    Center = new Vector2(player.Center.X + player.SpriteRect.Width / 2 + SpriteRect.Width / 2 + gap, player.Center.Y);
                if (direction == SideDirections.Left)
                    Center = new Vector2(player.Center.X - player.SpriteRect.Width / 2 - SpriteRect.Width / 2 - gap, player.Center.Y);

                movement.ViewDirection = direction;
            }
        }

        public void ToggleHoldState(SideScrollPlayer sideScrollPlayer)
        {
            if (player != null)
            {
                player = null;
                isGrounded = false;
            }
            else if (sideScrollPlayer != null)
                player = sideScrollPlayer;

            if (OnToggleHoldState != null)
                OnToggleHoldState();
        }

        public override void Destroy()
        {
            Collider.OnCollisionEnter -= OnCollisionEnter;
            Collider.OnCollisionStay -= OnCollisionStay;
            Collider.OnCollisionExit -= OnCollisionExit;

            base.Destroy();
        }

        public override void Move(Vector2 direction)
        {
            throw new Exception("Use other Move pls.");
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            Console.WriteLine("cube hit " + other.GameObject.Name);

            if (!other.IsTrigger)
            {
                //colliding from above
                if (!(Collider.Bottom < other.Top) && lastPosition.Y + Collider.Height <= other.Top)
                {
                    isGrounded = true;
                    movement.AccelerationMultipier = 0;
                    if (Position.Y != other.GameObject.Position.Y - SpriteRect.Height)
                        Position.Y = other.GameObject.Position.Y - SpriteRect.Height;
                    movement.ResetVelocityY();
                }

                #region
                //// colliding from beneigh
                //else if (!(Collider.Top > other.Bottom) && lastPosition.Y >= other.Bottom)
                //{
                //}
                ////colliding from right or left
                //else /*if (!(Collider.Right < other.Left && Collider.Left < other.Right) || !(Collider.Left > other.Right && Collider.Right > other.Left))*/
                //{
                //    if (!(other.GameObject is SideScrollPlayer))
                //        return;
                //    else
                //        movement.Move(((SideScrollPlayer)other.GameObject).ViewDirection);
                //}
                #endregion
            }

            if (other.GameObject is Portal)
            {
                if (player != null)
                    ToggleHoldState(player);
                Teleport(other, movement);
            }
        }

        private void OnCollisionStay(BoxCollider other)
        {
            #region
            ////colliding from above
            //if (!(Collider.Bottom < other.Top) && lastPosition.Y + Collider.Height <= other.Top)
            //{
            //}
            //// colliding from beneigh
            //else if (!(Collider.Top > other.Bottom) && lastPosition.Y >= other.Bottom)
            //{
            //}
            ////colliding from left or right
            //else
            //{
            //    if (!(other.GameObject is SideScrollPlayer) && !(other.GameObject is WeightedCompanionCube) && other.GameObject.Tag != "Ground")
            //        movement.ResetVelocityX();
            //    else if (other.GameObject is SideScrollPlayer)
            //        movement.Move(((SideScrollPlayer)other.GameObject).ViewDirection);
            //}
            #endregion
            if (!other.IsTrigger)
                if (!(Collider.Bottom <= other.Top) && lastPosition.Y < other.Bottom)
                {
                    if (isGrounded != true)
                        isGrounded = true;
                    if (Position.Y != other.GameObject.Position.Y - Collider.Height)
                        Position.Y = other.GameObject.Position.Y - Collider.Height;
                    if (movement.Velocity.Y != 0f)
                        movement.ResetVelocityY();
                }

            if (other.GameObject is Portal)
            {
                float temp = movement.MoveForce;
                movement.MoveForce = 300;
                Teleport(other, movement);
                movement.MoveForce = temp;
            }
        }

        private void OnCollisionExit(BoxCollider other)
        {
            if (other.GameObject.Tag == "Ground")
                isGrounded = false;
        }
    }
}
