using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceGame
{
    class MovingWorldObject:WorldObject
    {
        protected Vector2 velocity;
        protected float friction;
        
        public override Vector2 hitPosition
        {
            get
            {
                return base.hitPosition + velocity;
            }
        }
        public void setVelocity(Vector2 vel)
        {
            this.velocity = vel;
        }
        public MovingWorldObject()
        {
            this.position = new Vector2(130, 130);
            this.friction = 0.8f;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (!this.hitBlocks)
            {
                this.position += this.velocity;
            }
            else
            {
               
            }
            velocity *= friction;
            base.Update(gameTime);
        }
    }
}
