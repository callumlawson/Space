﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    public class LootObject:TriggerObject
    {
        protected int loot;
        public LootObject()
        {

        }

        public override void Init(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.Init(content);
            this.collider = new RectangleCollider(new Vector2(0, 0), new Vector2(64, 64));
            Random r = new Random();
            int choo = r.Next(0,100);
            if(choo < 3)
            {
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.jewelPurple), 64, animationSub.def);
                this.loot = 2500;
            }
            else if (choo < 10)
            {
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.jewelBlue), 64, animationSub.def);
                this.loot = 2000;
            }
            else if (choo <  20)
            {
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.jewelOrange), 64, animationSub.def);
                this.loot = 1500;
            }
            else if (choo < 50)
            {
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.jewelGreen), 64, animationSub.def);
                this.loot = 1000;
            }
            else
            {
                this.texture = new AnimatedTexture2D(content.Load<Texture2D>(FileNames.jewelRed), 64, animationSub.def);
                this.loot = 500;
            }

            this.texture.skipC = 4;
        }
        public override void trigger()
        {
            base.trigger();
            onObjectMessage(new objectMessageEventArgs(loot));
            onDestroyMe();
        }
    }
}
