using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    public class SideScrollEntity : Entity
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SceneManager.CurrentScene.SpriteSheet, Position, GetSpriteRect(), White);
        }

        public override void Teleport()
        {
        }
    }
}
