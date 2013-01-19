using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceGame
{
    public class CircleCollider:Collider
    {
        protected float radius;
        public CircleCollider()
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
            float sqDistance = ((position + p1) - (c.position + p2)).LengthSquared();
            float sqRads = (radius + c.radius);
            sqRads *= sqRads;
            return (sqDistance <= sqRads);
        }
        public override bool hit(Map map, Vector2 p)
        {
            return true;
        }
    }
}
