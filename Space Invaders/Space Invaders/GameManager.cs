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

        List<Enemy> invaders = new List<Enemy>();
        List<EnemyPosition> invaderPosition = new List<EnemyPosition>();

        List<Laser> lasers = new List<Laser>();

        int width;
        int height;

        public bool shift = false;

        public GameManager(int _width, int _height, ContentManager content)
        {
            width = _width;
            height = _height;

            LoadContent(content);
        }

        private void LoadContent(ContentManager content)
        {
            player = new Player(content.Load<Texture2D>("Graphics/Player"), new Vector2(width / 2, height - 40));
            Laser.playerLaser = content.Load<Texture2D>("Graphics/PlayerLaser");
            Laser.enemyLaser1 = content.Load<Texture2D>("Graphics/Laser");

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    invaderPosition.Add(new EnemyPosition());
                }
            }
            EnemyPosition.overallPosition = new Vector2(0, 0);

            NewWave(content);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            if (player.Space == true && player.Fired == false) 
            {
                lasers.Add(new Laser(null, new Vector2(player.x + player.width / 2 - 1, player.y - 10), true));
            }
            foreach (var shot in lasers)
            {
                shot.Update(gameTime);
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
                    if (invader.EnemyUpdate(gameTime, invaderPosition[invader.Number]))
                    {
                        shift = true;
                    }
                }
            }
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
                else if (Enemy.moveRight == true)
                {
                    EnemyPosition.move = new Vector2(-16, 0);
                    Enemy.moveRight = false;
                }
                else if (Enemy.moveRight == false)
                {
                    EnemyPosition.move = new Vector2(16, 0);
                    Enemy.moveRight = true;
                }
            }
            for (int i = 0; i < lasers.Count; i++)
            {
                for (int j = 0; j < invaders.Count; j++)
                {
                    if (lasers[i].Box().Intersects(invaders[j].Box())) 
                    {
                        //lasers.Remove(lasers[i]);
                        //i--;
                        invaders.Remove(invaders[j]);
                        j--;
                    }
                }
                if (lasers[i].y < 0 - lasers[i].height) 
                {
                    lasers.Remove(lasers[i]);
                    i--;
                }
            }
        }

        protected void NewWave(ContentManager content)
        {
            byte current = 0;
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
        }
    }
}
