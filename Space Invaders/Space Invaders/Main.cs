using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Invaders
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;

        List<Enemy> invaders = new List<Enemy>();
        List<EnemyPosition> invaderPosition = new List<EnemyPosition>();

        public int width;
        public int height;

        public bool shift = false;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            width = graphics.GraphicsDevice.Viewport.Width;
            height = graphics.GraphicsDevice.Viewport.Height;

            GraphicalObject.main = this;

            player = new Player(Content.Load<Texture2D>("Graphics/Player"), new Vector2(width / 2, height - 40));

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    invaderPosition.Add(new EnemyPosition());
                }
            }
            EnemyPosition.overallPosition = new Vector2(0, 0);

            NewWave();
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            player.Update(gameTime);

            foreach (Enemy invader in invaders)
            {
                if (invader.EnemyUpdate(gameTime, invaderPosition[invader.Number]))
                {
                    shift = true;
                }
            }
            if (shift == true)
            {
                Enemy.hitWall = true;
            }

            base.Update(gameTime);
        }

        protected void NewWave()
        {
            byte current = 0;
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 11; i++)
                {
                    invaders.Add(new Enemy(null, Vector2.Zero, 0, current));
                    invaders[current].x = i * 50;
                    invaders[current].y = j * 50;

                    if (j == 0) 
                    {
                        invaders[current].graphic = Content.Load<Texture2D>("Graphics/Invader3");
                    }
                    else if (j == 1 || j == 2)
                    {
                        invaders[current].graphic = Content.Load<Texture2D>("Graphics/Invader2");
                    }
                    else
                    {
                        invaders[current].graphic = Content.Load<Texture2D>("Graphics/Invader1");
                    }
                    current++;
                    /*if (i < 11)
                    {
                        invaders.Add(new Enemy(Content.Load<Texture2D>("Graphics/Invader3"), Vector2.Zero, 40));
                        invaders[i].x = i * (invaders[i].width + 10);
                        invaders[i].y = invaders[i].height * 1;
                    }
                    else if (10 < i && i < 22)
                    {
                        invaders.Add(new Enemy(Content.Load<Texture2D>("Graphics/Invader2"), Vector2.Zero, 20));
                        invaders[i].x = (i - 11) * invaders[i].width;
                        invaders[i].y = invaders[i].height * 2;
                    }
                    else if (21 < i && i < 33)
                    {
                        invaders.Add(new Enemy(Content.Load<Texture2D>("Graphics/Invader2"), Vector2.Zero, 20));
                        invaders[i].x = (i - 22) * invaders[i].width;
                        invaders[i].y = invaders[i].height * 3;
                    }
                    else if (32 < i && i < 44)
                    {
                        invaders.Add(new Enemy(Content.Load<Texture2D>("Graphics/Invader1"), Vector2.Zero, 10));
                        invaders[i].x = (i - 33) * invaders[i].width;
                        invaders[i].y = invaders[i].height * 4;
                    }
                    else if (43 < i)
                    {
                        invaders.Add(new Enemy(Content.Load<Texture2D>("Graphics/Invader1"), Vector2.Zero, 10));
                        invaders[i].x = (i - 44) * invaders[i].width;
                        invaders[i].y = invaders[i].height * 5;
                    }*/
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            player.Draw(spriteBatch);

            foreach (Enemy invader in invaders)
            {
                invader.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
