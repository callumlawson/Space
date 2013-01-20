using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SpaceGame
{
    public class HeatRay:WorldObject
    {
        protected int heatDuration;
        protected int aNumber = 31;
        protected Vector2 d ;

        public HeatRay(int duration)
        {
            heatDuration = duration;
        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            
            base.Init(content);
            float sa = (float)Math.Sin(this.angle);
            float ca = (float)Math.Cos(this.angle);
            d = new Vector2(-sa * 44.5f, ca * 44.5f);
            this.texture = new AnimatedTexture2D(content.Load<Texture2D>("lasersection"), 80, animationSub.def);
            this.destroys = true;
            List<Collider> ls = new List<Collider>();
            
            Vector2 perp = (new Vector2(d.Y, -d.X));
            perp.Normalize();

            ls.Add(new LineCollider(perp * 5f, (d * aNumber) + (perp * 5f)));
            ls.Add(new LineCollider(perp * 40f, (d * aNumber) + (perp * 40f)));
            ls.Add(new LineCollider(perp * 75f, (d * aNumber) + (perp * 75f)));

            this.collider = new CombiCollider(ls);
        }
        public override void Render(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Vector2 offset, Microsoft.Xna.Framework.Color tint)
        {
            for (int i = 0; i < aNumber; i++)
            {
                base.Render(spriteBatch, offset+(d*((float)(i)+1.3f)) , tint,new Vector2(40f,40f));
            }
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
