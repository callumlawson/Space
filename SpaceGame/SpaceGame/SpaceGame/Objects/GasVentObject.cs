using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceGame.Objects
{
    public class GasVentObject:TriggerObject
    {
        public GasVentObject()
        {

        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
            this.collider = new RectangleCollider(new Vector2(0, 0), new Vector2(64, 64));
            this.texture = new AnimatedTexture2D(content.Load<Texture2D>("proximitymine"), 128, animationSub.def);
        }
        public override void trigger()
        {
            base.trigger();
            Random random = new Random();
            List<Gass> toFriend = new List<Gass>();
            for (int i = 0; i < 80; i++)
            {
                float angle = (float)random.NextDouble() * MathHelper.TwoPi;
                float speed = (float)random.NextDouble() * 3 + 3;
                Vector2 vel = new Vector2(-(float)Math.Sin(angle) * speed, (float)Math.Cos(angle) * speed);
                Gass shrap = new Gass(vel, random.Next(0, 3));
                foreach (Gass s in toFriend)
                {
                    shrap.addFriend(s);
                }
                toFriend.Add(shrap);
                shrap.position = this.position;
                this.onAddThis(shrap);
            }
            this.onDestroyMe();   
        }
    }
}
