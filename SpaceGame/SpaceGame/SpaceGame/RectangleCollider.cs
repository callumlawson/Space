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
            Vector2 start = c.position + p2;
            Vector2 end = c.position + p2;
            Vector2 corner = position+p1;
            float s1 = sideLine(corner.X, corner.Y, start, end);
            float s2 = sideLine(corner.X + dimentions.X, corner.Y, start, end);
            float s3 = sideLine(corner.X, corner.Y + dimentions.Y, start, end);
            float s4 = sideLine(corner.X + dimentions.X, corner.Y + dimentions.Y, start, end);
            if ((s1 > 0 && s2 > 0 && s3 > 0 && s4 > 0) || (s1 < 0 && s2 < 0 && s3 < 0 && s4 < 0))
            {
                return false;
            }
            else
            {
                float xTR = corner.X + dimentions.X;
                float xBL = corner.X;
                float yTR = corner.Y;
                float yBL = corner.Y + dimentions.Y;
                return !((start.X > xTR && end.X > xTR) || (start.X < xBL && end.X < xBL) || (start.Y > yTR && end.Y > yTR) || (start.Y < yBL && end.Y < yBL));
            }
        }
        public float sideLine(float x, float y, Vector2 start, Vector2 finish)
        {
            return ((finish.Y - start.Y) * x) + ((start.Y - finish.Y) * y) + ((finish.X * start.Y) - (finish.Y * start.X));
        }
        public override bool hit(CircleCollider c, Vector2 p1, Vector2 p2)
        {
            //check if circle inside square...
            Vector2 cicleAt = c.position + p2;
            Vector2 squareAt = position + p1;
            if (cicleAt.X >= squareAt.X && cicleAt.X < squareAt.X + dimentions.X && cicleAt.Y > squareAt.Y && cicleAt.Y < squareAt.Y + dimentions.Y)
            {
                return true;
            }
            else
            {
                foreach (LineCollider l in this.squareToLines())
                {
                    if (c.hit(l, p2, p1))
                    {
                        return true;
                    }
                }
                return false;
            }
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
            List<RectangleCollider> recs = fromMap(position, dimentions, map);
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
