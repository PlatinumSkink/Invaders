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
        //Class for the laser that is fired. A graphical object that simply goes up and down with an animation.

        public static Texture2D playerLaser;
        public static Texture2D enemyLaser;

        bool friendly;

        int animation = 9;

        int frameWidth;

        public int speed = 8;

        //Begin with loading if it is friendly laser or not. Only friendly can hurt enemies, and only unfriendly can hurt the player. Friendly move up, unfriendly move down. Also, they have different texture.
        public Laser(string _texture, Vector2 _position, bool _friendly)
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
                texture = enemyLaser;
                frameWidth = texture.Width / 10;
            }
        }

        public bool GetFriendly
        {
            get { return friendly; }
            private set { friendly = value; }
        }

        //Update, that is, move. Go through the animation of the unfriendly laser.
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

        //The special box to allow animation with the laser.
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

        //Source rectangle för var i animationen man är.
        public Rectangle EnemyBox()
        {
            return new Rectangle((int)animation * frameWidth, (int)0, frameWidth, height);
        }

        //Draw med animation. Vanlig rectangle och source rectangle.
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Box(), EnemyBox(), Color.White);
        }
    }
}
