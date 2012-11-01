using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    class MenuManager
    {
        TextLine play;
        TextLine score;
        TextLine end;

        Enemy invader1;
        Enemy invader2;
        Enemy invader3;
        GraphicalObject ufo;

        int width;
        int height;

        int cursorNumber = 0;

        GraphicalObject cursor;

        public MenuManager(int _width, int _height, ContentManager content)
        {
            width = _width;
            height = _height;

            LoadContent(content);
        }

        public void LoadContent(ContentManager content)
        {
            cursor = new GraphicalObject(content.Load<Texture2D>("Graphics/Player"), new Vector2(50, 0));

            play = new TextLine(content.Load<SpriteFont>("Graphics/SpriteFont1"), "Play Game", Color.White, new Vector2(100, 100));
            score = new TextLine(content.Load<SpriteFont>("Graphics/SpriteFont1"), "High Scores", Color.White, new Vector2(100, 150));
            end = new TextLine(content.Load<SpriteFont>("Graphics/SpriteFont1"), "End Game", Color.White, new Vector2(100, 200));

            invader1 = new Enemy(content.Load<Texture2D>("Graphics/Invader1"), new Vector2(300, 100), 10, 1);
            invader2 = new Enemy(content.Load<Texture2D>("Graphics/Invader2"), new Vector2(300, 150), 20, 1);
            invader3 = new Enemy(content.Load<Texture2D>("Graphics/Invader3"), new Vector2(300, 200), 40, 1);
            ufo = new GraphicalObject(content.Load<Texture2D>("Graphics/UFO"), new Vector2(300, 250));
        }

        public void Update(GameTime gameTime)
        {
            cursor.Y = 100 + 50 * cursorNumber;
            if (Main.km.Key(Keys.Down))
            {
                cursorNumber++;
                if (cursorNumber > 2) 
                {
                    cursorNumber = 0;
                }
            }
            if (Main.km.Key(Keys.Up))
            {
                cursorNumber--;
                if (cursorNumber < 0)
                {
                    cursorNumber = 2;
                }
            }
        }

        public Main.MenuButtons KeyCheck()
        {
            if (Main.km.Key(Keys.Enter) || Main.km.Key(Keys.Space))
            {
                if (cursorNumber == 0)
                {
                    return Main.MenuButtons.PlayGame;
                }
                else if (cursorNumber == 1)
                {
                    return Main.MenuButtons.CheckScore;
                }
                else if (cursorNumber == 2)
                {
                    return Main.MenuButtons.EndGame;
                }
            }
            return Main.MenuButtons.Nothing;
        }

        public void Draw(SpriteBatch sprite)
        {
            cursor.Draw(sprite);

            play.Draw(sprite);
            score.Draw(sprite);
            end.Draw(sprite);

            invader1.Draw(sprite);
            invader2.Draw(sprite);
            invader3.Draw(sprite);
            ufo.Draw(sprite);
        }
    }
}