using Microsoft.Xna.Framework;

namespace MonoGamePortal3Practise
{
    public class SideScrollEntity : Entity
    {
        protected bool hasTeleported;
        private float teleportCooldown = 0.25f;
        private float teleportTimeStamp;

        public override void Update(GameTime gameTime)
        {
            if (hasTeleported)
                teleportTimeStamp += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (teleportTimeStamp > teleportCooldown)
            {
                hasTeleported = false;
                teleportTimeStamp = 0;
            }
            base.Update(gameTime);
        }

        public Portal Teleport(BoxCollider portalCollider, SideDirections viewDirection, Vector2 velocity)
        {
            if (hasTeleported)
                return null;

            Portal destinationPortal = SceneManager.GetDestinationPortal((Portal)portalCollider.GameObject);

            if (destinationPortal.Position == Vector2.Zero)
                return null;

            float gap = 25f;

            // colliding from left
            if (!(Collider.Right < portalCollider.Left) && viewDirection == SideDirections.Right)
            {
                if (destinationPortal.ViewDirection == SideDirections.Right)
                    Position = new Vector2(destinationPortal.Collider.Right + velocity.X + gap, destinationPortal.Position.Y + destinationPortal.Collider.Height / 2 - Collider.Height / 2);
                else if (destinationPortal.ViewDirection == SideDirections.Left)
                {
                    Position = new Vector2(destinationPortal.Collider.Left - velocity.X - gap - Collider.Width, destinationPortal.Position.Y + destinationPortal.Collider.Height / 2 - Collider.Height / 2);
                    velocity = -velocity;
                }
                hasTeleported = true;
            }
            //colliding from right
            else if (!(Collider.Left > portalCollider.Right) && viewDirection == SideDirections.Left)
            {
                if (destinationPortal.ViewDirection == SideDirections.Right)
                {
                    Position = new Vector2(destinationPortal.Collider.Right - velocity.X + gap, destinationPortal.Position.Y + destinationPortal.Collider.Height / 2 - Collider.Height / 2);
                    velocity = -velocity;
                }
                else if (destinationPortal.ViewDirection == SideDirections.Left)
                    Position = new Vector2(destinationPortal.Collider.Left + velocity.X - gap - Collider.Width, destinationPortal.Position.Y + destinationPortal.Collider.Height / 2 - Collider.Height / 2);
                hasTeleported = true;
            }
            return destinationPortal;
        }

        public Portal Teleport(BoxCollider portalCollider, Movement movement)
        {
            return Teleport(portalCollider, movement.ViewDirection, movement.Velocity);
        }
    }
}