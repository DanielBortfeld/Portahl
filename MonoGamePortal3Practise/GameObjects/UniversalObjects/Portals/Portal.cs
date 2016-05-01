// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    public abstract class Portal : Entity
    {
        public SideDirections ViewDirection = SideDirections.None;

        private float colliderExtension = 1.75f;

        public override void LoadContent()
        {
            Tag = "Portal";
            Collider = new BoxCollider(this, (int)(SpriteRect.Width * colliderExtension), (int)(SpriteRect.Height * colliderExtension), true);
            Collider.OnCollisionEnter += OnCollisionEnter;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine("portal position is " + Position);
            if (Position != Vector2.Zero)
            {
                //Console.WriteLine("Position is != 0");
                if (SceneManager.CurrentScene is SceneSideScroller)
                {
                    spriteBatch.Draw(SceneManager.CurrentScene.SpriteSheet, Position, SpriteRect, White);
                    //Console.WriteLine("draw lvl 2");
                }
                else
                {
                    spriteBatch.Draw(SpriteSheet, Position * SpriteRect.Width, SpriteRect, White);
                    //Console.WriteLine("draw lvl 1");
                }
            }
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject is PortalGunShot)
                other.GameObject.Destroy();
        }
    }
}
