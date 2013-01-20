using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceGame
{
    public class RectangleCollider:Collider
    {
        public Vector2 dimentions;
        public RectangleCollider()
        {

        }
        public RectangleCollider(Vector2 pos, Vector2 dim)
        {
            this.dimentions = dim;
            this.position = pos;
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
            if(hit(c.position,p1,p2) || hit(c.secondPosition,p1,p2)) return true;
            foreach(LineCollider l in this.squareToLines())
            {
                if(c.hit(l,p2,p1))return true;
            }
            return false;
        }
        public bool hit(Vector2 aPoint, Vector2 p1, Vector2 p2)
        {
            aPoint += p2;
            p1 += position;
            return (aPoint.X >= p1.X && aPoint.X < p1.X + dimentions.X && aPoint.Y > p1.Y && aPoint.Y < p1.Y + dimentions.Y);
        }
        public override bool hit(CircleCollider c, Vector2 p1, Vector2 p2)
        {
            //check if circle inside square...
            if (hit(c.position, p1, p2)) return true;

            foreach (LineCollider l in this.squareToLines())
            {
                if (c.hit(l, p2, p1))
                {
                   return true;
                }
            }
            return false;
            
        }
        public LineCollider[] squareToLines()
        {
            Vector2 TL = position;
            Vector2 TR = position;
            TR.X +=dimentions.X;
            Vector2 BL = position;
            BL.Y +=dimentions.Y;
            Vector2 BR = position + dimentions;
            return new LineCollider[] { new LineCollider(TL, TR), new LineCollider(BL, BR), new LineCollider(TL, BL), new LineCollider(TR, BR) };
        }
        public override bool hit(Map map, Vector2 p)
        {
            List<RectangleCollider> recs = fromMap(position + p, dimentions, map);
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
