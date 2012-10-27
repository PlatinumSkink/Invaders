using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class TextLine : GraphicalObject
    {
        string text;
        SpriteFont font;
        Color color;

        public string GetText
        {
            get { return text; }
            set { text = value; }
        }

        public TextLine(SpriteFont _font, string _text, Color _color, Vector2 _position)
            : base(null, _position)
        {
            text = _text;
            font = _font;
            color = _color;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color);
        }
    }
}
