using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    public abstract class Collider
    {
        public Vector2 position;
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
        public List<RectangleCollider> fromMap(Vector2 topLeft, Vector2 dimentions,Map map)
        {
            Vector2 ss = new Vector2(map.tileSize, map.tileSize);
            List<RectangleCollider> output = new List<RectangleCollider>();
            int tileMinX = ((int)topLeft.X) / map.tileSize;
            int tileMinY = ((int)topLeft.Y) / map.tileSize;
            int tileMaxX = (((int)(topLeft.X + dimentions.X)) / map.tileSize)+1;
            int tileMaxY = (((int)(topLeft.Y + dimentions.Y)) / map.tileSize)+1;
            for (int i = tileMinX; i <= tileMaxX; i++)
            {
                for (int j = tileMinY; j <= tileMaxY; j++)
                {
                    if (map.tiles[i + (j * map.width)].walkCost == 0)
                    {
                        Vector2 position = new Vector2(i * map.tileSize, j * map.tileSize);
                        output.Add(new RectangleCollider(position, ss));
                    }
                }
            }
            return output;
        }
        public abstract Boolean hit(CircleCollider c, Vector2 p1, Vector2 p2);
        public abstract Boolean hit(RectangleCollider c, Vector2 p1,Vector2 p2);
        public abstract Boolean hit(LineCollider c, Vector2 p1, Vector2 p2);
        public abstract Boolean hit(Map map, Vector2 p);
    }
}
