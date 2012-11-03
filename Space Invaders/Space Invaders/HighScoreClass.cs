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
using System.IO;

namespace Space_Invaders
{
    class HighScoreClass
    {
        //Class which contains my High Scores.

        List<int> highScorePoints = new List<int>();
        List<string> highScoreNames = new List<string>();

        ContentManager content;

        public HighScoreClass(ContentManager _content)
        {
            content = _content;
        }

        //Get scores from the High Score Class.
        public int GetScore(int i)
        {
            return highScorePoints[i];
        }
        public string GetName(int i)
        {
            return highScoreNames[i];
        }

        //Load the scores from the text document. The numbers are added to int Points, while the names become the names. If there is no file, create one and try again.
        public void LoadScores()
        {
            try
            {
                FileStream fs = new FileStream(content.RootDirectory + "\\HighScore.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                for (int i = 0; i < 10; i++)
                {
                    highScorePoints.Add(int.Parse(sr.ReadLine()));
                    highScoreNames.Add(sr.ReadLine());
                }
                sr.Close();
                fs.Close();
            }
            catch
            {
                FileStream fw = new FileStream(content.RootDirectory + "\\HighScore.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fw);
                sw.WriteLine("1000");
                sw.WriteLine("Strawberry");
                sw.WriteLine("900");
                sw.WriteLine("Peach");
                sw.WriteLine("800");
                sw.WriteLine("Blackberry");
                sw.WriteLine("700");
                sw.WriteLine("Orange");
                sw.WriteLine("600");
                sw.WriteLine("Banana");
                sw.WriteLine("500");
                sw.WriteLine("Apple");
                sw.WriteLine("400");
                sw.WriteLine("Blueberry");
                sw.WriteLine("300");
                sw.WriteLine("Grapefruit");
                sw.WriteLine("200");
                sw.WriteLine("Pineapple");
                sw.WriteLine("100");
                sw.WriteLine("Acorn");
                sw.Close();
                fw.Close();
                LoadScores();
            }
        }

        //Send in the current scores into the text-file.
        public void SetScores()
        {
            FileStream fw = new FileStream(content.RootDirectory + "\\HighScore.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fw);
            for (int i = 0; i < 10; i++)
            {
                sw.WriteLine(highScorePoints[i]);
                sw.WriteLine(highScoreNames[i]);
            }
            sw.Close();
            fw.Close();
        }

        //Insert a new score. The variable "placed" is turned on when it finds that this score is higher than the one in that place, and all the rest are pushed downwards with aid of "remember" variables.
        public void NewScore(int score, string name)
        {
            bool placed = false;
            int rememberScore = 0;
            string rememberName = "";
            for (int i = 0; i < 10; i++)
            {
                if (placed == false)
                {
                    if (score > highScorePoints[i])
                    {
                        placed = true;
                    }
                }
                if (placed == true)
                {
                    rememberName = highScoreNames[i];
                    rememberScore = highScorePoints[i];
                    highScoreNames[i] = name;
                    highScorePoints[i] = score;
                    name = rememberName;
                    score = rememberScore;
                }
            }
        }

        //I don't remember what this was for.
        public void Push(int score, string name)
        {

        }
    }
}
