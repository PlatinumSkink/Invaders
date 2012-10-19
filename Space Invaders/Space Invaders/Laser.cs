using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class Laser : GraphicalObject
    {
        public static Texture2D playerLaser;
        public static Texture2D enemyLaser1;
        public static Texture2D enemyLaser2;

        bool friendly;

        public int speed = 8;

        public Laser(Texture2D _texture, Vector2 _position, bool _friendly)
            : base(_texture, _position)
        {
            friendly = _friendly;
            if (friendly == true)
            {
                texture = playerLaser;
                speed = -8;
            }
            else
            {
                texture = enemyLaser1;
            }
        }

        public void Update(GameTime gameTime) 
        {
            y += speed;
        }
    }
}
