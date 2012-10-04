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
        protected Vector2 movement = Vector2.Zero;
        protected Vector2 direction = Vector2.Zero;
        public Spaceship(Texture2D _texture, Vector2 _position) 
            : base(_texture, _position)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            position += movement * direction;
        }
    }
}
