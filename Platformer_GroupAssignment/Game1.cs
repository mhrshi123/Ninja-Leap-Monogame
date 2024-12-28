/*
 Group: 6
 Members: Maharshi, Alby, Abia


 credits :
 sounds are taken from https://pixabay.com/
 Images are taken from https://opengameart.org/, https://opengameart.org/, https://pixabay.com/
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private GameManager _gameManager;
        private GameMenu _gameMenu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.WindowSize = new(Map.tiles.GetLength(1) * Map.TILE_SIZE, Map.tiles.GetLength(0) * Map.TILE_SIZE);
            _graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
            _graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
            _graphics.ApplyChanges();
            Globals.Content = Content;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.GraphicsDevice = GraphicsDevice;
            _gameManager = new GameManager();
            _gameMenu = new GameMenu();
            _gameMenu.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            // Restart the game if the game is over or won based on the key press
            if (Keyboard.GetState().IsKeyDown(Keys.R) && (GameManager.gameState == GameState.GameOver || GameManager.gameState == GameState.Win))
            {
                _gameManager.InitializeGame();
            }

            // Exit the game if the game is over or won based on the key press
            if (Keyboard.GetState().IsKeyDown(Keys.Q) && (GameManager.gameState == GameState.GameOver || GameManager.gameState == GameState.Win))
            {
                Exit();
            }


            switch (_gameMenu.GetCurrentState())
            {
                case GameState.Menu:
                case GameState.Information:
                    _gameMenu.Update();
                    break;
                case GameState.StartGame:
                    Globals.Update(gameTime);
                    _gameManager.Update();
                    break;
                case GameState.Quit:
                    Exit();
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);
            Globals.SpriteBatch.Begin();
            _gameMenu.Draw(Globals.SpriteBatch);
            if (_gameMenu.GetCurrentState() == GameState.StartGame)
            {
                _gameManager.Draw();
            }
            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

