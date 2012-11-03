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
        //An abstract class that contains only a position.
        protected Vector2 position;

        public static ContentManager content;

        //X and Y to get the position.
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
        //Only one variable necessary, but it is the one furthest up in the heirarchy. 
        public ScreenPosition(Vector2 _position)
        {
            position = _position;
        }
    }
}
