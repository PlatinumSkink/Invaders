using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    class ScoreManager
    {
        //List<string> Scores;

        List<TextLine> places;
        List<TextLine> scores;
        List<TextLine> names;

        TextLine input;

        string name;
        bool twinkle = true;

        int twinkleTimer = 0;

        bool pressedAnything = false;

        HighScoreClass highScores;

        public bool inputName = false;

        int stayTimer = 0;

        int textSpaceY = 30;
        int textSpaceX = 50;

        //Add all text.
        public ScoreManager(HighScoreClass _highScores, ContentManager content)
        {
            places = new List<TextLine>();
            scores = new List<TextLine>();
            names = new List<TextLine>();
            highScores = _highScores;

            input = new TextLine("Font", "Input name: " + name + "|", Color.White, new Vector2(0, 0));

            places.Add(new TextLine("Font", "1st", Color.White, new Vector2(textSpaceX, textSpaceY * 1)));
            places.Add(new TextLine("Font", "2nd", Color.White, new Vector2(textSpaceX, textSpaceY * 2)));
            places.Add(new TextLine("Font", "3rd", Color.White, new Vector2(textSpaceX, textSpaceY * 3)));

            for (int i = 0; i < 10; i++)
            {
                if (i > 2)
                {
                    places.Add(new TextLine("Font", (i + 1).ToString() + "th", Color.White, new Vector2(50, textSpaceY + textSpaceY * i)));
                }
                scores.Add(new TextLine("Font", "", Color.White, new Vector2(textSpaceX * 3, textSpaceY + textSpaceY * i)));
                names.Add(new TextLine("Font", "", Color.White, new Vector2(textSpaceX * 6, textSpaceY + textSpaceY * i)));
            }

            //Get the score from the files.
            GetScore();
        }
        public void Load()
        {
            //No graphics here.
        }

        //Update everything.
        public void Update(GameTime gameTime)
        {
            if (inputName == true) 
            {
                //If you are inputting a name, blink the last "|" every 800 milliseconds.
                twinkleTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (twinkleTimer > 800)
                {
                    twinkleTimer = 0;
                    if (twinkle == false)
                    {
                        twinkle = true;
                    } 
                    else if (twinkle == true) 
                    {
                        twinkle = false;
                    }
                }
                if (twinkle == true)
                {
                    input.GetText = "Input name: " + name + "|";
                }
                else
                {
                    input.GetText = "Input name: " + name + " ";
                }
                
                //If pressed anything is true, yet you are not pressing anything, turn boolean false.
                if (Main.km.InputKey() == false && pressedAnything == true)
                {
                    pressedAnything = false;
                }

                //Makes this char something that would never be used. Hopefully.
                char last = '$';
                
                char remember = ' ';
                int lastNum = 0;
                bool destroy = false;

                //If pressed back, make up the entire name again but with one less char.
                if (Main.km.Key(Keys.Back))
                {
                    //input.GetText = "";
                    string rememberName = name;
                    name = "";
                    for (int i = 0; i < rememberName.Length - 1; i++)
                    {
                        name += rememberName[i];
                    }
                }

                //If pressed enter, send in the new score along with the inputed name. Reset score afterwards and get new scores.
                if (Main.km.Key(Keys.Enter)) 
                {
                    inputName = false;
                    highScores.NewScore(Main.currentScore, name);
                    name = "";
                    Main.currentScore = 0;
                    highScores.SetScores();
                    highScores.LoadScores();
                    GetScore();
                }

                //If pressed a button, input the character pressed into the string where you are writing your name.
                if (Main.km.InputKey()) {
                    KeyGrabber.InboundCharEvent += (inboundCharacter) =>
                    {
                        if (pressedAnything == false)
                        {
                            pressedAnything = true;

                            //Only append characters that exist in the spritefont.
                            if (inboundCharacter < 32)
                                return;

                            if (inboundCharacter > 126)
                                return;

                            name += inboundCharacter;

                            //In order to counter writing an infinite number of the same character which it was doing, this program here erases the new character if it is the same as the last one.
                            if (name != null)
                            {
                                for (int i = 0; i < name.Length; i++)
                                {
                                    if (name[i] == remember)
                                    {
                                        destroy = true;
                                    }
                                    if (destroy == true)
                                    {
                                        name.Remove(i);
                                        //i--;
                                        break;
                                    }
                                    else
                                    {
                                        remember = name[i];
                                    }
                                    lastNum = i;
                                }
                            }
                            
                            //In the end, insert the last pressed character into "last".
                            if (name != null)
                            {
                                last = name[lastNum];
                            }
                        }
                    };
                }
                //If it is anything but $, insert it into the string.
                if (last != '$') {
                    input.GetText += last;
                }
            }
            //keyinput
        }

        //Go through the two lists of scores and names, and add them on the texts on the screen.
        private void GetScore()
        {
            for (int i = 0; i < scores.Count; i++)
            {
                scores[i].GetText = highScores.GetScore(i).ToString() + " points";
                names[i].GetText = highScores.GetName(i);
            }
        }
        private void SetScore()
        {

        }

        //If pressed space or enter, return to the main menu.
        public Main.MenuButtons KeyCheck()
        {
            if ((Main.km.Key(Keys.Enter) || Main.km.Key(Keys.Space)) && inputName == false)
            {
                stayTimer = 0;
                return Main.MenuButtons.CheckScore;
            }
            return Main.MenuButtons.Nothing;
        }

        //Draw.
        public void Draw(SpriteBatch sprite)
        {
            foreach (var place in places)
            {
                place.Draw(sprite);
            }
            foreach (var score in scores)
            {
                score.Draw(sprite);
            }
            foreach (var name in names)
            {
                name.Draw(sprite);
            }
            if (inputName == true) 
            {
                input.Draw(sprite);
            }
        }
    }
}
