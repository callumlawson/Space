using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    class PlayerObject:MovingWorldObject
    {
        public string transition = "none";

        public PlayerObject()
        {
            this.friction = 0.8f;
        }

        public Vector2 hack1
        {
            get
            {
                return new Vector2(velocity.X, 0);
            }
        }
        public Vector2 hack2
        {
            get
            {
                return new Vector2(0, velocity.Y);
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.hitdestroys)
            {
                this.onDestroyMe(true);
            }
            base.Update(gameTime);
            float speedMulti = 0.8f;
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Space))
            {
                speedMulti = 1.6f;
            }
            if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
            {
                this.velocity.Y -= 1 * speedMulti;
            }
            if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
            {
                this.velocity.X -= 1 * speedMulti;
            }
            if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
            {
                this.velocity.Y += 1 * speedMulti;
            }
            if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
            {
                this.velocity.X += 1 * speedMulti;
            }

            this.angle = (float)Math.Atan2(this.velocity.Y, this.velocity.X) + MathHelper.PiOver2;
            if (velocity.Length() > 0.8f)
            {
                this.texture.setAnim("walking");
            }
            else
            {
                if (speedMulti >= 1.5f)
                {
                    this.texture.setAnim("boosting");
                }
                else
                {
                    this.texture.setAnim("stationary");
                }
            }

            //Have we left the room?
            if (position.X > 1024)
            {
                transition = "right";
            }
            else if (position.X < 0)
            {
                transition = "left";
            }
             else if (position.Y > 768)
            {
                 transition = "down";
            }
             else if (position.Y < 0)
            {
                 transition = "up";
            }
            else{
                 transition = "none";
             }

        }

        public override void Init(ContentManager content)
        {
            base.Init(content);
            List<animationSub> subs = new List<animationSub>();

            subs.Add(new animationSub("stationary",0,0));
            subs.Add(new animationSub("walking",1,2));
            subs.Add(new animationSub("boosting", 3, 4));

            this.texture = new AnimatedTexture2D(content.Load<Texture2D>("player"),64,subs);

            this.collider = new CircleCollider(new Vector2(32, 50), 30);
            this.texture.skipC = 10;
            this.texture.setAnim("stationary");
        }
        public override void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            base.Render(spriteBatch, offset, tint, new Vector2(32, 32));
        } 
    }
}
