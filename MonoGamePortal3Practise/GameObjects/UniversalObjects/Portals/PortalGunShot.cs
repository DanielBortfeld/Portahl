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

        public PortalGunShot(Vector2 position, ref Portal shotPortal)
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
                Console.WriteLine("shot loaded");
            }
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            Console.WriteLine("shot hit " + other.GameObject.Name);
            if (!other.IsTrigger && !(other.GameObject is SideScrollPlayer))
            {
                Console.WriteLine("shot v = 0");
                Velocity = Vector2.Zero;
                Destroy();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // dont try to draw this
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > lifeTime)
                return;
            else if(Portal != null)
            {
                Move(Velocity);
                Console.WriteLine("shot moves");
            }
        }

        public override void Destroy()
        {
            if (Collider != null)
                Collider.OnCollisionEnter -= OnCollisionEnter;

            base.Destroy();
            Console.WriteLine("shot destroyed");
        }
    }
}
