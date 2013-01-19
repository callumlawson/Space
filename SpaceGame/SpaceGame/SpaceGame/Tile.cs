using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame
{
    public class Tile:IRenders
    {
        public int walkCost;
        public Texture2D tex;
        public Rectangle sourceRect;

        public Tile()
        {

        }

        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            spriteBatch.Draw(tex, offset, sourceRect, tint);
        }
    }
}
