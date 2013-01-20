using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace SpaceGame
{
    public class Room:IRenders,IUpdates,IInitable
    {
        public Map map;
        public List<WorldObject> objects;

        public Room()
        {

        }

        public void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {
            map.Render(spriteBatch, offset, tint);
            foreach (WorldObject wo in objects)
            {
                wo.Render(spriteBatch, offset, tint);
            }
        }
        public void Update(GameTime gameTime)
        {
            WorldObject[] wos = objects.ToArray();
            for (int i = 0; i < wos.Length; i++)
            {
                for (int j = i+1; j < wos.Length; j++)
                {
                    if (wos[i].hits(wos[j]))
                    {
                        if (wos[i].blocks) wos[j].hitBlocks = true;
                        if (wos[i].destroys) wos[j].destroys = true;
                        if (wos[j].blocks) wos[i].hitBlocks = true;
                        if (wos[j].destroys) wos[i].destroys = true;
                    }
                }
                if (!wos[i].hitBlocks)
                {
                    if (wos[i].hits(map))
                    {
                        //AWESOME FUCKING HACK
                        if (wos[i].GetType() == typeof(PlayerObject))
                        {
                            PlayerObject po = (PlayerObject)wos[i];
                            Vector2 hold = po.hack2;
                            po.setVelocity(po.hack1);
                            if (!po.hits(map)) continue;
                            po.setVelocity(hold);
                            if (!po.hits(map)) continue;
                        }
                        wos[i].hitBlocks = true;
                    }
                }
            }

            foreach (WorldObject wo in objects)
            {
                wo.Update(gameTime);
                wo.hitBlocks = false;
            }
        }
        public void Init(ContentManager content)
        {
            foreach (WorldObject wo in objects)
            {
                wo.Init(content);
            }
        }
    }
}
