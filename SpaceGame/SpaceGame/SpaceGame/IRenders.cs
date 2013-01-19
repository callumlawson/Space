using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    public interface IRenders
    {
        void render(SpriteBatch spriteBatch,Vector2 offset,Color tint);
    }
}
