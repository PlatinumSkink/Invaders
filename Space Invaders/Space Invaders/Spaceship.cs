using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class Spaceship : GraphicalObject
    {
        //Spaceship could as well be called "moving object". It is a graphical object that can move.

        protected Vector2 movement = Vector2.Zero;
        protected Vector2 direction = Vector2.Zero;
        public Spaceship(string _texture, Vector2 _position) 
            : base(_texture, _position)
        {

        }

        //It moves in Update.
        public virtual void Update(GameTime gameTime)
        {
            position += movement * direction;
        }
    }
}
