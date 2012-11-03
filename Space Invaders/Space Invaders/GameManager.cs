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
    class GameManager
    {
        Player player;

        ContentManager content;

        List<Enemy> invaders = new List<Enemy>();
        List<EnemyPosition> invaderPosition = new List<EnemyPosition>();
        List<GraphicalObject> lifeMarkers = new List<GraphicalObject>();
        List<BlockFormation> blockFormations = new List<BlockFormation>();
        List<Laser> lasers = new List<Laser>();
        List<GraphicalObject> invaderDestroyed = new List<GraphicalObject>();

        Texture2D destroyedTexture;

        UFO UFO;

        int width;
        int height;

        Random rand = new Random();
        int shootNext = 0;
        int shootTimer = 0;

        int lives = 2;

        public bool shift = false;

        TextLine pointText;

        int overallDifficulty = 0;
        int levelDifficulty = 0;

        public GameManager(int _width, int _height, ContentManager content)
        {
            width = _width;
            height = _height;

            LoadContent(content);
        }

        private void LoadContent(ContentManager content)
        {
            this.content = content;

            //Insert the graphics into the static variables.
            Laser.playerLaser = content.Load<Texture2D>("Graphics/PlayerLaser");
            Laser.enemyLaser = content.Load<Texture2D>("Graphics/Laser");
            destroyedTexture = content.Load<Texture2D>("Graphics/Destroyed");

            Player.SetDeath = content.Load<Texture2D>("Graphics/PlayerDestroyed");

            UFO = new UFO("UFO", new Vector2(-50, 10), width);
            UFO.Reset();
        }

        //Reset everything for a new round of Space Invaders.
        public void Reset()
        {
            Main.currentScore = 0;
            overallDifficulty = 0;
            levelDifficulty = 0;

            invaders = new List<Enemy>();
            invaderPosition = new List<EnemyPosition>();
            lasers = new List<Laser>();
            invaderDestroyed = new List<GraphicalObject>();
            blockFormations = new List<BlockFormation>();
            lifeMarkers = new List<GraphicalObject>();
            
            player = new Player("Player", new Vector2(width / 2, height - 40));
            player.GetLifeTexture();

            EnemyPosition.overallPosition = new Vector2(0, 0);

            pointText = new TextLine("Font", Main.currentScore.ToString(), Color.White, new Vector2(10, 10));

            shootNext = rand.Next(800, 2000);
            lifeMarkers.Add(new GraphicalObject("Player", new Vector2(width, 0)));
            lifeMarkers.Add(new GraphicalObject("Player", new Vector2(width, 0)));

            int blockPoint = width / 5;

            for (int i = 0; i < 4; i++)
            {
                blockFormations.Add(new BlockFormation());
                blockFormations[i].LoadContent(content, blockPoint + blockPoint * i, height);
            }

            UFO.Reset();

            NewWave(content);
        }

        //Update everything.
        public void Update(GameTime gameTime)
        {
            //Update positions and score.
            player.Update(gameTime);
            pointText.GetText = Main.currentScore.ToString();

            //Timer for when enemy shoots next. If over the time, a randomly chosen enemy among those still living will fire.
            shootTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (shootTimer >= shootNext) 
            {
                shootTimer = 0;
                shootNext = rand.Next(800, 2000);
                int enemy = rand.Next(0, invaders.Count);
                lasers.Add(new Laser("Laser", new Vector2(invaders[enemy].X + invaders[enemy].width / 2 - 1, invaders[enemy].Y + 10), false));
            }

            //If the player presses space while there is no shot in play and he isn't hit at the moment, the player fires a laser.
            if (player.Space == true && player.Fired == false && player.GetHit == false) 
            {
                player.Fired = true;
                lasers.Add(new Laser(null, new Vector2(player.X + player.width / 2 - 1, player.Y - 10), true));
            }

            //Update all lasers.
            foreach (var shot in lasers)
            {
                shot.Update(gameTime);
            }

            //Update lives.
            for (int i = 0; i < lifeMarkers.Count; i++)
            {
                lifeMarkers[i].X = width - lifeMarkers[i].width * (1 + i);
            }

            //Update the enemy positions. Shift animation while doing so. 
            foreach (Enemy invader in invaders)
            {
                if (invaderPosition[invader.Number].Update(gameTime, levelDifficulty))
                {
                    invader.animation++;
                    if (invader.animation > 1)
                    {
                        invader.animation = 0;
                    }
                    //If an enemy hits the side of the course, shift which side all enemies are heading towards. But start with a boolean.
                    if (invader.EnemyUpdate(gameTime, invaderPosition[invader.Number]) || EnemyPosition.move.Y == 16)
                    {
                        shift = true;
                    }
                }
            }

            //If shifting side, first check if it is when hitting the wall or not. If hit the wall, go downwards. If not, start going in the opposite direction they headed in earlier.
            if (shift == true)
            {
                shift = false;
                if (Enemy.hitWall == false) {
                    Enemy.hitWall = true;
                } 
                else 
                {
                    Enemy.hitWall = false;
                }
                if (Enemy.hitWall == true)
                {
                    EnemyPosition.move = new Vector2(0, 16);
                }
                else if (Enemy.moveRight == true && EnemyPosition.move.Y == 16)
                {
                    EnemyPosition.move = new Vector2(-16, 0);
                    Enemy.moveRight = false;
                    levelDifficulty += 2;
                }
                else if (Enemy.moveRight == false && EnemyPosition.move.Y == 16)
                {
                    EnemyPosition.move = new Vector2(16, 0);
                    Enemy.moveRight = true;
                    levelDifficulty += 2;
                }
            }

            //Checking laser collisions.
            for (int i = 0; i < lasers.Count; i++)
            {
                //Boolean for the case when the laser has already been removed.
                bool removed = false;

                //If hit invader.
                for (int j = 0; j < invaders.Count; j++)
                {
                    //If the laser if your own (friendly), remove invader, add its score to the main score and place destruction graphical object in the invader's place.
                    if (lasers[i].Box().Intersects(invaders[j].Box()) && lasers[i].GetFriendly == true) 
                    {
                        lasers.Remove(lasers[i]);
                        i--;
                        removed = true;
                        player.Fired = false;
                        Main.km.PressedSpace = false;
                        Main.currentScore += invaders[j].GetPoints;
                        invaderDestroyed.Add(new GraphicalObject("Destroyed", new Vector2(invaders[j].X + invaders[j].frameWidth / 2 - destroyedTexture.Width / 2, invaders[j].Y)));
                        invaders.Remove(invaders[j]);
                        j--;
                        break;
                    }
                }
                if (removed == false)
                {
                    //If an unfriendly laser hits player. Remove laser, subtract a life-marker and set the player's hit-boolean to true.
                    if (lasers[i].Box().Intersects(player.Box()) && lasers[i].GetFriendly == false && player.GetHit == false)
                    {
                        lasers.Remove(lasers[i]);
                        i--;
                        removed = true;
                        lifeMarkers.Remove(lifeMarkers[lifeMarkers.Count - 1]);
                        player.GetHit = true;
                        break;
                    }
                }
                if (removed == false)
                {
                    //If hit UFO, exactly the same as if hit an invader.
                    if (lasers[i].Box().Intersects(UFO.Box()))
                    {
                        lasers.Remove(lasers[i]);
                        i--;
                        removed = true;
                        player.Fired = false;
                        Main.km.PressedSpace = false;
                        Main.currentScore += UFO.GetPoints;
                        invaderDestroyed.Add(new GraphicalObject("Destroyed", new Vector2(UFO.X + UFO.width / 2 - destroyedTexture.Width / 2, UFO.Y)));
                        UFO.Reset();
                    }
                }
                if (removed == false)
                {
                    //If laser hits a block of defence, remove the laser and subtract one point of life from it.
                    for (int j = 0; j < blockFormations.Count; j++)
                    {
                        if (blockFormations[j].CollisionCheck(lasers[i]))
                        {
                            if (lasers[i].GetFriendly == true)
                            {
                                player.Fired = false;
                                Main.km.PressedSpace = false;
                                removed = true;
                            }
                            lasers.Remove(lasers[i]);
                            i--;
                            removed = true;
                            break;
                        }
                    }
                }
                if (removed == false)
                {
                    //If the laser flies out of the course, remove it.
                    if (lasers[i].Y < 0 - lasers[i].height || lasers[i].Y > height)
                    {
                        if (lasers[i].GetFriendly == true)
                        {
                            player.Fired = false;
                            Main.km.PressedSpace = false;
                        }
                        lasers.Remove(lasers[i]);
                        i--;
                    }
                }
            }

            //Update the timer on the destroyed enemy. After 300 miliseconds, remove the graphic.
            for (int i = 0; i < invaderDestroyed.Count; i++)
            {
                invaderDestroyed[i].timer += gameTime.ElapsedGameTime.Milliseconds;
                if (invaderDestroyed[i].timer > 300)
                {
                    invaderDestroyed.Remove(invaderDestroyed[i]);
                    i--;
                }
            }

            UFO.Update(gameTime);
            
            //When there is no enemies left, remove all positions (because we'll be creating new ones) and start the new wave.
            if (invaders.Count == 0) 
            {
                for (int i = 0; i < invaderPosition.Count; i++)
                {
                    invaderPosition.Remove(invaderPosition[i]);
                    i--;
                }
                NewWave(content);
            }
        }

        //To check for the end of the game. Check if the enemy is further down than the player of if the life markers are all gone. If so, return true.
        public bool CheckEnd ()
        {
            foreach (var invader in invaders)
	        {
                if (invader.Y > player.Y)
                {
                    return true;
                }
	        }
            if (lifeMarkers.Count == 0 && player.GetHit == false)
            {
                return true;
            }
            return false;
        }

        //When calling a new wave. Player gains a life, new invader positions are added.
        protected void NewWave(ContentManager content)
        {
            byte current = 0;
            lives++;
            lifeMarkers.Add(new GraphicalObject("Player", new Vector2(width, 0)));

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    invaderPosition.Add(new EnemyPosition());
                }
            }

            Enemy.moveRight = true;
            EnemyPosition.move = new Vector2(16, 0);

            //Create new invaders. Two loops, one on the vertical and one on the horisontal.
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 11; i++)
                {
                    invaders.Add(new Enemy(null, Vector2.Zero, 0, current));
                    invaderPosition[current].x = (1 + i) * 32 * GraphicalObject.sizeMultiplier;
                    invaderPosition[current].y = 30 + (float)((height / 3 * 2) * Math.Sqrt(0.01 * overallDifficulty)) + (1 + j) * 25 * GraphicalObject.sizeMultiplier;

                    //Depending on what row, they get different graphics and points to provide the player.
                    if (j == 0)
                    {
                        invaders[current].graphic = content.Load<Texture2D>("Graphics/Invader3");
                        invaders[current].GetPoints = 40;
                    }
                    else if (j == 1 || j == 2)
                    {
                        invaders[current].graphic = content.Load<Texture2D>("Graphics/Invader2");
                        invaders[current].GetPoints = 20;
                    }
                    else
                    {
                        invaders[current].graphic = content.Load<Texture2D>("Graphics/Invader1");
                        invaders[current].GetPoints = 10;
                    }

                    invaders[current].X = invaderPosition[current].x - invaders[current].frameWidth / 2;
                    invaders[current].Y = invaderPosition[current].y;

                    current++;
                }
            }
            //If it isn't the first time calling, get 1000 points for destroyed wave. Increase difficulty.
            if (overallDifficulty != 0)
            {
                Main.currentScore += 1000;
            }
            overallDifficulty += 2;
            levelDifficulty = overallDifficulty * 2;
        }

        //Draw everything.
        public void Draw(SpriteBatch sprite)
        {
            player.Draw(sprite);
            pointText.Draw(sprite);
            foreach (var shot in lasers)
            {
                shot.Draw(sprite);
            }

            foreach (var invader in invaderDestroyed)
            {
                invader.Draw(sprite);
            }
            foreach (Enemy invader in invaders)
            {
                invader.Draw(sprite);
            }
            foreach (var marker in lifeMarkers)
            {
                marker.Draw(sprite);
            }
            foreach (var blockFormation in blockFormations)
            {
                blockFormation.Draw(sprite);
            }
            UFO.Draw(sprite);
        }
    }
}
