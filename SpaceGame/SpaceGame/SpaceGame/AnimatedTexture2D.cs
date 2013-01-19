using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SpaceGame
{
    class AnimatedTexture2D:IRenders,IUpdates
    {
        protected Texture2D sheet;
        protected int width;
        protected int frame = 0;
        protected int frames;

        protected int skip = 0;
        protected int skipC = 0;

        public AnimatedTexture2D()
        {

        }
        public void Setup(Texture2D spriteSheet, int width)
        {
            this.width = width;
            this.frames = spriteSheet.Width / width;
            this.sheet = spriteSheet;
        }
    }
}
