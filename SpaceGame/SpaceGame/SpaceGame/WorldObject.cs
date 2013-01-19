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
        protected AnimatedTexture2D texture;
        protected Collider collider;
        protected direction faces;
        public WorldObject()
        {
            
        }
        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            texture.Render(spriteBatch, position + offset, tint);
        }
        public void Update(GameTime gameTime)
        {
            texture.Update(gameTime);
        }
        public Boolean hits(WorldObject wo)
        {
            
        }
        public Boolean hits(Map map)
        {
            
        }
    }
}
