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

        HighScoreClass highScore;

        public static KeyBoardManager km = new KeyBoardManager();

        GameManager gameManager;
        MenuManager menuManager;
        ScoreManager scoreManager;

        //SpriteBatch spriteBatch;
        SpriteFont font;
        string typedText = "";

        public static int currentScore = 0;

        enum GameState
        {
            Menu,
            Game,
            End
        }

        public enum MenuButtons
        {
            Nothing,
            PlayGame,
            CheckScore,
            EndGame
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
            ScreenPosition.content = Content;
            this.Window.Title = "Space Invaders - Niklas Cullberg";
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

            highScore = new HighScoreClass(Content);

            highScore.LoadScores();
            highScore.SetScores();

            gameManager = new GameManager(width, height, Content);
            menuManager = new MenuManager(width, height, Content);
            scoreManager = new ScoreManager(highScore, Content);

            //highScore.NewScore(100000, "Someone");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            km.Update();
            switch (gameState)
            {
                case GameState.Game:
                    {
                        gameManager.Update(gameTime);

                        if (gameManager.CheckEnd())
                        {
                            gameState = GameState.End;
                        }
                        break;
                    }
                case GameState.Menu:
                    {
                        MenuButtons menuButtons = MenuButtons.Nothing;

                        //(int buttonPressed = menuManager.KeyCheck(this);

                        menuButtons = menuManager.KeyCheck();

                        menuManager.Update(gameTime);

                        switch (menuButtons)
                        {
                            case MenuButtons.Nothing:
                                break;

                            case MenuButtons.CheckScore:
                                gameState = GameState.End;
                                break;

                            case MenuButtons.PlayGame:
                                scoreManager.inputName = true;
                                gameState = GameState.Game;
                                gameManager.Reset();
                                break;

                            case MenuButtons.EndGame:
                                this.Exit();
                                break;
                        }
                        break;
                    }
                case GameState.End:
                    {
                        scoreManager.Update(gameTime);

                        MenuButtons scoreButtons = MenuButtons.Nothing;

                        scoreButtons = scoreManager.KeyCheck();

                        switch (scoreButtons)
                        {
                            case MenuButtons.Nothing:
                                break;
                            case MenuButtons.CheckScore:
                                gameState = GameState.Menu;
                                break;
                        }
                        
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
                        scoreManager.Draw(spriteBatch);
                        break;
                    }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
