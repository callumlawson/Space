﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
namespace SpaceGame
{
    public interface IInitable
    {
        void Init(ContentManager content);
    }
}
