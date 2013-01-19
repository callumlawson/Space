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

        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            
        }
    }
}
