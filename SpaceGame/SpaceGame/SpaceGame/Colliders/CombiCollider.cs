using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame
{
    public class CombiCollider:Collider
    {
        public CombiCollider(List<Collider> with)
        {
            within = with;
        }
        protected List<Collider> within = new List<Collider>();
        public override bool hit(CircleCollider c, Vector2 p1, Vector2 p2)
        {
 	        foreach (Collider colin in within)
            {
                if(colin.hit(c,p1,p2)) return true;
            }
            return false;
        }
        public override bool hit(RectangleCollider c, Vector2 p1, Vector2 p2)
        {
 	        foreach (Collider colin in within)
            {
                if(colin.hit(c,p1,p2)) return true;
            }
            return false;
        }
        public override bool hit(LineCollider c, Vector2 p1, Vector2 p2)
        {
 	        foreach (Collider colin in within)
            {
                if(colin.hit(c,p1,p2)) return true;
            }
            return false;
        }
        public override bool hit(CombiCollider c, Vector2 p1, Vector2 p2)
        {
 	        foreach (Collider colin in within)
            {
                foreach(Collider colen in c.within)
                {
                    if(colin.hit(c,p1,p2)) return false;
                }
            }
            return false;
        }
        public override bool  hit(Map map, Vector2 p)
        {
 	        foreach (Collider colin in within)
            {
                if(colin.hit(map,p)) return true;
            }
            return false;
        }
    }
}
