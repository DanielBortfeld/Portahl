// Copyright (c) 2016 Daniel Bortfeld

using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class DeathTrigger : VictoryTrigger
    {
        public DeathTrigger(int index) : base(index)
        {
            Name = "DeathTrigger";
            Tag = "DeathTrigger";
        }

        public override void LoadContent()
        {
            Collider = new BoxCollider(this, 1, 1, true);
            Collider.OnCollisionEnter += OnTriggerEnter;

            base.LoadContent();
        }

        public override void Destroy()
        {
            if (Collider != null)
                Collider.OnCollisionEnter -= OnTriggerEnter;
            base.Destroy();
        }

        private void OnTriggerEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Player" || other.GameObject.Tag == "Cube")
            {
                TriggerEvent(other.GameObject);
            }
        }
    }
}
