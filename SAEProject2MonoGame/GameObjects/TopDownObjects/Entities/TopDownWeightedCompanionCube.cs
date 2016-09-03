// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    /// <summary>
    /// It's the cube you can push around - e.g. onto a button - in the TopDown part of the game.
    /// </summary>
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
