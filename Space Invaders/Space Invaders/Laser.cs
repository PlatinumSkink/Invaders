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

        int animation = 9;

        int frameWidth;

        public int speed = 8;

        public Laser(Texture2D _texture, Vector2 _position, bool _friendly)
            : base(_texture, _position)
        {
            GetFriendly = _friendly;
            if (GetFriendly == true)
            {
                texture = playerLaser;
                speed = -8;
            }
            else
            {
                texture = enemyLaser1;
                frameWidth = texture.Width / 10;
            }
        }

        public bool GetFriendly
        {
            get { return friendly; }
            private set { friendly = value; }
        }

        public void Update(GameTime gameTime) 
        {
            Y += speed;
            if (friendly == false)
            {
                animation++;
                if (animation > 9)
                {
                    animation = 0;
                }
            }
        }

        public override Rectangle Box()
        {
            if (GetFriendly == true)
            {
                return base.Box();
            }
            else
            {
                return new Rectangle((int)X, (int)Y, width / 10, height);
            }
        }

        public Rectangle EnemyBox()
        {
            return new Rectangle((int)animation * frameWidth, (int)0, frameWidth, height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Box(), EnemyBox(), Color.White);
        }
    }
}
