using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceGame
{
    public class RectangleCollider:Collider
    {
        protected Vector2 dimentions;
        public RectangleCollider()
        {

        }
        public override bool hit(RectangleCollider c,Vector2 p1, Vector2 p2)
        {
            Vector2 pos1 = position + p1;
            Vector2 pos2 = c.position + p2;
            Vector2 size1 = dimentions;
            Vector2 size2 = c.dimentions;

            //check only in X
            return (((pos1.X + size1.X >= pos2.X) || (pos1.X <= pos2.X + size2.X)) && ((pos1.Y + size1.Y >= pos2.Y) || (pos1.Y <= pos2.Y + size2.Y)));
        }
        public override bool hit(LineCollider c, Vector2 p1, Vector2 p2)
        {
            return true;
        }
        public override bool hit(CircleCollider c, Vector2 p1, Vector2 p2)
        {
            return true;
        }
        public override bool hit(Map map, Vector2 p)
        {
            return true;
        }
    }
}
