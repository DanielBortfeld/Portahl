using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    public enum ViewDirection { Up, Down, Left, Right }

  public  class TopDownEntity : Entity
    {
        protected TopDownMap map;

        protected Vector2 offset = Vector2.Zero;
        protected Vector2 directionDown = new Vector2(0, 1);
        protected Vector2 directionRight = new Vector2(1, 0);

        protected int spriteWidth = 32;

        protected TopDownPlayer player;

        public Vector2 OffsetPosition
        {
            get { return Position + offset; }
        }

        public Vector2 OffsetStandartPosition
        {
            get { return StandartPosition + offset; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SceneManager.CurrentScene.SpriteSheet, Position * spriteWidth, SceneManager.CurrentScene.GetSpriteRect(Name), Color.White);
        }

        public override void LoadContent()
        {
            map = (TopDownMap)SceneManager.CurrentScene.FindGameObject("ChamberOne");
            player = (TopDownPlayer)SceneManager.CurrentScene.FindGameObject("Chell");
        }

        public virtual void Move(Vector2 direction)
        {
            Vector2 targetPosition = GetTargetPosition(this, direction, offset);
            Tile targetTile = GetTargetTile(targetPosition);

            if (EntityBlocksPosition(targetPosition))
                return;

            if (targetTile is ToxicGooAnim)
            {
                Position = StandartPosition;
                ResetPortals();
            }

            else if (targetTile.IsWalkable)
                Position += direction;

            GameManager.ReportMove();
        }

        public void MoveInViewDirection(TopDownEntity entity)
        {
            switch (player.viewDirection)
            {
                case ViewDirection.Up:
                    entity.Move(-directionDown);
                    break;
                case ViewDirection.Down:
                    entity.Move(directionDown);
                    break;
                case ViewDirection.Left:
                    entity.Move(-directionRight);
                    break;
                case ViewDirection.Right:
                    entity.Move(directionRight);
                    break;
                default:
                    break;
            }
        }

        protected Portal GetDestinationPortal(Portal enteredPortal)
        {
            if (enteredPortal == portalOrange)
                return portalBlue;
            else
                return portalOrange;
        }

        private bool EntityBlocksPosition(Vector2 targetPosition)
        {
            foreach (var item in SceneManager.CurrentScene.GameObjects)
            {
                if (item is TopDownEntity)
                    if (((TopDownEntity)item).Position == targetPosition)
                    {
                        if (item is TopDownMaterialEmancipationGrill)
                            HandleEmancipationGrill((TopDownMaterialEmancipationGrill)item);

                        else if (item is WeightedCompanionCube)
                        {
                            MoveInViewDirection((TopDownEntity)item);
                            return true;
                        }
                        if (item is Portal)
                            if (GetDestinationPortal((Portal)item).Position != Vector2.Zero)
                            {
                                Teleport((Portal)item);
                                return true;
                            }
                    }
            }
            return false;
        }

        private void HandleEmancipationGrill(TopDownMaterialEmancipationGrill grill)
        {
            if (grill.isOn)
            {
                if (this is TopDownPlayer)
                    ResetPortals();
                else if (this is WeightedCompanionCube)
                    Position = StandartPosition;
            }
        }

        private void Teleport(Portal enteredPortal)
        {
            Portal destinationPortal = GetDestinationPortal(enteredPortal);
            Vector2 direction = Vector2.Zero;

            for (int x = 0; x < 4; x++)
            {
                switch (x)
                {
                    case 0:
                        direction = directionRight;
                        player.viewDirection = ViewDirection.Right;
                        break;
                    case 1:
                        direction = -directionRight;
                        player.viewDirection = ViewDirection.Left;
                        break;
                    case 2:
                        direction = directionDown;
                        player.viewDirection = ViewDirection.Down;
                        break;
                    case 3:
                        direction = -directionDown;
                        player.viewDirection = ViewDirection.Up;
                        break;
                }

                Vector2 targetPosition = GetTargetPosition(destinationPortal, direction, Vector2.Zero);
                Tile targetTile = GetTargetTile(targetPosition);

                if (targetTile.IsWalkable)
                {
                    if (!EntityBlocksPosition(targetPosition))
                    {
                        Position = targetPosition - this.offset;
                        return;
                    }
                }
            }
        }

        private Tile GetTargetTile(Vector2 targetPosition)
        {
            Tile targetTile = map.GetTile(targetPosition);
            return targetTile;
        }

        private Vector2 GetTargetPosition(Entity source, Vector2 direction, Vector2 offset)
        {
            Vector2 targetPosition = source.Position + direction + offset;
            return targetPosition;
        }
    }
}