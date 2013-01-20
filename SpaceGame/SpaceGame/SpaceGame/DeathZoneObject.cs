using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame
{
    class DeathZoneObject: WorldObject
    {
        public DeathZoneObject()
        {

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }

        public override void Init(ContentManager content)
        {


            Console.WriteLine("Yes!");
           // if(name == "GasVent")
            //{
                //sounds.Add((SoundEffect) content.Load<SoundEffect>("Sounds/blackholedeath")); //Hit is sound one

                List<animationSub> subs = new List<animationSub>();

                subs.Add(new animationSub("idle", 0, 0));

                //subs.Add(new animationSub("triggered", 1, 2));

                this.texture = new AnimatedTexture2D(content.Load<Texture2D>("Traps/gasvent"), 64, subs);
            //}
        }
    }
}
