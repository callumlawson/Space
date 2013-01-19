using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    public abstract class Collider
    {
        protected Vector2 position;
        public Collider()
        {

        }
        public bool hit(Collider c, Vector2 p1, Vector2 p2)
        {
            if (c.GetType() == typeof(RectangleCollider))
            {
                return hit((RectangleCollider)c, p1, p2);
            }
            else if (c.GetType() == typeof(CircleCollider))
            {
                return hit((CircleCollider)c, p1, p2);
            }
            else if (c.GetType() == typeof(LineCollider))
            {
                return hit((LineCollider)c, p1, p2);
            }
            return false;
        }
        public abstract Boolean hit(CircleCollider c, Vector2 p1,Vector2 p2);
        public abstract Boolean hit(RectangleCollider c, Vector2 p1,Vector2 p2);
        public abstract Boolean hit(LineCollider c, Vector2 p1, Vector2 p2);
        public abstract Boolean hit(Map map, Vector2 p);
    }
}
