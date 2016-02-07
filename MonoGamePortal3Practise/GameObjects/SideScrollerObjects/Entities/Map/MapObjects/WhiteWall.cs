using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    class WhiteWall : SideScrollEntity
    {
        public WhiteWall(int x, int y)
        {
            Name = "WhiteWall";
            Position.X = x;
            Position.Y = y;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Collider = new BoxCollider(this, SpriteRect.Width, SpriteRect.Height, false);
            Collider.OnCollisionEnter += OnCollisionEnter;
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject is PortalGunShot)
            {
                Vector2 portalPosition = ((PortalGunShot)other.GameObject).Portal.Position;
                Vector2 otherPortalPosition = SceneManager.GetDestinationPortal(((PortalGunShot)other.GameObject).Portal).Position;

                if (otherPortalPosition != Position)
                {
                    portalPosition = Position;
                    Console.WriteLine("portal position is " + portalPosition);
                    Console.WriteLine("other portal position is " + otherPortalPosition);
                }
            }
        }

        public override void Destroy()
        {
            Collider.OnCollisionEnter -= OnCollisionEnter;

            base.Destroy();
        }
    }
}
