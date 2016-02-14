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
            Collider = new BoxCollider(this, (int)(SpriteRect.Width * colliderExtension), (int)(SpriteRect.Height * colliderExtension), true);
            Collider.OnCollisionEnter += OnCollisionEnter;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine("portal position is " + Position);
            if (Position != Vector2.Zero)
            {
                //Console.WriteLine("Position is != 0");
<<<<<<< HEAD
                if (SceneManager.CurrentScene is SceneSideScroller)
=======
                if (SceneManager.CurrentScene is SceneLevelOneTD)
                {
                    spriteBatch.Draw(SpriteSheet, Position * SpriteRect.Width, SpriteRect, White);
                    //Console.WriteLine("draw lvl 1");
                }
                else if (SceneManager.CurrentScene is SceneSideScroller)
>>>>>>> 8bb0c244afa36d2bc646a220d65ddd1690d4801d
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
<<<<<<< HEAD
                other.GameObject.Destroy();
=======
            {
                other.GameObject.Destroy();
            }
>>>>>>> 8bb0c244afa36d2bc646a220d65ddd1690d4801d
        }
    }
}
