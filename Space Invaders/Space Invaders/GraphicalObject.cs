using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Space_Invaders
{
    class GraphicalObject : ScreenPosition
    {
        //An object that is only a piece of graphics and a location.

        protected Texture2D texture;
        public static byte sizeMultiplier = 1;

        public int timer = 0;

        public static Main main;

        string textureName;

        //Width and height to get the width and height of the graphics.
        public int width
        {
            get
            {
                if (texture != null)
                {
                    return texture.Width * sizeMultiplier;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int height
        {
            get
            {
                if (texture != null)
                {
                    return texture.Height * sizeMultiplier;
                }
                else
                {
                    return 0;
                }
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

        public GraphicalObject(string _texture, Vector2 _position) : base (_position)
        {
            textureName = _texture;
            Load(textureName);
        }

        //Sending a string to Load will result in it loading the graphical bit with that name in the folder Graphics.
        public void Load(string route)
        {
            if (route != null)
            {
                texture = content.Load<Texture2D>("Graphics/" + route);
            }
        }

        //The rectangle which will be used for collisions everywhere.
        public virtual Rectangle Box()
        {
            return new Rectangle((int)X, (int)Y, width, height);
        }

        //If there is texture, draw.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, Box(), Color.White);
            }
        }
    }
}
