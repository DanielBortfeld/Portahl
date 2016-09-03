// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class VictoryTrigger : TopDownTrigger
    {
        public VictoryTrigger(int index) : base(index)
        {
            Name = "VictoryTrigger";
            Tag = "VictoryTrigger";
        }

        public override void LoadContent()
        {
            Collider = new BoxCollider(this, 1, 1, true);
            Collider.OnCollisionEnter += OnTriggerEnter;

            base.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // dont draw this
        }

        public override void Trigger_OnMove()
        {
            if (Position == player.OffsetPosition)
                TriggerEvent(null);
        }

        public void SetSize(int width, int height)
        {
            if (Collider != null)
            {
                Collider.OnCollisionEnter -= OnTriggerEnter;
                Collider.Remove();
            }
            Collider = new BoxCollider(this, width, height, true);
            Collider.OnCollisionEnter += OnTriggerEnter;
        }

        public override void Destroy()
        {
            if (Collider != null)
                Collider.OnCollisionEnter -= OnTriggerEnter;
            base.Destroy();
        }

        private void OnTriggerEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Player")
                TriggerEvent(other.GameObject);
        }
    }
}
