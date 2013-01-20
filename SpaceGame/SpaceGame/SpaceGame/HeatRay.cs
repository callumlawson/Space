using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceGame
{
    public class HeatRay:WorldObject
    {
        protected int heatDuration;
        public HeatRay(int duration)
        {
            heatDuration = duration;

        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
        }
        public override void Render(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Vector2 offset, Microsoft.Xna.Framework.Color tint)
        {
            base.Render(spriteBatch, offset, tint);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            heatDuration--;
            if (heatDuration == 0)
            {
                this.onDestroyMe();
            }
        }
    }
}
