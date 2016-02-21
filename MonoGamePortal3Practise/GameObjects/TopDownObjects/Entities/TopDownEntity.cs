using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    public enum MainDirections { None, Up, Down, Left, Right }

    public class TopDownEntity : Entity
    {
        public delegate void TopDownEventHandler();
        public event TopDownEventHandler OnDeathThroughToxicGoo, OnTraversingEmancipationGrill;

        public bool IsActive = true;
        protected int spriteWidth = 32;
        protected TopDownMap map;
        protected TopDownPlayer player;

        protected Vector2 offset = Vector2.Zero;
        protected Vector2 directionDown = new Vector2(0, 1);
        protected Vector2 directionRight = new Vector2(1, 0);

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
            spriteBatch.Draw(SpriteSheet, Position * spriteWidth, SpriteRect, White);
        }

        public override void LoadContent()
        {
            map = (TopDownMap)SceneManager.CurrentScene.FindGameObjectTag("TDMap");
            player = (TopDownPlayer)SceneManager.CurrentScene.FindGameObject("Chell");
        }

        public override void Move(Vector2 direction)
        {
            Vector2 targetPosition = GetTargetPosition(this, direction, offset);
            Tile targetTile = GetTargetTile(targetPosition);

            if (EntityBlocksPosition(targetPosition))
                return;

            if (targetTile is ToxicGooAnim)
            {
                IsActive = false;
                if (OnDeathThroughToxicGoo != null)
                    OnDeathThroughToxicGoo();
            }
            else if (targetTile.IsWalkable)
                Position += direction;

            GameManager.ReportMove();
        }

        public void MoveInPlayersViewDirection(Entity entity)
        {
            switch (player.viewDirection)
            {
                case MainDirections.Up:
                    entity.Move(-directionDown);
                    break;
                case MainDirections.Down:
                    entity.Move(directionDown);
                    break;
                case MainDirections.Left:
                    entity.Move(-directionRight);
                    break;
                case MainDirections.Right:
                    entity.Move(directionRight);
                    break;
                default:
                    break;
            }
        }

        private bool EntityBlocksPosition(Vector2 targetPosition)
        {
            foreach (var item in SceneManager.CurrentScene.GameObjects)
            {
                if (item.Position == targetPosition)
                {
                    if (item is TopDownEntity)
                        if (((TopDownEntity)item).IsActive)
                        {
                            if (item is TopDownMaterialEmancipationGrill)
                                HandleEmancipationGrill((TopDownMaterialEmancipationGrill)item);
                            else if (item is TopDownWeightedCompanionCube)
                            {
                                MoveInPlayersViewDirection((TopDownEntity)item);
                                return true;
                            }
                        }
                    if (item is Portal)
                        if (SceneManager.GetDestinationPortal((Portal)item).Position != Vector2.Zero)
                        {
                            Teleport((Portal)item);
                            return true;
                        }
                }
            }
            return false;
        }

        private void Teleport(Portal enteredPortal)
        {
            Portal destinationPortal = SceneManager.GetDestinationPortal(enteredPortal);
            Vector2 direction = Vector2.Zero;

            for (int x = 0; x < 4; x++)
            {
                switch (x)
                {
                    case 0:
                        direction = directionRight;
                        player.viewDirection = MainDirections.Right;
                        break;
                    case 1:
                        direction = -directionRight;
                        player.viewDirection = MainDirections.Left;
                        break;
                    case 2:
                        direction = directionDown;
                        player.viewDirection = MainDirections.Down;
                        break;
                    case 3:
                        direction = -directionDown;
                        player.viewDirection = MainDirections.Up;
                        break;
                }

                Vector2 targetPosition = GetTargetPosition(destinationPortal, direction, Vector2.Zero);
                Tile targetTile = GetTargetTile(targetPosition);

                if (targetTile.IsWalkable)
                    if (!EntityBlocksPosition(targetPosition))
                    {
                        Position = targetPosition - this.offset;
                        return;
                    }
            }
        }

        private void HandleEmancipationGrill(TopDownMaterialEmancipationGrill grill)
        {
            if (grill.isOn)
            {
                if (this is TopDownPlayer)
                    SceneManager.CurrentScene.ResetPortals();
                else if (this is TopDownWeightedCompanionCube)
                    IsActive = false;
            }
            if (OnTraversingEmancipationGrill != null)
                OnTraversingEmancipationGrill();
            if (grill.OnTraversingEmancipationGrill != null)
                grill.OnTraversingEmancipationGrill();
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
