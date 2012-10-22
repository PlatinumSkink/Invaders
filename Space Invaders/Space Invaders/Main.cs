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

        GameManager gameManager;
        MenuManager menuManager;

        enum GameState
        {
            Menu,
            Game,
            End
        }

        GameState gameState = GameState.Menu;

        public int width;
        public int height;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 400;
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

            gameManager = new GameManager(width, height, Content);
            menuManager = new MenuManager(width, height, Content);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            switch (gameState)
            {
                case GameState.Game:
                    {
                        gameManager.Update(gameTime);
                        break;
                    }
                case GameState.Menu:
                    {
                        menuManager.Update(gameTime);
                        break;
                    }
                case GameState.End:
                    {
                        break;
                    }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Game:
                    {
                        gameManager.Draw(spriteBatch);
                        break;
                    }
                case GameState.Menu:
                    {
                        menuManager.Draw(spriteBatch);
                        break;
                    }
                case GameState.End:
                    {
                        break;
                    }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
