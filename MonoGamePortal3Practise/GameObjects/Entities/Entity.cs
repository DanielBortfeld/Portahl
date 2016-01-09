using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    public enum ViewDirection { Up, Down, Left, Right }

    class Entity : GameObject
    {
        protected static PortalBlue portalBlue = new PortalBlue(Vector2.Zero);
        protected static PortalOrange portalOrange = new PortalOrange(Vector2.Zero);

        public Vector2 Position;
        public Vector2 StandartPosition;
        //public Rectangle Collider
        //{
        //    get { return new Rectangle((int)Position.X, (int)Position.Y, spriteWidth, spriteWidth); }
        //}

        protected Map map;

        protected Vector2 offset = Vector2.Zero;
        protected Vector2 directionDown = new Vector2(0, 1);
        protected Vector2 directionRight = new Vector2(1, 0);

        protected int spriteWidth = 32;

        private Player player;

        public Vector2 OffsetPosition
        {
            get { return Position + offset; }
        }

        public Vector2 OffsetStandartPosition
        {
            get { return StandartPosition + offset; }
        }

        public Entity() : base()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameManager.SpriteSheet, Position * spriteWidth, GameManager.GetSpriteRect(Name), Color.White);
        }

        public override void LoadContent()
        {
            map = (Map)GameManager.FindGameObject("ChamberOne");
            player = (Player)GameManager.FindGameObject("Chell");
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
        }

        public void MoveInViewDirection(Entity entity)
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
            foreach (var item in GameManager.GameObjects)
            {
                if (item is Entity)
                    if (((Entity)item).Position == targetPosition)
                    {
                        if (item is MaterialEmancipationGrill)
                            HandleEmancipationGrill((MaterialEmancipationGrill)item);

                        else if (item is WeightedCompanionCube)
                        {
                            MoveInViewDirection((Entity)item);
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

        private void HandleEmancipationGrill(MaterialEmancipationGrill item)
        {
            if (item.isOn)
            {
                if (this is Player)
                    ResetPortals();
                else if (this is WeightedCompanionCube)
                    Position = StandartPosition;
            }
        }

        private void ResetPortals()
        {
            GameManager.RemoveGameObject(GameManager.FindGameObject("PortalOrange"));
            GameManager.RemoveGameObject(GameManager.FindGameObject("PortalBlue"));
            portalBlue = new PortalBlue(Vector2.Zero);
            portalOrange = new PortalOrange(Vector2.Zero);
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