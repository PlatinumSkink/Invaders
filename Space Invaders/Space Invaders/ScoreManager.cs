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
            GetScore();
        }
        public void Load()
        {

        }
        public void Update(GameTime gameTime)
        {
            if (inputName == true) 
            {
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
                //char key = Main.km.InputKey();
                if (Main.km.InputKey() == false && pressedAnything == true)
                {
                    pressedAnything = false;
                }
                char last = '$';
                
                char remember = ' ';
                int lastNum = 0;
                bool destroy = false;
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
                            //input.GetText = name;
                            if (name != null)
                            {
                                last = name[lastNum];
                            }
                        }
                    };
                }
                if (last != '$') {
                    input.GetText += last;
                }
            }
            if (stayTimer <= 2000) 
            {
                
                stayTimer += gameTime.ElapsedGameTime.Milliseconds;
                //Console.WriteLine(stayTimer);
            }
            //keyinput
        }
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

        public Main.MenuButtons KeyCheck()
        {
            KeyboardState ks = Keyboard.GetState();
            if ((Main.km.Key(Keys.Enter) || Main.km.Key(Keys.Space)) && inputName == false)
            {
                stayTimer = 0;
                return Main.MenuButtons.CheckScore;
            }
            return Main.MenuButtons.Nothing;
        }

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
