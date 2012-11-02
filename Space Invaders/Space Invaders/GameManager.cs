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
        

        UFO UFO;

        int width;
        int height;

        Random rand = new Random();
        int shootNext = 0;
        int shootTimer = 0;

        int lives = 2;

        public bool shift = false;

        TextLine pointText;

        public GameManager(int _width, int _height, ContentManager content)
        {
            width = _width;
            height = _height;

            LoadContent(content);
        }

        private void LoadContent(ContentManager content)
        {
            this.content = content;
            Laser.playerLaser = content.Load<Texture2D>("Graphics/PlayerLaser");
            Laser.enemyLaser1 = content.Load<Texture2D>("Graphics/Laser");

            Player.SetDeath = content.Load<Texture2D>("Graphics/PlayerDestroyed");

            UFO = new UFO("UFO", new Vector2(-50, 50));
        }

        public void Reset()
        {
            Main.currentScore = 0;
            /*for (int i = 0; i < invaders.Count; i++)
            {
                invaders.Remove(invaders[i]);
                i--;
            }*/

            invaders = new List<Enemy>();

            /*for (int i = 0; i < invaderPosition.Count; i++)
            {
                invaderPosition.Remove(invaderPosition[i]);
                i--;
            }*/

            invaderPosition = new List<EnemyPosition>();
            /*for () 
            {

            }*/

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
                blockFormations[i].LoadContent(content, blockPoint, i, height);
            }

            /*    blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(-100, -100), SpriteEffects.None));

            for (int i = 0; i < 4; i++)
            {
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseInnerCornerRight"), new Vector2(blockPoint + blockPoint * i - blocks[0].height / 2, height - 100), SpriteEffects.FlipHorizontally));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseInnerCornerRight"), new Vector2(blockPoint + blockPoint * i + blocks[0].height / 2, height - 100), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint + blockPoint * i - blocks[0].height - blocks[0].height / 2, height - 100), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint + blockPoint * i + blocks[0].height + blocks[0].height / 2, height - 100), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint + blockPoint * i - blocks[0].height / 2, height - 100 - blocks[0].height), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint + blockPoint * i + blocks[0].height / 2, height - 100 - blocks[0].height), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint + blockPoint * i - blocks[0].height - blocks[0].height / 2, height - 100 + blocks[0].height), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint + blockPoint * i + blocks[0].height + blocks[0].height / 2, height - 100 + blocks[0].height), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenceOuterCornerLeft"), new Vector2(blockPoint + blockPoint * i - blocks[0].height - blocks[0].height / 2, height - 100 - blocks[0].height), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenceOuterCornerLeft"), new Vector2(blockPoint + blockPoint * i + blocks[0].height + blocks[0].height / 2, height - 100 - blocks[0].height), SpriteEffects.FlipHorizontally));
            }*/

            UFO.X = -50;

            NewWave(content);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            pointText.GetText = Main.currentScore.ToString();
            shootTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (shootTimer >= shootNext) 
            {
                shootTimer = 0;
                shootNext = rand.Next(800, 2000);
                int enemy = rand.Next(0, invaders.Count);
                lasers.Add(new Laser("Laser", new Vector2(invaders[enemy].X + invaders[enemy].width / 2 - 1, invaders[enemy].Y + 10), false));
            }
            if (player.Space == true && player.Fired == false && player.GetHit == false) 
            {
                player.Fired = true;
                lasers.Add(new Laser(null, new Vector2(player.X + player.width / 2 - 1, player.Y - 10), true));
            }
            foreach (var shot in lasers)
            {
                shot.Update(gameTime);
            }
            for (int i = 0; i < lifeMarkers.Count; i++)
            {
                lifeMarkers[i].X = width - lifeMarkers[i].width * (1 + i);
            }
            foreach (Enemy invader in invaders)
            {
                if (invaderPosition[invader.Number].Update(gameTime))
                {
                    invader.animation++;
                    if (invader.animation > 1)
                    {
                        invader.animation = 0;
                    }
                    if (invader.EnemyUpdate(gameTime, invaderPosition[invader.Number]) || EnemyPosition.move.Y == 16)
                    {
                        shift = true;
                    }
                }
            }
            /*if (EnemyPosition.move.Y == 16) 
            {
                EnemyPosition.move = new Vector2(16, 0);
            }*/

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
                }
                else if (Enemy.moveRight == false && EnemyPosition.move.Y == 16)
                {
                    EnemyPosition.move = new Vector2(16, 0);
                    Enemy.moveRight = true;
                }
            }
            for (int i = 0; i < lasers.Count; i++)
            {
                bool removed = false;
                for (int j = 0; j < invaders.Count; j++)
                {
                    if (lasers[i].Box().Intersects(invaders[j].Box()) && lasers[i].GetFriendly == true) 
                    {
                        lasers.Remove(lasers[i]);
                        i--;
                        removed = true;
                        player.Fired = false;
                        Main.km.PressedSpace = false;
                        Main.currentScore += invaders[j].GetPoints;
                        invaders.Remove(invaders[j]);
                        j--;
                        break;
                    }
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

            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 11; i++)
                {
                    invaders.Add(new Enemy(null, Vector2.Zero, 0, current));
                    invaderPosition[current].x = (1 + i) * 32 * GraphicalObject.sizeMultiplier;
                    invaderPosition[current].y = (1 + j) * 25 * GraphicalObject.sizeMultiplier;
                    /*invaders[current].x = (1 + i) * 25 * GraphicalObject.sizeMultiplier;
                    invaders[current].y = (1 + j) * 25 * GraphicalObject.sizeMultiplier;*/

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
        }

        public void Draw(SpriteBatch sprite)
        {
            player.Draw(sprite);
            pointText.Draw(sprite);
            foreach (var shot in lasers)
            {
                shot.Draw(sprite);
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
        }
    }
}
