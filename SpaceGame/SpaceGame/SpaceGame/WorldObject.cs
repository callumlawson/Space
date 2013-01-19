using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    public enum direction
    {
        up,
        down,
        left,
        right
    };

    class WorldObject:IRenders,IUpdates
    {
        protected Vector2 position;
        protected direction faces;
        protected AnimatedTexture2D texture;
        public WorldObject()
        {
            
        }
        public void Setup(Vector2 position,direction faces,AnimatedTexture2D texture)
        {
            this.position = position;
            this.faces = faces;
            this.texture = texture;
        }
    }
}
