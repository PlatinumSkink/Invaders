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

        List<Laser> lasers = new List<Laser>();
        List<Block> blocks = new List<Block>();

        int width;
        int height;

        Random rand = new Random();
        int shootNext = 0;
        int shootTimer = 0;

        int lives = 2;

        public bool shift = false;

        public GameManager(int _width, int _height, ContentManager content)
        {
            width = _width;
            height = _height;

            LoadContent(content);
        }

        private void LoadContent(ContentManager content)
        {
            this.content = content;
            player = new Player(content.Load<Texture2D>("Graphics/Player"), new Vector2(width / 2, height - 40));
            Laser.playerLaser = content.Load<Texture2D>("Graphics/PlayerLaser");
            Laser.enemyLaser1 = content.Load<Texture2D>("Graphics/Laser");

            EnemyPosition.overallPosition = new Vector2(0, 0);

            shootNext = rand.Next(800, 2000);
            lifeMarkers.Add(new GraphicalObject(content.Load<Texture2D>("Graphics/Player"), new Vector2(width, 0)));
            lifeMarkers.Add(new GraphicalObject(content.Load<Texture2D>("Graphics/Player"), new Vector2(width, 0)));

            int blockPoint = width / 5;

            for (int i = 0; i < 4; i++)
            {
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint * i, height - 300), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint * i - blocks[0].height, height - 300), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint * i + blocks[0].height, height - 300), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint * i, height - 300 - blocks[0].height), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint * i - blocks[0].height, height - 300 + blocks[0].height), SpriteEffects.None));
                blocks.Add(new Block(content.Load<Texture2D>("Graphics/DefenseBlock"), new Vector2(blockPoint * i + blocks[0].height, height - 300 + blocks[0].height), SpriteEffects.None));
            }

            NewWave(content);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            shootTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (shootTimer >= shootNext) 
            {
                shootTimer = 0;
                shootNext = rand.Next(800, 2000);
                int enemy = rand.Next(0, invaders.Count);
                lasers.Add(new Laser(content.Load<Texture2D>("Graphics/Laser"), new Vector2(invaders[enemy].x + invaders[enemy].width / 2 - 1, invaders[enemy].y + 10), false));
            }
            if (player.Space == true && player.Fired == false) 
            {
                player.Fired = true;
                lasers.Add(new Laser(null, new Vector2(player.x + player.width / 2 - 1, player.y - 10), true));
            }
            foreach (var shot in lasers)
            {
                shot.Update(gameTime);
            }
            for (int i = 0; i < lifeMarkers.Count; i++)
            {
                lifeMarkers[i].x = width - lifeMarkers[i].width * (1 + i);
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
                        invaders.Remove(invaders[j]);
                        j--;
                        break;
                    }
                }
                if (removed == false)
                {
                    if (lasers[i].y < 0 - lasers[i].height || lasers[i].y > height)
                    {
                        if (lasers[i].GetFriendly == true)
                        {
                            player.Fired = false;
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

        protected void NewWave(ContentManager content)
        {
            byte current = 0;
            lives++;
            lifeMarkers.Add(new GraphicalObject(content.Load<Texture2D>("Graphics/Player"), new Vector2(width, 0)));

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
                    }
                    else if (j == 1 || j == 2)
                    {
                        invaders[current].graphic = content.Load<Texture2D>("Graphics/Invader2");
                    }
                    else
                    {
                        invaders[current].graphic = content.Load<Texture2D>("Graphics/Invader1");
                    }

                    invaders[current].x = invaderPosition[current].x - invaders[current].frameWidth / 2;
                    invaders[current].y = invaderPosition[current].y;

                    current++;
                }
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            player.Draw(sprite);
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
            foreach (var block in blocks)
            {
                block.Draw(sprite);
            }
        }
    }
}
