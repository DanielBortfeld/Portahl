﻿// Copyright (c) 2016 Daniel Bortfeld
using System;

namespace MonoGamePortal3Practise
{
    public class Tile
    {
        public int TilesetPosX;
        public int TilesetPosY;

        public bool IsWalkable = false;
        public bool IsPortalable = false;

        public Tile(int x, int y)
        {
            TilesetPosX = x;
            TilesetPosY = y;
        }

        public Tile(int x, int y, bool isWalkable)
        {
            TilesetPosX = x;
            TilesetPosY = y;
            IsWalkable = isWalkable;
        }

        public Tile(int x, int y, bool isWalkable, bool isPortalable)
        {
            TilesetPosX = x;
            TilesetPosY = y;
            IsWalkable = isWalkable;
            IsPortalable = isPortalable;
        }
    }
}
