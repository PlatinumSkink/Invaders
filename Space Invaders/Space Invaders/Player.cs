using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    class Player : Spaceship
    {
        bool pressedSpace = false;
        bool fired = false;
        public bool Space
        {
            get { return pressedSpace; }
            set { pressedSpace = value; }
        }
        public bool Fired
        {
            get { return fired; }
            set { fired = value; }
        }

        public Player(Texture2D _texture, Vector2 _position) : 
            base(_texture, _position) 
        {
            movement.X = 4;
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            KeyInput(keyboard);
            base.Update(gameTime);
            if (x < 0) 
            {
                x = 0;
            }
            else if (x > main.width - width) 
            {
                x = main.width - width;
            }
        }
        protected void KeyInput(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Right)) 
            {
                direction.X = 1;
            }
            else if (ks.IsKeyDown(Keys.Left))
            {
                direction.X = -1;
            }
            else
            {
                direction.X = 0;
            }
            if (ks.IsKeyDown(Keys.Space))
            {
                Space = true;
            }
            else
            {
                Space = false;
            }
        }
    }
}