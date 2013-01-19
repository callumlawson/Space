using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    interface IRenders
    {
        public void render(SpriteBatch spriteBatch,Vector2 offset,Color tint);
    }
}
