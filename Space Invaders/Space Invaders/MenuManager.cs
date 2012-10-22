using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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

        bool downPressed = false;
        bool upPressed = false;

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

            play = new TextLine("Play Game", content.Load<SpriteFont>("Graphics/SpriteFont1"), Color.White, new Vector2(100, 100));
            score = new TextLine("High Scores", content.Load<SpriteFont>("Graphics/SpriteFont1"), Color.White, new Vector2(100, 150));
            end = new TextLine("End Game", content.Load<SpriteFont>("Graphics/SpriteFont1"), Color.White, new Vector2(100, 200));

            invader1 = new Enemy(content.Load<Texture2D>("Graphics/Invader1"), new Vector2(300, 100), 10, 1);
            invader2 = new Enemy(content.Load<Texture2D>("Graphics/Invader2"), new Vector2(300, 150), 20, 1);
            invader3 = new Enemy(content.Load<Texture2D>("Graphics/Invader3"), new Vector2(300, 200), 40, 1);
            ufo = new GraphicalObject(content.Load<Texture2D>("Graphics/UFO"), new Vector2(300, 250));
        }

        public void Update(GameTime gameTime)
        {
            cursor.y = 100 + 50 * cursorNumber;
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && downPressed == false)
            {
                downPressed = true;
                cursorNumber++;
                if (cursorNumber > 2) 
                {
                    cursorNumber = 0;
                }
            }
            if (ks.IsKeyUp(Keys.Down))
            {
                downPressed = false;
            }
            if (ks.IsKeyDown(Keys.Up) && upPressed == false)
            {
                upPressed = true;
                cursorNumber--;
                if (cursorNumber < 0)
                {
                    cursorNumber = 2;
                }
            }
            if (ks.IsKeyUp(Keys.Up))
            {
                upPressed = false;
            }
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