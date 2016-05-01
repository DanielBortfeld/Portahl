// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    class PortalGunShot : Entity
    {
        public Vector2 Velocity = Vector2.Zero;
        public SideDirections ViewDirection;

        private float timer;
        private float lifeTime = 3f;

        public Portal Portal { get; private set; }

        public PortalGunShot(Vector2 position)
        {
            Name = "PortalGunShot";
            Tag = "Projectile";

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

        public override void Destroy()
        {
            Velocity = Vector2.Zero;

            if (Collider != null)
                Collider.OnCollisionEnter -= OnCollisionEnter;

            base.Destroy();
            Console.WriteLine("shot destroyed");
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
                    // colliding from left
                    if (!(Collider.Right < other.Left) && ViewDirection == SideDirections.Right)
                    {
                        Portal.Position = new Vector2(other.Left, Position.Y - (Portal.SpriteRect.Height / 2));
                        Portal.ViewDirection = SideDirections.Left;
                    }
                    //colliding from right
                    else if (!(Collider.Left > other.Right) && ViewDirection == SideDirections.Left)
                    {
                        Portal.Position = new Vector2(other.Right - Portal.SpriteRect.Width, Position.Y - (Portal.SpriteRect.Height / 2));
                        Portal.ViewDirection = SideDirections.Right;
                    }

                    //Console.WriteLine("portal position is " + Portal.Position);
                    //Console.WriteLine("other portal position is " + SceneManager.GetDestinationPortal(Portal).Position);
                }

                Destroy();
            }
        }
    }
}
