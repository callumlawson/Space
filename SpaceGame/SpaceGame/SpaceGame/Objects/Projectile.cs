using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame
{
    public class Projectile:MovingWorldObject
    {
        protected string parentType;
        public Projectile(string parentType)
        {
            this.parentType = parentType;
        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
            int speed = 40;
            this.velocity = new Vector2((float)-Math.Sin(angle) * speed, (float)Math.Cos(angle) * speed);
            if (parentType == "Turret")
            {
                this.velocity /= 8f;
                this.collider = new CircleCollider(new Vector2(7.5f, 7.5f), 7.5f);
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>("projectile1"), 15, animationSub.def);
            }
            else if (parentType == "TurretTrack")
            {
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>("laserbeam"), 4, animationSub.def);
                this.collider = new LineCollider(new Vector2(0, 0), velocity);
                this.velocity /= 4f;
            }
            
            this.destroys = true;
            
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.hitBlocks)
            {
                this.onDestroyMe();
            }
        }
        public override void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            base.Render(spriteBatch, offset, tint,new Vector2(2,20));
        }
    }
}
