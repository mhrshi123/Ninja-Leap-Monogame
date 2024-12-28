using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer_GroupAssignment
{
    public class GameMenu
    {
        private GameState currentState;
        private KeyboardState previousKeyboardState;
        private string[] menuOptions = { "Start Game", "Information", "Quit" };
        private int selectedOption = 0;
        private SpriteFont menuFont;
        private SpriteFont menuTitleFont;
        private Texture2D bgTexture;
        private Texture2D _gameMenuTexture;
        private SpriteFont gameInfoFont;
        private SpriteFont gameInfoFont2;
        public GameMenu()
        {
            currentState = GameState.Menu;
            previousKeyboardState = Keyboard.GetState();
        }

        // loads content for the menu
        public void LoadContent(ContentManager content)
        {
            menuFont = content.Load<SpriteFont>("Fonts/menuFont");
            menuTitleFont = content.Load<SpriteFont>("Fonts/mainMenuTitle");
            _gameMenuTexture = content.Load<Texture2D>("Images/gameMenu");
           bgTexture = content.Load<Texture2D>("Images/background");
            gameInfoFont = content.Load<SpriteFont>("Fonts/gameInfo");
            gameInfoFont2 = content.Load<SpriteFont>("Fonts/gameInfo2");
        }

        // updates the menu state
        public void Update()
        {
            var keyboardState = Keyboard.GetState();

            switch (currentState)
            {
                case GameState.Menu:
                    UpdateMenu(keyboardState);
                    break;
                case GameState.Information:
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        currentState = GameState.Menu;
                    }
                    break;
                case GameState.Quit:
                    break;
            }
            previousKeyboardState = keyboardState;
        }


        // updates the menu options based on user input
        private void UpdateMenu(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))
            {
                selectedOption = (selectedOption + 1) % menuOptions.Length;
            }
            if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
            {
                selectedOption = (selectedOption - 1 + menuOptions.Length) % menuOptions.Length;
            }

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                if (menuOptions[selectedOption] == "Start Game")
                {
                    currentState = GameState.StartGame;
                }
                else if (menuOptions[selectedOption] == "Information")
                {
                    currentState = GameState.Information;
                }
                else if (menuOptions[selectedOption] == "Quit")
                {
                    currentState = GameState.Quit;
                }
            }
        }

        //Method to call draw for respective states
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentState)
            {
                case GameState.Menu:
                    DrawMenu(spriteBatch);
                    break;
                case GameState.Information:
                    DrawInformation(spriteBatch);
                    break;
            }
        }


        //Method to draw the menu
        private void DrawMenu(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(750, 500);
            spriteBatch.Draw(_gameMenuTexture, new Rectangle(0, 0, 1700, 1000), Color.White);

            spriteBatch.DrawString(menuTitleFont, "Main Menu", new Vector2(700, 275), Color.White);

            for (int i = 0; i < menuOptions.Length; i++)
            {
                Color color = (i == selectedOption) ? Color.Green : Color.Blue;
                spriteBatch.DrawString(menuFont, menuOptions[i], position, color);
                position.Y += 70;
            }
        }

        //Method to draw the information screen
        private void DrawInformation(SpriteBatch spriteBatch)
        {


            spriteBatch.Draw(bgTexture, new Rectangle(0, 0, 1800, 1080), Color.White);

            spriteBatch.DrawString(menuTitleFont, "Penguin Adventure", new Vector2(610, 50), Color.White);

            spriteBatch.DrawString(gameInfoFont2, "How to play:", new Vector2(50, 380), Color.Black);


            spriteBatch.DrawString(gameInfoFont, "Welcome to Penguin Adventure, a fun and engaging platformer game! Your mission is simple yet thrilling: guide\nour brave penguin to the flag. Beware, if the penguin misses a jump and falls off the tiles, it's game over." +
                                                  "\r\n\r\n\n\n\n\n" +
                                                  
                                                   "Move Left: Press A" + "\r\n\r\n" +
                                                  "Move Right: Press D\r\n\r\n" +
                                                  "Jump: Press Space Bar\r\n\r\n\n\n" +
                                                  "Enjoy the journey and help our penguin reach its destination safely! Happy jumping!", new Vector2(50, 250), Color.Black);

            spriteBatch.DrawString(gameInfoFont2, "Press ESC to return to the main menu", new Vector2(50, 900), Color.Black);

        }


        //Method to get the current state
        public GameState GetCurrentState()
        {
            return currentState;
        }
    }
}
