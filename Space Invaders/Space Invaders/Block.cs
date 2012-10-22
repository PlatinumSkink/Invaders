using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class Block : GraphicalObject
    {
        int life = 4;
        int frameWidth;
        Vector2 origin;

        SpriteEffects effect;

        public Block(Texture2D _texture, Vector2 _position, SpriteEffects _effect)
            : base(_texture, _position)
        {
            effect = _effect;
            frameWidth = _texture.Width / 4;
            origin = new Vector2(texture.Width / 8, texture.Height / 2);
        }
        public int GetLife
        {
            get { return life; }
            set { life = value; }
        }
        public override Rectangle Box()
        {
            return new Rectangle((int)x, (int)y, width / 4, height);
        }

        public Rectangle EnemyBox()
        {
            return new Rectangle((int)(4 - life) * frameWidth, (int)0, frameWidth, height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Box(), EnemyBox(), Color.White, 0f, origin, effect, 0f);
        }
    }
}