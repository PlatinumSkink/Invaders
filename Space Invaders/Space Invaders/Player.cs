﻿using System;
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
        public Player(Texture2D _texture, Vector2 _position) : 
            base(_texture, _position) 
        {
            movement.X = 4;
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
        }
    }
}