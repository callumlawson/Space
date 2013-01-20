using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SpaceGame
{
    public class Ambient:WorldObject
    {
        public Ambient()
        {

        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
            Texture2D getTex = content.Load<Texture2D>(props["imageName"]);
            int witdth = getTex.Width;

            if(props.ContainsKey("width"))
            {
                witdth = int.Parse(props["width"]);
            }
            this.texture = new AnimatedTexture2D(getTex, witdth, animationSub.def);
        }
    }
}
