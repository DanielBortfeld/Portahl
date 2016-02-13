using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    public class SideScrollEntity : Entity
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, Position, SpriteRect, White);
        }

        public void Teleport(BoxCollider portalCollider, SideDirections viewDirection, Vector2 velocity)
        {
            Portal destinationPortal = SceneManager.GetDestinationPortal((Portal)portalCollider.GameObject);

            if (destinationPortal.Position == Vector2.Zero)
                return;

            float gap = 10f;

            // colliding from left
            if (!(Collider.Right < portalCollider.Left) && viewDirection == SideDirections.Right)
            {
                if (destinationPortal.ViewDirection == SideDirections.Right)
                    Position = new Vector2(destinationPortal.Collider.Right + velocity.X + gap, destinationPortal.Position.Y + destinationPortal.SpriteRect.Height / 2 - SpriteRect.Height / 2);
                else if (destinationPortal.ViewDirection == SideDirections.Left)
                {
                    Position = new Vector2(destinationPortal.Collider.Left - velocity.X + gap - SpriteRect.Width, destinationPortal.Position.Y + destinationPortal.SpriteRect.Height / 2 - SpriteRect.Height / 2);
                    velocity = -velocity;
                }
            }
            //colliding from right
            else if (!(Collider.Left > portalCollider.Right) && viewDirection == SideDirections.Left)
            {
                if (destinationPortal.ViewDirection == SideDirections.Right)
                {
                    Position = new Vector2(destinationPortal.Collider.Right - velocity.X + gap, destinationPortal.Position.Y + destinationPortal.SpriteRect.Height / 2 - SpriteRect.Height / 2);
                    velocity = -velocity;
                }
                else if (destinationPortal.ViewDirection == SideDirections.Left)
                    Position = new Vector2(destinationPortal.Collider.Left + velocity.X + gap - SpriteRect.Width, destinationPortal.Position.Y + destinationPortal.SpriteRect.Height / 2 - SpriteRect.Height / 2);
            }
        }

        public void Teleport(BoxCollider portalCollider, Movement movement)
        {
            Teleport(portalCollider, movement.ViewDirection, movement.Velocity);
        }
    }
}
