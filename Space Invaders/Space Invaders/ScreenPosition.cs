using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Space_Invaders
{
    abstract class ScreenPosition
    {
        protected Vector2 position;

        public static ContentManager content;

        public float X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }
        public float Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
            }
        }
        public ScreenPosition(Vector2 _position)
        {
            position = _position;
        }
    }
}
