using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
  public  class TopDownPlayer : TopDownEntity
    {
        public ViewDirection viewDirection;

        private KeyboardState previousState;

        public TopDownPlayer() : base()
        {
            Name = "Chell";

            offset = new Vector2(0, 1); // only for large chell! moves "collider" to chell's lower part

            StandartPosition = new Vector2(1, 3);
            Position = StandartPosition;
        }

        public override void Update(GameTime gameTime)
        {
            ProcessInput();
        }

        public void ProcessInput()
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (PlayerCanMove(keyState, Keys.W))
            {
                viewDirection = ViewDirection.Up;
                Move(-directionDown);
            }

            if (PlayerCanMove(keyState, Keys.A))
            {
                viewDirection = ViewDirection.Left;
                Move(-directionRight);
            }

            if (PlayerCanMove(keyState, Keys.S))
            {
                viewDirection = ViewDirection.Down;
                Move(directionDown);
            }

            if (PlayerCanMove(keyState, Keys.D))
            {
                viewDirection = ViewDirection.Right;
                Move(directionRight);
            }

            //if (CheckKeyState(keyState, Keys.E))
            //Take();

            //if (CheckKeyState(keyState, Keys.F))
            //    GameManager.Graphics.ToggleFullScreen();
            //    GameManager.Graphics.ApplyChanges();

            if (mouseState.LeftButton == ButtonState.Pressed)
                Shoot(portalBlue);
            if (mouseState.RightButton == ButtonState.Pressed)
                Shoot(portalOrange);

            previousState = keyState;
        }

        private bool PlayerCanMove(KeyboardState keyState, Keys key)
        {
            if (keyState.IsKeyDown(key) && !previousState.IsKeyDown(key))
                return true;
            return false;
        }

        private void Shoot(Portal portal)
        {
            Vector2 position = GetPortalablePosition(portal.Position);

            Portal otherPortal = GetDestinationPortal(portal);

            if (otherPortal.Position == position)
                return;

            portal.Position = position;
        }

        private Vector2 GetPortalablePosition(Vector2 portalPosition)
        {
            PortalGunShot shot = new PortalGunShot(Position + offset);

            while (true)
            {
                Tile currentTile = map.GetTile(shot.Position);

                if (currentTile is IronWall)
                    break;

                foreach (var item in SceneManager.CurrentScene.GameObjects)
                {
                    if ((item is TopDownMaterialEmancipationGrill && ((TopDownMaterialEmancipationGrill)item).isOn) || item is Portal)
                        if (((TopDownEntity)item).Position == shot.Position)
                            return portalPosition;
                }

                if (currentTile.IsPortalable)
                    return shot.Position;

                MoveInViewDirection(shot);
            }
            return portalPosition;
        }

        private void TakeCube()
        {

        }
    }
}
