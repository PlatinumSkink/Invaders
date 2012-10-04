using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    abstract class GraphicalObject
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected float x
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }
        protected float y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
            }
        }
        protected int width
        {
            get
            {
                return texture.Width;
            }
        }
        protected int height
        {
            get
            {
                return texture.Height;
            }
        }

        public GraphicalObject(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;
        }

        protected Rectangle Box()
        {
            return new Rectangle((int)x, (int)y, width, height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Box(), Color.White);
        }
    }
}
