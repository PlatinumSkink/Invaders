using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class Enemy : Spaceship
    {
        public static bool moveRight = true;
        public static bool hitWall = false;



        int points;
        public Enemy(Texture2D _texture, Vector2 _position, int _points)
            : base(_texture, _position)
        {
            points = _points;
        }
        public bool EnemyUpdate(GameTime gameTime)
        {
            if (hitWall == true)
            {
                direction.X = 0;
                direction.Y = 1;
            }
            else if (moveRight == true)
            {
                direction.X = 1;
                direction.Y = 0;
            }
            else
            {
                direction.X = -1;
                direction.Y = 0;
            }
            base.Update(gameTime);
            return false;
        }
    }
}
