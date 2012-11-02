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

        public int animation = 0;

        public byte Number = 0;

        public int frameWidth
        {
            get { return width / 2; }
        }

        int points;

        public int GetPoints
        {
            get { return points; }
            set { points = value; }
        }

        public Enemy(string _texture, Vector2 _position, int _points, byte _Number)
            : base(_texture, _position)
        {
            points = _points;
            Number = _Number;
        }

        public bool EnemyUpdate(GameTime gameTime, EnemyPosition invaderPosition)
        {
            X = invaderPosition.x - frameWidth / 2;
            Y = invaderPosition.y;
            if (X > main.width - width && moveRight == true) {
                return true;
            } 
            else if (X < width / 2 && moveRight == false) 
            {
                return true;
            }
            
            return false;
            /*if (hitWall == true)
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
            if (moveRight == true && x > main.width - width) 
            {
                return true;
            }
            else if (moveRight == false && x < 0)
            {
                return true;
            }
            else
            {
                return false;
            }*/
        }

        public override Rectangle Box()
        {
            return new Rectangle((int)X, (int)Y, width / 2, height);
        }

        public Rectangle EnemyBox()
        {
            return new Rectangle((int)animation * frameWidth, (int)0, frameWidth, height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, Box(), EnemyBox(), Color.White);
            }
        }
    }
}
