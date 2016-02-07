using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class Camera : GameObject
    {
        public float X;
        public float Y;

        private SideScrollPlayer player;

        private float viewMargin = 0.25f;
        private float cameraTranslationX;
        private float cameraTranslationY;

        private int backgroundWidth = 1920;
        private int backgroundHeight = 1080;

		public Camera(SideScrollPlayer player)
		{
			this.player = player;
		}

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void LoadContent()
        {
        }

        public void SetTarget(SideScrollPlayer player)
        {
            this.player = player;
        }

        public void SetBackgroundResolution(int backgroundWidth, int backgroundHeight)
        {
            this.backgroundWidth = backgroundWidth;
            this.backgroundHeight = backgroundHeight;
        }

        public void UpdatePosition(Viewport viewport)
        {
            //borders left and right:
            float marginWidth = viewport.Width * viewMargin;
            float marginLeft = X + marginWidth;
            float marginRight = X + viewport.Width - marginWidth;

            //borders top and floor
            float marginHeight = viewport.Height * viewMargin;
            float marginTop = Y + marginHeight;
            float marginFloor = Y + viewport.Height - marginHeight;

            // resets translation:
            cameraTranslationX = 0f;
            cameraTranslationY = 0f;

            // translation:
            if (player.Pivot.X < marginLeft) // wenn der player weiter links als die linke grenze ist...
                cameraTranslationX = player.Pivot.X - marginLeft;
            else if (player.Pivot.X > marginRight) // wenn der player weiter rechts als die rechte grenze ist...
                cameraTranslationX = player.Pivot.X - marginRight;

            if (player.Pivot.Y < marginTop) // wenn der player weiter oben ist als die obere grenze...
                cameraTranslationY = player.Pivot.Y - marginTop;
            else if (player.Pivot.Y > marginFloor) // wenn der player weiter unten als die untere grenze...
                cameraTranslationY = player.Pivot.Y - marginFloor;
            //--> ...dann sag an, wie weit die cam verschoben werden muss

            // apply translation:
            X = MathHelper.Clamp(X + cameraTranslationX, 0f, backgroundWidth - GameManager.Graphics.GraphicsDevice.Viewport.Width);
            Y = MathHelper.Clamp(Y + cameraTranslationY, 0f, backgroundHeight - GameManager.Graphics.GraphicsDevice.Viewport.Height);
            // cam wird verschoben auf die aktuelle position
            // -> die cam wird aber immer innerhalb vom gesetzten background bleiben
        }
    }
}
