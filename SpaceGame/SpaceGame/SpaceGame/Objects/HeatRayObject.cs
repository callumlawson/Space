using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SpaceGame
{
    public class HeatRayObject:WorldObject
    {
        protected int chargeCounter = 0;

        protected int chargeTime = 25;
        protected int fireTime = 20;

        public HeatRayObject()
        {

        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.angle = 0f ;
            base.Init(content);
            this.collider = new RectangleCollider(new Vector2(13, 13), new Vector2(56, 55));
            List<animationSub> subs = new List<animationSub>();
            subs.Add(new animationSub("charging", 0, 0));
            subs.Add(new animationSub("firing", 1, 4));
            subs.Add(new animationSub("fired", 5, 5));
            this.texture = new AnimatedTexture2D(content.Load<Texture2D>("laserdish"), 80, subs);
            this.texture.setAnim("charging");
            this.texture.skipC = 5;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            chargeCounter++;
            if (chargeCounter < chargeTime - 20) this.texture.setAnim("charging");
            else if (chargeCounter < chargeTime) this.texture.setAnim("firing");
            else if (chargeCounter < chargeTime + fireTime) this.texture.setAnim("fired");
            else
            {
                chargeCounter = 0;
                this.texture.setAnim("charging");
            }
            if (chargeCounter == chargeTime)
            {
                HeatRay hr = new HeatRay(fireTime);
                hr.position = this.position;
                hr.angle = this.angle;
                this.onAddThis(hr);
            }

        }
    }
}
