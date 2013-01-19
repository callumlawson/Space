using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceGame
{
    class LineCollider:Collider
    {
        protected float secondPosition;
        public LineCollider()
        {

        }
        public override bool hit(RectangleCollider c, Vector2 p1, Vector2 p2)
        {
            return c.hit(this, p1, p2);
        }
        public override bool hit(LineCollider c, Vector2 p1, Vector2 p2)
        {
            return true;
        }
        public override bool hit(CircleCollider c, Vector2 p1, Vector2 p2)
        {
            return c.hit(this, p1, p2);
        }
        public override bool hit(Map map, Vector2 p)
        {
            return true;
        }
    }
}
