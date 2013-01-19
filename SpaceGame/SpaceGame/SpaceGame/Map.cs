using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    public class Map:IRenders
    {
        public int width;
        public int height;
        public Tile[] tiles;
        public int tileSize;

        public Map()
        {
            
        }
        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tiles[i + (j*width)].Render(spriteBatch, new Vector2(offset.X + (tileSize * i), offset.Y + (tileSize * j)), tint);
                }
            }
        }
    }
}
