using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace SpaceGame
{
    public class DoorObject:WorldObject 
    {
        public Room room;

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }

        public override void Init(ContentManager content)
        {
            string direction = this.props["direction"];
        }

        public override void Init(ContentManager content, Room room)
        {
            string direction = this.props["direction"];
            this.room = room;
        }

        public override void Render(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Vector2 offset, Microsoft.Xna.Framework.Color tint)
        {
            

        }

        

    }
}
