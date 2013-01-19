using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    class Map:IRenders
    {
        protected int width;
        protected int height;
        protected Tile[,] tiles;
        protected float tileSize;
        public Map()
        {
            
        }
        public void Setup(int width, int height, Tile[,] tiles,int tileSize)
        {
            this.width = width;
            this.height = height;
            this.tiles = tiles;
            this.tileSize = (float)tileSize;
        }
        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tiles[i, j].Render(spriteBatch, new Vector2(offset.X + (tileSize * i), offset.Y + (tileSize * j)), tint);
                }
            }
        }
    }
}
