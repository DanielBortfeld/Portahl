using Microsoft.Xna.Framework;

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

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject is Floor)
            {
                isGrounded = true;
                movement.AccelerationMultipier = 0;
                if (Position.Y != other.Y - SpriteRect.Height)
                    Position.Y = other.Y - SpriteRect.Height;
                movement.ResetVelocityY();
            }
            if (other.GameObject is Wall)
            {
                movement.ResetVelocityX();
                Position = lastPosition;
            }
            if (!other.IsTrigger)
            {
            }
        }

        private void OnCollisionStay(BoxCollider other)
        {
        }

        private void OnCollisionExit(BoxCollider other)
        {
            if (other.GameObject is Floor)
                isGrounded = false;
        }

        public override void Destroy()
        {
            Collider.OnCollisionEnter -= OnCollisionEnter;
            Collider.OnCollisionStay -= OnCollisionStay;
            Collider.OnCollisionExit -= OnCollisionExit;

            base.Destroy();
        }
    }
}
