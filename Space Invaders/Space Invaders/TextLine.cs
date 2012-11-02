using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class TextLine : ScreenPosition
    {
        string text;
        SpriteFont font;
        string fontName;
        Color color;

        public string GetText
        {
            get { return text; }
            set { text = value; }
        }

        public TextLine(string _font, string _text, Color _color, Vector2 _position)
            : base(_position)
        {
            text = _text;
            fontName = _font;
            LoadFont(fontName);
            
            color = _color;
        }
        public void LoadFont(string _font)
        {
            font = content.Load<SpriteFont>("Graphics/" + _font);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color);
        }
    }
}
