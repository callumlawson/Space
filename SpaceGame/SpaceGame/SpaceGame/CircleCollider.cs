using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceGame
{
    public class CircleCollider:Collider
    {
        public float radius;
        public CircleCollider()
        {

        }
        public override bool hit(RectangleCollider c, Vector2 p1, Vector2 p2)
        {
            return c.hit(this, p1, p2);
        }
        public override bool hit(LineCollider lc, Vector2 p1, Vector2 p2)
        {
            float a = lc.secondPosition.X - lc.position.X;
            float b = lc.secondPosition.Y - lc.position.Y;
            float c = (position.X + p1.X) - (lc.position.X + p2.X);
            float d = (position.Y + p1.Y) - (lc.position.Y + p2.Y);
            float r = radius;
            if ((d * a - c * b) * (d * a - c * b) <= r * r * (a * a + b * b))
            {
                if (c * c + d * d <= r * r)
                {
                    // Line segment start point is inside the circle
                    return true;
                }
                if ((a - c) * (a - c) + (b - d) * (b - d) <= r * r)
                {
                    // Line segment end point is inside the circle
                    return true;
                }
                if (c * a + d * b >= 0 && c * a + d * b <= a * a + b * b)
                {
                    // Middle section only
                    return true;
                }
            }
            return false;
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
            List<RectangleCollider> recs = fromMap(position - new Vector2(radius,radius), position + new Vector2(radius,radius), map);
            foreach (RectangleCollider re in recs)
            {
                if (this.hit(re, p, new Vector2(0, 0)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
