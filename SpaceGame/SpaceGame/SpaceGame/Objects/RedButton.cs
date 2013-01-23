using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame
{
    public class RedButton:TriggerObject
    {
        public RedButton()
        {

        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
            List<animationSub> subs = new List<animationSub>();
            subs.Add(new animationSub("depressed", 0, 1));
            subs.Add(new animationSub("pressed", 2, 3));
            this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.controls), 256, subs);
            this.texture.skipC = 5;
            this.texture.setAnim("depressed");
            this.collider = new RectangleCollider(new Vector2(38, 102), new Vector2(52, 52));
        }
        public override void trigger()
        {
            base.trigger();
            this.texture.setAnim("pressed");
            this.onObjectMessage(new objectMessageEventArgs());
        }
    }
}
