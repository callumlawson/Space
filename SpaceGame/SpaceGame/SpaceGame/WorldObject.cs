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

    public class WorldObject:IRenders,IUpdates
    {
        protected Vector2 position;
        protected AnimatedTexture2D texture;
        protected Collider collider;
        protected direction faces;

        public Boolean blocks;
        public Boolean destroys;

        public Boolean hitBlocks;
        public Boolean hitdestroys;

        public virtual Vector2 hitPosition
        {
            get
            {
                return position;
            }
        }
        public WorldObject()
        {
            
        }
        public virtual void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            texture.Render(spriteBatch, position + offset, tint);
        }
        public virtual void Update(GameTime gameTime)
        {
            texture.Update(gameTime);
        }
        public virtual Boolean hits(WorldObject wo)
        {
            return collider.hit(wo.collider, hitPosition, wo.hitPosition);
        }
        public virtual Boolean hits(Map map)
        {
            return collider.hit(map,hitPosition);
        }

    }
}
