using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceGame
{
    public class GasVentObject:TriggerObject
    {
        protected int cooldown = 0;
        protected int cooldownMax = 100;
        public GasVentObject()
        {

        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
            float extra = 30f;
            this.collider = new RectangleCollider(new Vector2(-extra, -extra), new Vector2(64+extra+extra, 64+extra+extra));
            this.texture = new AnimatedTexture2D(content.Load<Texture2D>("Traps/gasvent"), 64, animationSub.def);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (cooldown > 0) cooldown--;
        }
        public override void trigger()
        {
            if (cooldown == 0)
            {
                cooldown = cooldownMax;
                base.trigger();
                Random random = new Random();
                List<Gas> toFriend = new List<Gas>();
                for (int i = 0; i < 80; i++)
                {
                    float angle = (float)random.NextDouble() * MathHelper.TwoPi;
                    float speed = (float)random.NextDouble() * 1 + 0.5f;
                    Vector2 vel = new Vector2(-(float)Math.Sin(angle) * speed, (float)Math.Cos(angle) * speed);
                    Gas shrap = new Gas(vel, random.Next(0, 3));
                    foreach (Gas s in toFriend)
                    {
                        shrap.addFriend(s);
                    }
                    toFriend.Add(shrap);
                    shrap.position = this.position + new Vector2(32,32);
                    this.onAddThis(shrap);
                }
            }
        }
    }
}
