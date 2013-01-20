using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceGame
{
    class TriggerObject:MovingWorldObject
    {
        public TriggerObject()
        {

        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.hitTriggers)
            {
                this.trigger();
            }
        }
        public virtual void trigger()
        {

        }
    }
}
