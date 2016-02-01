using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    public class Gravity
    {
        public float GravityForce = 2f;
        public float Timer;

        private CharacterController controller;

        public Gravity(CharacterController controller)
        {
            this.controller = controller;
        }

        public void Update(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //controller.velocity.Y += GravityForce * Timer;
        }
    }
}
