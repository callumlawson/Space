using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SpaceGame
{
    public class AnimatedTexture2D:IRenders,IUpdates
    {
        protected Texture2D sheet;
        protected int width;
        protected int frame = 0;
        protected int frames;

        protected int skip = 0;
        protected int skipC = 0;

        public AnimatedTexture2D()
        {
            this.frames = sheet.Width / width;
        }
        public void Update(GameTime gameTime)
        {
            skip++;
            if (skip >= skipC)
            {
                skip = 0;
                frame++;
                if (frame == frames)
                {
                    frame = 0;
                }
            }
        }
        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            spriteBatch.Draw(sheet, offset, new Rectangle?(new Rectangle(frame * width, 0, width, sheet.Height)), tint);
        }
    }
}
