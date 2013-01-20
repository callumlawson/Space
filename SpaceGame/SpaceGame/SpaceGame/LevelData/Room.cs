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
                        if (wos[i].destroys) wos[j].hitdestroys = true;
                        if (wos[j].blocks) wos[i].hitBlocks = true;
                        if (wos[j].destroys) wos[i].hitdestroys = true;
                        if (wos[i].triggers) wos[j].hitTriggers = true;
                        if (wos[j].triggers) wos[i].hitTriggers = true;
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
                if (wos[i].position.X < -100 || wos[i].position.Y < -100 || wos[i].position.X > 1124 || wos[i].position.Y > 868)
                {
                    wos[i].onDestroyMe();
                }
            }
            WorldObject[] war = new WorldObject[objects.Count];
            objects.CopyTo(war);
            foreach (WorldObject wo in war)
            {
                wo.Update(gameTime);
                wo.hitBlocks = false;
                wo.hitdestroys = false;
                wo.hitTriggers = false;
            }

        }

        protected ContentManager hehehe;
        public void Init(ContentManager content)
        {
            hehehe = content;

            foreach (WorldObject wo in objects)
            {
                wo.addThis += new addThisEventHandler(wo_addThis);
                wo.destroyMe += new destroyMeEventHandler(wo_destroyMe);

                if (wo.type == "Door")
                {
                    wo.Init(content, this); 
                }
                else
                {
                    wo.Init(content);
                }
            }
        }
        public void addWO(WorldObject wo)
        {
            wo.addThis += new addThisEventHandler(wo_addThis);
            wo.destroyMe += new destroyMeEventHandler(wo_destroyMe);
            this.objects.Add(wo);
        }
        void wo_destroyMe(WorldObject sender, Boolean dyrmi)
        {
            this.objects.Remove(sender);
            sender.addThis -= new addThisEventHandler(wo_addThis);
            sender.destroyMe -= new destroyMeEventHandler(wo_destroyMe);
        }

        void wo_addThis(WorldObject sender, WorldObject newObject)
        {
            newObject.Init(hehehe);
            newObject.addThis += new addThisEventHandler(wo_addThis);
            newObject.destroyMe += new destroyMeEventHandler(wo_destroyMe);

            this.objects.Add(newObject);
        }

        
    }
}
