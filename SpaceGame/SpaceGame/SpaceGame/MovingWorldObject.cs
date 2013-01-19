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

        public MovingWorldObject()
        {
            this.position = new Vector2(50, 50);
            this.friction = 0.9f;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (!this.hitBlocks)
            {
                this.position += this.velocity;
            }
            velocity *= friction;
            base.Update(gameTime);
        }
    }
}
