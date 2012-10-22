using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class GraphicalObject
    {
        protected Texture2D texture;
        protected Vector2 position;
        public static byte sizeMultiplier = 1;

        public static Main main;

        public float x
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
        public float y
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
        public int width
        {
            get
            {
                return texture.Width * sizeMultiplier;
            }
        }
        public int height
        {
            get
            {
                return texture.Height * sizeMultiplier;
            }
        }
        public Texture2D graphic
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        public GraphicalObject(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;
        }

        public virtual Rectangle Box()
        {
            return new Rectangle((int)x, (int)y, width, height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Box(), Color.White);
        }
    }
}
