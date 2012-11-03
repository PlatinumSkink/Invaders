using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class UFO : Spaceship
    {
        int next;
        bool notMoving = true;
        int screenWidth;
        int UFOSpeed = 3;

        int points;

        static Random rand = new Random();

        public int GetPoints
        {
            get { return points; }
            set { points = value; }
        }

        public UFO(string _texture, Vector2 _position, int _screenWidth) : base (_texture, _position)
        {
            screenWidth = _screenWidth;
        }
        public override void Update(GameTime gameTime)
        {
            if (notMoving == true)
            {
                timer += gameTime.ElapsedGameTime.Milliseconds;
                if (timer > next)
                {
                    if (X < 0)
                    {
                        direction = new Vector2(1, 0);
                    }
                    else if (X > 0)
                    {
                        direction = new Vector2(-1, 0);
                    }
                    movement = new Vector2(UFOSpeed, 0);
                    notMoving = false;
                }
            }
            else
            {
                if ((X < -width && direction.X == -1) ||  (X > screenWidth && direction.X == 1))
                {
                    Reset();
                }
            }
            base.Update(gameTime);
        }

        public void Reset()
        {
            timer = 0;
            direction = Vector2.Zero;
            movement = Vector2.Zero;
            notMoving = true;

            points = rand.Next(10, 31) * 10;

            if (rand.Next(0, 2) > 0)
            {
                X = screenWidth + width + 50;
            }
            else
            {
                X = -width - 50;
            }
            next = rand.Next(0, 20000);
        }
    }
}
