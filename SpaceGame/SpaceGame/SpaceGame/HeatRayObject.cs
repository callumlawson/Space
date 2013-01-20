using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    class HeatRayObject:WorldObject
    {
        
        public HeatRayObject()
        {

        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
            this.collider = new RectangleCollider(new Vector2(13, 13), new Vector2(56, 55));
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
