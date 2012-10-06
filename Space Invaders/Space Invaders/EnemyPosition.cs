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

        public static Vector2 move = new Vector2(2, 0);
        public static Vector2 overallPosition;
        public Vector2 position;

        public EnemyPosition()
        {

        }
        public void Update(byte x, byte y)
        {

        }
    }
}
