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
        bool pressedSpace = false;
        bool fired = false;

        bool hit = false;
        int hitTimer = 0;
        int timeHit = 1000;

        static Texture2D deathTexture;
        static Texture2D lifeTexture;

        public static Texture2D SetDeath
        {
            get { return deathTexture; }
            set { deathTexture = value; }
        }

        public bool GetHit
        {
            get { return hit; }
            set { hit = value; }
        }

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

        public Player(string _texture, Vector2 _position) : 
            base(_texture, _position) 
        {
            movement.X = 4;
        }
        public void GetLifeTexture()
        {
            lifeTexture = texture;
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (hit == false)
            {
                texture = lifeTexture;
                KeyInput();
                base.Update(gameTime);
                if (X < 0)
                {
                    X = 0;
                }
                else if (X > main.width - width)
                {
                    X = main.width - width;
                }
            }
            else
            {
                texture = deathTexture;
                hitTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (hitTimer > timeHit) 
                {
                    hitTimer = 0;
                    hit = false;
                }
            }
        }
        protected void KeyInput()
        {
            if (Main.km.Key(Keys.Right)) 
            {
                direction.X = 1;
            }
            else if (Main.km.Key(Keys.Left))
            {
                direction.X = -1;
            }
            else
            {
                direction.X = 0;
            }
            if (Main.km.Key(Keys.Space))
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