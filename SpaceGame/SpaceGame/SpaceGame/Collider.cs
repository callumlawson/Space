using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    public class Collider
    {
        protected Vector2 position;
        public Collider()
        {

        }
        public virtual Boolean hit(Collider c, Vector2 p1,Vector2 p2);
        public virtual Boolean hit(Map map, Vector2 p);
    }
}
