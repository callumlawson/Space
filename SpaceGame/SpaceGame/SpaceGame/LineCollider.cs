using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceGame
{
    public class LineCollider:Collider
    {
        public Vector2 secondPosition;
        public LineCollider()
        {

        }
        public LineCollider(Vector2 start, Vector2 end)
        {
            this.position = start;
            this.secondPosition = end;
        }
        public override bool hit(RectangleCollider c, Vector2 p1, Vector2 p2)
        {
            return c.hit(this, p1, p2);
        }
        public override bool hit(LineCollider c, Vector2 p1, Vector2 p2)
        {
            Vector2 q = position + p1;
            Vector2 p = c.position + p2;
            Vector2 r = c.secondPosition-c.position;
            Vector2 s = secondPosition-position;
            float rs = cross(r, s);
            if (rs == 0)
            {
                return (cross((q-p), r) != 0);
            }

            float t = cross((q-p),s)/rs;
            float u = cross((q-p), r) / rs;

            return (t > 0 && t < 1 && u > 0 && u < 1);
        }
        private float cross(Vector2 a, Vector2 b)
        {
            return (a.X * b.Y) - (a.Y * b.X);
        }
        public override bool hit(CircleCollider c, Vector2 p1, Vector2 p2)
        {
            return c.hit(this, p1, p2);
        }
        public override bool hit(Map map, Vector2 p)
        {
            float hold;
            Vector2 a = position;
            Vector2 b = secondPosition;
            if (a.X > b.X)
            {
                hold = a.X;
                a.X = b.X;
                b.X = hold;
            }
            if (a.Y > b.Y)
            {
                hold = a.Y;
                a.Y = b.Y;
                b.Y = hold;
            }
            List<RectangleCollider> recs = fromMap(a, b, map);
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
