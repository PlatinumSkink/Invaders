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
        //Enemies that stand in formation and are seeking down towards the player.

        public static bool moveRight = true;
        public static bool hitWall = false;

        public int animation = 0;

        public byte Number = 0;

        //Framewidth becomes the width of an animated thing.
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

        //Update everything. Move the enemy to the same position as its corresponding EnemyPosition. If too far on the left or right, return a "true" so that the enemies can proceed closer to the player.
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
        }

        //Rectangle for an animated enemy. Half the size of a normal.
        public override Rectangle Box()
        {
            return new Rectangle((int)X, (int)Y, width / 2, height);
        }

        //Source rectangle.
        public Rectangle EnemyBox()
        {
            return new Rectangle((int)animation * frameWidth, (int)0, frameWidth, height);
        }

        //Draw animated enemy.
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, Box(), EnemyBox(), Color.White);
            }
        }
    }
}
