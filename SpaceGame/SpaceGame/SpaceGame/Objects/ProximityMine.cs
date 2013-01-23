using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    public class ProximityMine:TriggerObject
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
            this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.proximityMine), 128, subs);
            this.texture.setAnim("idle");
            this.texture.skipC = 5;
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
            List<Shrapnel> toFriend = new List<Shrapnel>();
            for (int i = 0; i < 30; i++)
            {
                float angle = (float)random.NextDouble() * MathHelper.TwoPi;
                float speed = (float)random.NextDouble() * 10 + 10;
                Vector2 vel = new Vector2(-(float)Math.Sin(angle) * speed, (float)Math.Cos(angle) * speed);
                Shrapnel shrap = new Shrapnel(vel, random.Next(0,3));
                foreach (Shrapnel s in toFriend)
                {
                    shrap.addFriend(s);
                }
                toFriend.Add(shrap);
                shrap.position = this.position + new Vector2(64,64);
                this.onAddThis(shrap);
            }
            this.onDestroyMe();
        }
    }
}
