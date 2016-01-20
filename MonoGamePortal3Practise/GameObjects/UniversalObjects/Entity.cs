using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    class Entity : GameObject
    {
        protected static PortalBlue portalBlue = new PortalBlue(Vector2.Zero);
        protected static PortalOrange portalOrange = new PortalOrange(Vector2.Zero);

        public Vector2 StandartPosition;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SceneManager.CurrentScene.SpriteSheet, Position, SceneManager.CurrentScene.GetSpriteRect(Name), Color.White);
        }

        protected void ResetPortals()
        {
            SceneManager.CurrentScene.RemoveGameObject(SceneManager.CurrentScene.FindGameObject("PortalOrange"));
            SceneManager.CurrentScene.RemoveGameObject(SceneManager.CurrentScene.FindGameObject("PortalBlue"));
            portalBlue = new PortalBlue(Vector2.Zero);
            portalOrange = new PortalOrange(Vector2.Zero);
        }
    }
}
