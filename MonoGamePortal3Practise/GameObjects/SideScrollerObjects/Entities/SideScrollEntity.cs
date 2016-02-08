using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    public class SideScrollEntity : Entity
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SceneManager.CurrentScene.SpriteSheet, Position, GetSpriteRect(), White);
        }

        public virtual void Teleport(Vector2 targetPosition)
        {
            Position = targetPosition;
        }
    }
}
