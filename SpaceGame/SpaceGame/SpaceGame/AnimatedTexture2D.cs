using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SpaceGame
{
    public struct animationSub
    {
        public string id;
        public int startFrame;
        public int stopFrame;
        public animationSub(string id, int start, int stop)
        {
            this.id = id;
            this.startFrame = start;
            this.stopFrame = stop;
        }
    }
    public class AnimatedTexture2D:IRenders,IUpdates
    {
        protected Texture2D sheet;
        protected int width;
        protected int frame = 0;
        protected int frames;

        protected int skip = 0;
        public int skipC = 0;

        protected int subStart;
        protected int subStop;

        protected List<animationSub> subs;

        public AnimatedTexture2D(Texture2D sheet,int width,List<animationSub> subs)
        {
            this.sheet = sheet;
            this.width = width;
            this.subs = subs;
            this.frames = sheet.Width / width;
            subStart = 0;
            subStop = frames;
        }
        public void setAnim(string id)
        {
            foreach (animationSub sub in subs)
            {
                if (sub.id == id)
                {
                    subStart = sub.startFrame;
                    subStop = sub.stopFrame;
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            skip++;
            if (skip >= skipC)
            {
                skip = 0;
                frame++;
                if (frame > subStop)
                {
                    frame = subStart;
                }
            }
        }

        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            spriteBatch.Draw(sheet, offset, new Rectangle?(new Rectangle(frame * width, 0, width, sheet.Height)), tint);
        }

        public void Render(SpriteBatch spriteBatch, Vector2 offset, float angle, Color tint)
        {
            spriteBatch.Draw(sheet, new Rectangle((int)offset.X + width/2, (int)offset.Y + sheet.Height/2, width, sheet.Height), new Rectangle?(new Rectangle(frame * width, 0, width, sheet.Height)), tint, (angle + MathHelper.PiOver2), new Vector2(width / 2, sheet.Height / 2), SpriteEffects.None, 0f);
        }
    }
}
