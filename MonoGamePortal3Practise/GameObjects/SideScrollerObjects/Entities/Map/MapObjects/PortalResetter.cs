using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    class PortalResetter : SideScrollEntity
    {
        private int width;
        private int height;

        public PortalResetter(int x, int y, int width, int height)
        {
            Name = "PortalResetter";
            Tag = "Trigger";
            Position = new Vector2(x, y);
            this.width = width;
            this.height = height;
        }

        public override void LoadContent()
        {
            Collider = new BoxCollider(this, width, height, true);
            Collider.OnCollisionEnter += Collider_OnCollisionEnter;

            base.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // dont draw this until there is a Texture for it
        }

        void Collider_OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Player")
                SceneManager.CurrentScene.ResetPortals();
        }
    }
}
