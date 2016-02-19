using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGamePortal3Practise
{
    public class TopDownPlayer : TopDownEntity
    {
        public MainDirections viewDirection;

        private KeyboardState previousState;
        private bool hasMoved;

        public TopDownPlayer(Vector2 position)
        {
            Name = "Chell";

            offset = new Vector2(0, 1); // only for 32x64p sized chell! moves "collider" to chell's lower part

            StandartPosition = position;
            Position = StandartPosition;
        }

        public override void LoadContent()
        {
            InputManager.OnKeyPressed += OnKeyPressed;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                Position = StandartPosition;
                SceneManager.CurrentScene.ResetPortals();
                IsActive = true;
            }
        }

        public override void Destroy()
        {
            InputManager.OnKeyPressed -= OnKeyPressed;

            base.Destroy();
        }

        private void OnKeyPressed(InputEventArgs eventArgs)
        {
            switch (eventArgs.Key)
            {
                case Keys.W:
                    viewDirection = MainDirections.Up;
                    Move(-directionDown);
                    hasMoved = true;
                    break;
                case Keys.A:
                    viewDirection = MainDirections.Left;
                    Move(-directionRight);
                    hasMoved = true;
                    break;
                case Keys.S:
                    viewDirection = MainDirections.Down;
                    Move(directionDown);
                    hasMoved = true;
                    break;
                case Keys.D:
                    viewDirection = MainDirections.Right;
                    Move(directionRight);
                    hasMoved = true;
                    break;
                default:
                    break;
            }

            switch (eventArgs.MouseButton)
            {
                case MouseButtons.LeftButton:
                    Shoot(SceneManager.CurrentScene.PortalBlue);
                    break;
                case MouseButtons.RightButton:
                    Shoot(SceneManager.CurrentScene.PortalOrange);
                    break;
                default:
                    break;
            }
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

            Portal otherPortal = SceneManager.GetDestinationPortal(portal);

            if (otherPortal.Position == position)
                return;

            portal.Position = position;
        }

        private Vector2 GetPortalablePosition(Vector2 portalPosition)
        {
            PortalGunShot shot = new PortalGunShot(Position + offset);

            while (hasMoved)
            {
                Tile currentTile = map.GetTile(shot.Position);

                if (currentTile is IronWall)
                    break;

                foreach (var item in SceneManager.CurrentScene.GameObjects)
                {
                    if ((item is TopDownMaterialEmancipationGrill && ((TopDownMaterialEmancipationGrill)item).isOn) || item is Portal)
                        if (((Entity)item).Position == shot.Position)
                        {
                            shot.Destroy();
                            return portalPosition;
                        }
                }

                if (currentTile.IsPortalable)
                {
                    Vector2 shotPosition = shot.Position;
                    shot.Destroy();
                    return shotPosition;
                }

                MoveInPlayersViewDirection(shot);
            }
            shot.Destroy();
            return portalPosition;
        }

        //public void ProcessInput()
        //{
        //    KeyboardState keyState = Keyboard.GetState();
        //    MouseState mouseState = Mouse.GetState();

        //    if (PlayerCanMove(keyState, Keys.W))
        //    {
        //        viewDirection = ViewDirection.Up;
        //        Move(-directionDown);
        //    }

        //    if (PlayerCanMove(keyState, Keys.A))
        //    {
        //        viewDirection = ViewDirection.Left;
        //        Move(-directionRight);
        //    }

        //    if (PlayerCanMove(keyState, Keys.S))
        //    {
        //        viewDirection = ViewDirection.Down;
        //        Move(directionDown);
        //    }

        //    if (PlayerCanMove(keyState, Keys.D))
        //    {
        //        viewDirection = ViewDirection.Right;
        //        Move(directionRight);
        //    }

        //    //if (CheckKeyState(keyState, Keys.E))
        //    //Take();

        //    //if (CheckKeyState(keyState, Keys.F))
        //    //    GameManager.Graphics.ToggleFullScreen();
        //    //    GameManager.Graphics.ApplyChanges();

        //    if (mouseState.LeftButton == ButtonState.Pressed)
        //        Shoot(SceneManager.CurrentScene.PortalBlue);
        //    if (mouseState.RightButton == ButtonState.Pressed)
        //        Shoot(SceneManager.CurrentScene.PortalOrange);

        //    previousState = keyState;
        //}
    }
}
