using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace SpaceGame
{
    public enum direction
    {
        up,
        down,
        left,
        right
    };

    public class WorldObject:IRenders,IUpdates,IInitable
    {
        //public List<SoundEffect> sounds;

        public Vector2 position;
        protected AnimatedTexture2D texture;
        protected Collider collider;

        public Boolean blocks;
        public Boolean destroys;

        public Boolean hitBlocks;
        public Boolean hitdestroys;

        public String objectName;

        public Dictionary<String,String> properties;

        public float angle;

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

        public virtual void Init(ContentManager content)
        {
            //Must set/have by this point a: collider, animTexture.

        }
        public virtual void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            texture.Render(spriteBatch, position + offset, angle, tint);
        }
        public virtual void Update(GameTime gameTime)
        {
            texture.Update(gameTime);
        }
        public virtual Boolean hits(WorldObject wo)
        {
            if (collider == null || wo.collider == null) return false;
            return collider.hit(wo.collider, hitPosition, wo.hitPosition);
        }
        public virtual Boolean hits(Map map)
        {
            if (collider == null) return false;
            return collider.hit(map,hitPosition);
        }
    }
}
