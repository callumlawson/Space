using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace SpaceGame
{
    public enum direction
    {
        up,
        down,
        left,
        right,
    };
    public delegate void destroyMeEventHandler(WorldObject sender,Boolean didYouReallyMeanIt);
    public delegate void addThisEventHandler(WorldObject sender,WorldObject newObject);
    public class WorldObject:IRenders,IUpdates,IInitable
    {
        //public List<SoundEffect> sounds;

        protected AnimatedTexture2D texture;
        protected Collider collider;

        public Vector2 position;

        public Boolean blocks;
        public Boolean destroys;
        public Boolean triggers;

        public Boolean hitBlocks;
        public Boolean hitdestroys;
        public Boolean hitTriggers;

        public String objectName;
        public String type;

        public Dictionary<String, String> props = new Dictionary<String, String>();

        public float angle;

        public virtual Vector2 hitPosition
        {
            get
            {
                return position;
            }
        }

        protected List<WorldObject> friends = new List<WorldObject>();

        public void addFriend(WorldObject newFriend)
        {
            newFriend.destroyMe += new destroyMeEventHandler(newFriend_destroyMe);
            friends.Add(newFriend);
        }

        void newFriend_destroyMe(WorldObject sender, Boolean dyrmi)
        {
            sender.destroyMe += new destroyMeEventHandler(newFriend_destroyMe);
            friends.Remove(sender);
        }

        public WorldObject()
        {
            
        }

        public virtual void Init(ContentManager content)
        {
            //Must set/have by this point a: collider, animTexture.
            if (props.ContainsKey("angle"))
            {
                this.angle = float.Parse(props["angle"]);
            }
        }

        public virtual void Init(ContentManager content, Room room)
        {
            //Must set/have by this point a: collider, animTexture.

        }
        public virtual void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint)
        {

            texture.Render(spriteBatch, position + offset, angle, tint);
        }
        public virtual void Render(SpriteBatch spriteBatch, Vector2 offset, Color tint, Vector2 center)
        {
            texture.Render(spriteBatch, position + offset, angle, tint, center);
        }
        public virtual void Update(GameTime gameTime)
        {
            texture.Update(gameTime);
        }
        public virtual Boolean hits(WorldObject wo)
        {
            if (collider == null || wo.collider == null) return false;
            if (this.friends.Contains(wo) || wo.friends.Contains(this)) return false;
            return collider.hit(wo.collider, hitPosition, wo.hitPosition);
        }
        public virtual Boolean hits(Map map)
        {
            if (collider == null) return false;
            return collider.hit(map,hitPosition);
        }
        public virtual void onDestroyMe()
        {
            onDestroyMe(false);
        }
        public virtual void onDestroyMe(Boolean dyrmi)
        {
            if (destroyMe != null)
            {
                destroyMe(this, dyrmi);
            }
        }
        public virtual void onAddThis(WorldObject newObject)
        {
            if (addThis != null)
            {
                addThis(this, newObject);
            }
        }
        public event destroyMeEventHandler destroyMe;
        public event addThisEventHandler addThis;
    }
}
