using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SpaceGame
{
    public class Room:IRenders,IUpdates
    {
        public Map map;
        public List<WorldObject> objects;

        public Room()
        {

        }

        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            map.Render(spriteBatch, offset, tint);
            foreach (WorldObject wo in objects)
            {
                wo.Render(spriteBatch, offset, tint);
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach (WorldObject wo in objects)
            {
                wo.Update(gameTime);
            }
        }
    }
}
