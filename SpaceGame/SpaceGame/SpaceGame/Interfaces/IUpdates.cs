using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace SpaceGame
{
    public interface IUpdates
    {
        void Update(GameTime gameTime);
    }
}
