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
                if (invader.EnemyUpdate(gameTime))
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
            for (int i = 0; i < 55; i++)
            {
                if (i < 11)
                {
                    invaders.Add(new Enemy(Content.Load<Texture2D>("Graphics/Invaders3"), Vector2.Zero, 40));
                }
                else if (10 < i && i < 33)
                {
                    invaders.Add(new Enemy(Content.Load<Texture2D>("Graphics/Invaders2"), Vector2.Zero, 20));
                }
                else if (32 < i)
                {
                    invaders.Add(new Enemy(Content.Load<Texture2D>("Graphics/Invaders1"), Vector2.Zero, 10));
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
