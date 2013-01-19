using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceGame
{
    public struct entryPoint
    {
        public Room room;
        public WorldObject point;
        public entryPoint(Room room, WorldObject point)
        {
            this.room = room;
            this.point = point;
        }

    }
    class Ship:IUpdates
    {
        protected entryPoint mainEntry;
        public Ship()
        {

        }
        public void Update(GameTime gameTime)
        {

        }
    }
}
