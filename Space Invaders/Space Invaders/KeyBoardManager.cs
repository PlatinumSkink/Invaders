﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    public class KeyBoardManager
    {
        public bool PressedSpace = false;
        bool PressedEnter = false;
        bool downPressed = false;
        bool upPressed = false;
        bool backPressed = false;

        public bool Key(Keys key)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(key))
            {
                if (key == Keys.Down && downPressed == false)
                {
                    downPressed = true;
                }
                else if (key == Keys.Down)
                {
                    return false;
                }
                if (key == Keys.Up && upPressed == false)
                {
                    upPressed = true;
                }
                else if (key == Keys.Up)
                {
                    return false;
                }
                if (key == Keys.Enter && PressedEnter == false)
                {
                    PressedEnter = true;
                } 
                else if (key == Keys.Enter) 
                {
                    return false;
                }
                if (key == Keys.Space && PressedSpace == false)
                {
                    PressedSpace = true;
                }
                else if (key == Keys.Space)
                {
                    return false;
                }
                if (key == Keys.Back && backPressed == false)
                {
                    backPressed = true;
                }
                else if (key == Keys.Back)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool InputKey()
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.GetPressedKeys().Length > 0)
            {
                return true;
            }
            return false;
        }

        public void Update()
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyUp(Keys.Space)) 
            {
                PressedSpace = false;
            }
            if (ks.IsKeyUp(Keys.Enter))
            {
                PressedEnter = false;
            }
            if (ks.IsKeyUp(Keys.Up))
            {
                upPressed = false;
            }
            if (ks.IsKeyUp(Keys.Down))
            {
                downPressed = false;
            }
            if (ks.IsKeyUp(Keys.Back))
            {
                backPressed = false;
            }
        }
    }
}