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
        protected int walkCost;
        protected Texture2D tex;

        public Tile(Texture2D tex,int walkCost)
        {
        }

        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            spriteBatch.Draw(tex, offset, tint);
        }
    }
}
