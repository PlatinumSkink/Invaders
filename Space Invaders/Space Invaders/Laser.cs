using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class Laser : GraphicalObject
    {
        public Laser(Texture2D _texture, Vector2 _position)
            : base(_texture, _position)
        {

        }
    }
}
