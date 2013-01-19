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
        private int width;
        private int height;
        private Tile[,] tiles;
        public Map()
        {
            
        }
        public void Setup(int width, int height, Tile[,] tiles)
        {
            this.width = width;
            this.height = height;
            this.tiles = tiles;
        }
        public void Render(
    }
}
