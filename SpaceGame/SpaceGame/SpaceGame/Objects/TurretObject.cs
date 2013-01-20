using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame
{
    public class TurretObject:WorldObject
    {
        protected float endAngle = MathHelper.TwoPi;
        protected int rateOfFire = 5;
        protected float rateofRotation = 0.05f;
        protected float startAngle = 0f;
        protected int rotationsFiring = 1;
        protected int rotationsIdle = 0;

        protected int inThisState = 0;
        protected int shotWait = 0;
        protected Boolean firing = true;
        protected float dir = 1f;

        protected WorldObject target = null;

        public TurretObject()
        {

        }

        public override void  Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
 	        base.Init(content);
            this.texture = new AnimatedTexture2D(content.Load<Texture2D>("turret1"), 80, animationSub.def);
            this.blocks = true;
            this.collider = new CircleCollider(new Vector2(40, 40), 38);
            if(this.type == "TurretTrack")
            {
                this.target = Game1.hacks;
            }
            if(props.ContainsKey("endAngle"))
            {
                this.endAngle = MathHelper.ToRadians(float.Parse(props["endAngle"]));
            }
            if(props.ContainsKey("rateOfFire"))
            {
                this.rateOfFire = int.Parse(props["rateOfFire"]);
            }
            if(props.ContainsKey("rateofRotation"))
            {
                this.rateofRotation = MathHelper.ToRadians(float.Parse(props["rateofRotation"]));
            }
            if(props.ContainsKey("rotationsFiring"))
            {
                this.rotationsFiring = int.Parse(props["rotationsFiring"]);
            }
            if(props.ContainsKey("rotationsIdle"))
            {
                this.rotationsIdle = int.Parse(props["rotationsIdle"]);
            }
            if(props.ContainsKey("startAngle"))
            {
                this.startAngle = MathHelper.ToRadians(float.Parse(props["startAngle"]));
            }
            this.angle = this.startAngle;
        }
        public override void  Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
 	        base.Update(gameTime);
            if (target == null)
            {
                this.angle += (rateofRotation * dir);
                if (!(this.startAngle == this.endAngle) && ((this.angle > endAngle) || (this.angle < this.startAngle)))
                {
                    dir *= -1;
                    inThisState++;
                    if ((firing && inThisState == rotationsFiring) || (!firing && inThisState == rotationsIdle))
                    {
                        inThisState = 0;
                        firing = !firing;
                        if ((firing && inThisState == rotationsFiring) || (!firing && inThisState == rotationsIdle))
                        {
                            inThisState = 0;
                            firing = !firing;
                        }
                    }

                }
            }
            else
            {
                Vector2 difference = target.position - this.position;
                float targetAngle = (float)Math.Atan2(difference.Y, difference.X) - MathHelper.PiOver2;
                if (Math.Abs(targetAngle - angle)%MathHelper.TwoPi > Math.Abs(angle - targetAngle)%MathHelper.TwoPi)
                {
                    this.angle += rateofRotation;
                }
                else
                {
                    this.angle -= rateofRotation;
                }
            }
            if (firing)
            {
                shotWait++;
                if (shotWait >= rateOfFire)
                {
                    shotWait = 0;
                    fire();
                }
            }
        }
        public void fire()
        {
            Projectile pj = new Projectile(this.type);
            pj.angle = this.angle;
            pj.position = this.position + new Vector2(33,33);
            pj.addFriend(this);
            this.onAddThis(pj);
        }
    }
}
