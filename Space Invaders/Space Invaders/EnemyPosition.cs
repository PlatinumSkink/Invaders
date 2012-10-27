using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    class EnemyPosition
    {
        public static bool moveRight = true;
        public static bool hitWall = false;

        public static Vector2 move = new Vector2(16, 0);
        public int Timer = 0;
        public int TimeUntilChange = 1000;
        public static Vector2 overallPosition;
        Vector2 position = Vector2.Zero;

        public float x
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
        public float y
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

        public EnemyPosition()
        {

        }
        public bool Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime.Milliseconds;
            if (Timer > TimeUntilChange) {
                Timer = 0;
                //TimeUntilChange = (int)(TimeUntilChange * 0.99);
                position += move;
                
                return true;
            }
            return false;
        }
    }
}
