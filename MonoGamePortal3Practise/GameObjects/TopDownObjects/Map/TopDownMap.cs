using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    public class TopDownMap : GameObject
    {
        private Tile[,] tileMap;
        private Texture2D tileset;

        private int tileWidth = 32;
        private int tileHeight = 32;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public int TileWidth { get { return tileWidth; } private set { tileWidth = value; } }
        public int TileHeight { get { return tileHeight; } private set { tileHeight = value; } }

        public TopDownMap(string name)
        {
            Name = name;

            tileset = GameManager.LoadTexture2D("Tileset");
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Tile currentTileToBeDrawn = tileMap[x, y];
                    Vector2 position/*OfTheTilesUpperLeftPixelOnTheScreen*/ = new Vector2(x * tileWidth, y * tileHeight);
                    Rectangle frameOfTheFullTile = new Rectangle(currentTileToBeDrawn.TilesetPosX * tileWidth, currentTileToBeDrawn.TilesetPosY * tileHeight, tileWidth, tileHeight);
                    spriteBatch.Draw(tileset, position/*OfTheTilesUpperLeftPixelOnTheScreen*/, frameOfTheFullTile, Color.White);
                }
            }
        }

        public Tile GetTile(Vector2 targetPosition)
        {
            return tileMap[(int)targetPosition.X, (int)targetPosition.Y];
        }

        public void LoadMapFromImage(Texture2D image)
        {
            InitMapSize(40, 24);
            Color[] colors = GetColorsFromImage(image);
            InitTiles(colors);
        }

        public void LoadSpritesFromImage(Texture2D image)
        {
            Color[] colors = GetColorsFromImage(image);
            InitSprites(colors);
        }

        private void InitSprites(Color[] pixelSnake)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = y * Width + x; // the position of the pixel in the pixel snake
                    Color spriteType = pixelSnake[index]; // the pixel in the pixel snake
                    TopDownEntity entity =/**/ GetSpriteByType(spriteType); // returns sprite with a position on the tileset
                    if (entity != null)
                    {
                        entity.StandartPosition = new Vector2(x, y);
                        entity.Position = entity.StandartPosition;
                    }
                }
            }
        }

        private TopDownEntity GetSpriteByType(Color color)
        {
            if (color == new Color(237, 28, 36))
                return new TopDownHeavyDutySuperCollidingSuperButton();

            if (color == new Color(255, 174, 201))
                return new TopDownWeightedCompanionCube();

            if (color == new Color(255, 127, 39))
                return new TopDownDoor(DoorPosition.Left);

            if (color == new Color(185, 122, 87))
                return new TopDownMaterialEmancipationGrill(GrillDirection.Down);

            if (color == new Color(0, 255, 0))
                return new TopDownVictoryTrigger();
            else return null;
        }

        // generates map
        private void InitMapSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            tileMap = new Tile[width, height];
        } // sets the size of the map in amount-of-tiles

        private Color[] GetColorsFromImage(Texture2D image)
        {
            Color[] allPixelsAsASnake = new Color[image.Width * image.Height];
            image.GetData<Color>(allPixelsAsASnake);
            return allPixelsAsASnake;
        } // makes a snake of pixels out of the small map

        private void InitTiles(Color[] pixelSnake)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = y * Width + x; // the position of the pixel in the pixel snake
                    Color tileType = pixelSnake[index]; // the pixel in the pixel snake
                    tileMap[x, y] =/**/ GetTileByType(tileType); // returns tile with a position on the tileset
                }
            }
        } // puts a tile for every pixel of the snake into the tileMap

        private Tile GetTileByType(Color color)
        {
            if (color == new Color(0, 0, 0))
                return new IronWall(2, 0); // iron wall

            if (color == new Color(63, 72, 204))
                return new ToxicGooAnim(2, 1); // water/goo

            if (color == new Color(255, 255, 255))
                return new Tile(1, Random.Next(3), false, true); // portal wall
            else
                return new Tile(0, Random.Next(3), true); // floor
        } // determines which colors refers to which tile and returns the tile
    }
}
