using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    class PortalGunShot : Entity
    {
        public Portal Portal { get; private set; }
        public Vector2 Velocity = Vector2.Zero;

        private float timer;
        private float lifeTime = 3f;

        public PortalGunShot(Vector2 position)
        {
            Name = "PortalGunShot";

            Position = position;
        }

        public PortalGunShot(Vector2 position, Portal shotPortal)
        {
            Name = "PortalGunShot";

            Position = position;
            Portal = shotPortal;
        }

        public override void LoadContent()
        {
            if (Portal != null)
            {
                Collider = new BoxCollider(this, 10, 10, true);
                Collider.OnCollisionEnter += OnCollisionEnter;
                //Console.WriteLine("shot loaded");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // don't draw this
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > lifeTime)
                Destroy();
            else if (Portal != null)
            {
                Move(Velocity);
                //Console.WriteLine("shot moves by " + Velocity);
            }
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            Console.WriteLine("shot hit " + other.GameObject.Name);

            if (other.GameObject == SceneManager.GetDestinationPortal(Portal))
            {
                Destroy();
                return;
            }

            if (!other.IsTrigger && !(other.GameObject is SideScrollPlayer))
            {
                if (other.GameObject is WhiteWall)
                {

                    //if (Velocity.X < 0)
                    //    Portal.Position = new Vector2(Position.X - Portal.SpriteRect.Width, Position.Y - (Portal.SpriteRect.Height / 2));
                    //else
                        Portal.Position = new Vector2(Position.X, Position.Y - (Portal.SpriteRect.Height / 2));

                    //Console.WriteLine("portal position is " + Portal.Position);
                    //Console.WriteLine("other portal position is " + SceneManager.GetDestinationPortal(Portal).Position);
                }

                Destroy();
            }
        }

        public override void Destroy()
        {
            Velocity = Vector2.Zero;

            if (Collider != null)
                Collider.OnCollisionEnter -= OnCollisionEnter;

            base.Destroy();
            Console.WriteLine("shot destroyed");
        }
    }
}
