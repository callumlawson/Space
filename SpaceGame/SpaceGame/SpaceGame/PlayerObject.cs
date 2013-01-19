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
        private float speedMulti = 0.8f;

        public PlayerObject()
        {

        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Console.WriteLine(this.hitBlocks);

            KeyboardState ks = Keyboard.GetState();
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

            this.angle =  (float)Math.Atan2(this.velocity.Y, this.velocity.X);

            if (velocity.Length() > 0.8f)
            {
                this.texture.setAnim("walking");
            }
            else
            {
                this.texture.setAnim("stationary");
            }

            base.Update(gameTime);
        }

        public override void Init(ContentManager content)
        {
            base.Init(content);
            List<animationSub> subs= new List<animationSub>();
            subs.Add(new animationSub("stationary",0,0));
            subs.Add(new animationSub("walking",1,2));
            this.texture = new AnimatedTexture2D(content.Load<Texture2D>("player"),64,subs);
            this.collider = new CircleCollider(new Vector2(32, 32), 30);
            this.texture.skipC = 10;
            this.texture.setAnim("stationary");


        }
    }
}
