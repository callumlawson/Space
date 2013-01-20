using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SpaceGame.Objects
{
    class ProximityMine:TriggerObject
    {
        public ProximityMine()
        {

        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
            this.collider = new CircleCollider(new Vector2(64, 64), 64);
            List<animationSub> subs = new List<animationSub>();
            subs.Add(new animationSub("idle", 0, 0));
            subs.Add(new animationSub("triggered", 1, 4));
            this.texture = new AnimatedTexture2D(content.Load<Texture2D>("proximitymine"), 128, subs);
        }
        public override void trigger()
        {
            base.trigger();
            this.texture.animEndEvent += new animEndEventDelegate(texture_animEndEvent);
            this.texture.setAnim("triggered");
        }

        void texture_animEndEvent(AnimatedTexture2D sender)
        {
            this.texture.animEndEvent -= new animEndEventDelegate(texture_animEndEvent);
            Random random = new Random();
            for (int i = 0; i < 15; i++)
            {
                float angle = (float)random.NextDouble() * MathHelper.TwoPi;
                float speed = (float)random.NextDouble() * 5 + 5;
                Vector2 vel = new Vector2(-(float)Math.Sin(angle) * speed, (float)Math.Cos(angle) * speed);

            }
        }
    }
}
