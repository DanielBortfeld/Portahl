using Microsoft.Xna.Framework;
using System;

namespace MonoGamePortal3Practise
{
    class WeightedCompanionCube : SideScrollEntity
    {
        private Vector2 lastPosition;
        private Movement movement;

        private bool isGrounded;

        public WeightedCompanionCube(int x, int y)
        {
            Name = "Cube";
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
            else
                movement.ApplyGravity(gameTime);

            base.Update(gameTime);
        }

        public override void Destroy()
        {
            Collider.OnCollisionEnter -= OnCollisionEnter;
            Collider.OnCollisionStay -= OnCollisionStay;
            Collider.OnCollisionExit -= OnCollisionExit;

            base.Destroy();
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
                //colliding from beneigh
                else if (!(Collider.Top > other.Bottom) && lastPosition.Y >= other.Bottom)
                    return;
                //colliding from left or right
                else if (!(Collider.Right < other.Left) || !(Collider.Left > other.Right))
                {
                    if (other.GameObject is SideScrollPlayer && other.GameObject is WeightedCompanionCube)
                        movement.Move(((SideScrollPlayer)other.GameObject).ViewDirection);
                }
            }
        }

        private void OnCollisionStay(BoxCollider other)
        {
            //colliding from above
            if (!(Collider.Bottom < other.Top) && lastPosition.Y + Collider.Height <= other.Top)
                return;
            //colliding from beneigh
            else if (!(Collider.Top > other.Bottom) && lastPosition.Y >= other.Bottom)
                return;
            //colliding from left or right
            else if (!(Collider.Right < other.Left) || !(Collider.Left > other.Right))
            {
                if (!(other.GameObject is SideScrollPlayer) && !(other.GameObject is WeightedCompanionCube))
                    return;
            }
        }

        private void OnCollisionExit(BoxCollider other)
        {
            if (other.GameObject is Floor)
                isGrounded = false;
            if (other.GameObject is SideScrollPlayer)
                movement.ResetVelocityX();
        }
    }
}
