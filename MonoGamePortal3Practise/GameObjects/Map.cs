﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGamePortal3Practise
{
    class Map : GameObject
    {
        private Tile[,] tileMap;
        private Texture2D tileset;

        public int Width;
        public int Height;

        public int TileWidth = 32;
        public int TileHeight = 32;

        public Map(string name):base()
        {
            Name = name;

            tileset = GameManager.LoadTexture2D("Tileset");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Tile currentTileToBeDrawn = tileMap[x, y];
                    Vector2 position/*OfTheTilesUpperLeftPixelOnTheScreen*/ = new Vector2(x * TileWidth, y * TileHeight);
                    Rectangle frameOfTheFullTile = new Rectangle(currentTileToBeDrawn.TilesetPosX * TileWidth, currentTileToBeDrawn.TilesetPosY * TileHeight, TileWidth, TileHeight);
                    spriteBatch.Draw(tileset, position/*OfTheTilesUpperLeftPixelOnTheScreen*/, frameOfTheFullTile, Color.White);
                }
            }
        }

        public Tile GetTile(Vector2 targetPosition)
        {
            return tileMap[(int)targetPosition.X, (int)targetPosition.Y];
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
                    Entity entity =/**/ GetSpriteByType(spriteType); // returns sprite with a position on the tileset
                    if (entity != null)
                    {
                        entity.StandartPosition = new Vector2(x, y);
                        entity.Position = entity.StandartPosition;
                    }
                }
            }
        }
        private Entity GetSpriteByType(Color color)
        {
            if (color == new Color(237, 28, 36))
                return new HeavyDutySuperCollidingSuperButton();

            if (color == new Color(255, 174, 201))
                return new WeightedCompanionCube();

            if (color == new Color(255, 127, 39))
                return new Door(DoorPosition.Left);

            if (color == new Color(185, 122, 87))
                return new MaterialEmancipationGrill(GrillDirection.Down);

            else return null;
        }

        public void LoadMapFromImage(Texture2D image)
        {
            InitMapSize(40, 24);
            Color[] colors = GetColorsFromImage(image);
            InitTiles(colors);
        } // generates map
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