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

        int points;
        public Enemy(Texture2D _texture, Vector2 _position, int _points, byte _Number)
            : base(_texture, _position)
        {
            points = _points;
            Number = _Number;
        }
        public bool EnemyUpdate(GameTime gameTime, EnemyPosition position)
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
            }
        }

        public Rectangle EnemyBox1()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width / 2, texture.Height);
        }
        public Rectangle EnemyBox2()
        {
            return new Rectangle((int)position.X + texture.Width / 2, (int)position.Y, texture.Width / 2, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, EnemyBox1(), EnemyBox1(), Color.White);
        }
    }
}
