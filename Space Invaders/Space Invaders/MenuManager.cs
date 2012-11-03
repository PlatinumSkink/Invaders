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

        GraphicalObject title;

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

        //Load everything and set position.
        public void LoadContent(ContentManager content)
        {
            cursor = new GraphicalObject("Player", new Vector2(190, 0));

            play = new TextLine("Font", "Play Game", Color.White, new Vector2(240, 200));
            score = new TextLine("Font", "High Scores", Color.White, new Vector2(240, 250));
            end = new TextLine("Font", "End Game", Color.White, new Vector2(240, 300));

            title = new GraphicalObject("Title", new Vector2(0, 0));
            title.X = width / 2 - title.width / 2;
            title.Y = 25;
        }

        //If press buttun up or down, switch cursor's location to appropriate place.
        public void Update(GameTime gameTime)
        {
            cursor.Y = 200 + 50 * cursorNumber;
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

        //Upon pressing enter or space, check where the curson is located and return appropriate enum to send the player where he wants to go.
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

        //Draw everything in the menu.
        public void Draw(SpriteBatch sprite)
        {
            cursor.Draw(sprite);

            title.Draw(sprite);

            play.Draw(sprite);
            score.Draw(sprite);
            end.Draw(sprite);
        }
    }
}