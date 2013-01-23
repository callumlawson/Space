using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame
{
    public class Gas:MovingWorldObject
    {
        protected int st;
        public Gas(Vector2 velocity, int st)
        {
            this.st = st;
            this.friction = 0.99f;
            this.velocity = velocity;
        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
            if(st == 0)
            {
                this.collider = new CircleCollider(new Vector2(15f,15f),15f);
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.gas1),15,animationSub.def);

            }
            else if(st == 1)
            {
                this.collider = new CircleCollider(new Vector2(15f,15f),15f);
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.gas2),15,animationSub.def);
                
            }
            else if (st == 2)
            {
                this.collider = new CircleCollider(new Vector2(15f, 15f), 15f);
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.gas3), 15, animationSub.def);
            }
            this.destroys = true;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.blocks)
            {
                this.onDestroyMe();
            }
            if (this.velocity.Length() < 0.5f)
            {
                this.onDestroyMe();
            }
        }
    }
}
