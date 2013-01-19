using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SpaceGame
{
    class Room:IRenders,IUpdates
    {
        protected Map map;
        protected List<WorldObject> objects;

        public Room()
        {

        }
        public void Setup(Map map,List<WorldObject> objects)
        {
            this.map = map;
            this.objects = objects;
        }
        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {

        }
    }
}
