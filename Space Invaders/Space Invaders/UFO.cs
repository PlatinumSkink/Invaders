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
        //UFO, the spaceship that will be flying on top of the course.

        int next;
        bool notMoving = true;
        int screenWidth;
        int UFOSpeed = 3;

        int points;

        static Random rand = new Random();

        //Get the number of points it will provide.
        public int GetPoints
        {
            get { return points; }
            set { points = value; }
        }

        public UFO(string _texture, Vector2 _position, int _screenWidth) : base (_texture, _position)
        {
            screenWidth = _screenWidth;
        }

        //Update the ship.
        public override void Update(GameTime gameTime)
        {
            //If it isn't moving, have the timer going. When it is over "next" (next time it should go) set a direction and a speed to it. Direction is to take it over the field, no matter which side it is on.
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
                //If it is moving, reset it as soon as it goes off-stage.
                if ((X < -width && direction.X == -1) ||  (X > screenWidth && direction.X == 1))
                {
                    Reset();
                }
            }
            base.Update(gameTime);
        }
        
        //Reset the UFO. Reset the timer, direction and speed. Randomize the next points between 100 and 300. Randomize what side it will appear from. Randomize when the ship will appear next.
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
            next = rand.Next(1000, 20000);
        }
    }
}
