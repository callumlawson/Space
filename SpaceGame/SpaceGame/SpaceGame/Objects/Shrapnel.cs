using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame
{
    public class Shrapnel:MovingWorldObject
    {
        protected int st;
        public Shrapnel(Vector2 velocity, int st)
        {
            this.st = st;
            this.friction = 0.95f;
            this.velocity = velocity;
        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
            if(st == 0)
            {
                this.collider = new CircleCollider(new Vector2(17.5f,17.5f),17.5f);
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.shrapnal1),35,animationSub.def);

            }
            else if(st == 1)
            {
                this.collider = new CircleCollider(new Vector2(22f,22f),22f);
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.shrapnal2), 44, animationSub.def);
                
            }
            else if (st == 2)
            {
                this.collider = new CircleCollider(new Vector2(20f, 20f), 20f);
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.shrapnal3), 40, animationSub.def);
            }
            this.destroys = true;
            this.triggers = true;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.blocks)
            {
                this.velocity *= -1;
            }
            this.angle += this.velocity.Length() / 20;
            if (this.velocity.Length() < 1)
            {
                this.onDestroyMe();
            }
        }
    }
}
