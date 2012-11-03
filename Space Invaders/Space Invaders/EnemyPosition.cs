using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    class EnemyPosition
    {
        //The enemy position is its own class, while the enemy itself is another. This to make movement easier.

        public static bool moveRight = true;
        public static bool hitWall = false;

        public static Vector2 move = new Vector2(16, 0);
        public int Timer = 0;
        public int TimeUntilChange = 1000;
        public static Vector2 overallPosition;
        Vector2 position = Vector2.Zero;

        //X and Y regained.
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
        //Update moves the position every now and then. With higher difficulty, it will move more often.
        public bool Update(GameTime gameTime, int difficulty)
        {
            Timer += gameTime.ElapsedGameTime.Milliseconds;
            if (Timer > TimeUntilChange - (float)((800) * Math.Sqrt(0.02 * difficulty)))
            {
                Timer = 0;
                //TimeUntilChange = (int)(TimeUntilChange * 0.99);
                position += move;
                
                return true;
            }
            return false;
        }
    }
}
