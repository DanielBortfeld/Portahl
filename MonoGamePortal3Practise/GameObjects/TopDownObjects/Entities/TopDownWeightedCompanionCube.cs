using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    class TopDownWeightedCompanionCube : TopDownEntity
    {
        public bool IsRespawnable;
        public int ID { get; private set; }

        public TopDownWeightedCompanionCube(int index)
        {
            Name = "CompanionCube";
            Tag = "Cube";
            ID = index;
            IsRespawnable = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
                base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive == false && IsRespawnable == false)
                Destroy();
        }

        public void Respawn()
        {
            if (IsRespawnable)
            {
                Position = StandartPosition;
                IsActive = true;
            }
        }
    }
}
