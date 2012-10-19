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
        public Enemy(Texture2D _texture, Vector2 _position, int _points, byte _Number)
            : base(_texture, _position)
        {
            points = _points;
            Number = _Number;
        }
        public bool EnemyUpdate(GameTime gameTime, EnemyPosition invaderPosition)
        {
            x = invaderPosition.x - frameWidth / 2;
            y = invaderPosition.y;
            if (x > main.width - width && moveRight == true) {
                return true;
            } 
            else if (x < width / 2 && moveRight == false) 
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
            return new Rectangle((int)x, (int)y, width / 2, height);
        }

        public Rectangle EnemyBox()
        {
            return new Rectangle((int)animation * frameWidth, (int)0, frameWidth, height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Box(), EnemyBox(), Color.White);
        }
    }
}
