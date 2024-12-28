using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    namespace Platformer_GroupAssignment
    {


        public class GameManager
        {
            bool _gameEnded = false;
            private SoundEffect _winningSound;
            private SoundEffect _losingSound;
            private Penguin _penguin;
            private Flag _flag;
            private Map _map;
            private SpriteFont _font;
            private SpriteFont _font2;
            public static GameState gameState;
            private readonly float _bgSpeed = 200f;
            public float BgMovement { get; set; }
            private readonly BackgroundManager _bgm = new();

            public GameManager()
            {
                InitializeGame();
                _winningSound = Globals.Content.Load<SoundEffect>("SoundEffects/Winning");
                _losingSound = Globals.Content.Load<SoundEffect>("SoundEffects/game-over");
                _font = Globals.Content.Load<SpriteFont>("Fonts/gameOverFont");
                _font2 = Globals.Content.Load<SpriteFont>("Fonts/gameInfo2");
                _bgm.AddLayer(new(Globals.Content.Load<Texture2D>("Images/Layer0"), 0.0f, 0.0f));
                _bgm.AddLayer(new(Globals.Content.Load<Texture2D>("Images/Layer1"), 0.1f, 0.2f));
                _bgm.AddLayer(new(Globals.Content.Load<Texture2D>("Images/Layer2"), 0.2f, 0.5f));
                _bgm.AddLayer(new(Globals.Content.Load<Texture2D>("Images/Layer4"), 0.4f, 0.2f, -100.0f));
            }

            public void InitializeGame()
            {
                _map = new Map();
                _penguin = new Penguin(Globals.Content.Load<Texture2D>("Images/Penguin"), new Vector2(200, 200));
                _flag = new Flag(Globals.Content.Load<Texture2D>("Images/Red-Flag"), new Vector2(1536, 128), 0.83f);
                gameState = GameState.Playing;
                _gameEnded = false;
            }

            public void Update()
            {
                if (gameState == GameState.Playing)
                {
                    _penguin.Update();

                    if (_penguin.position.Y > Globals.WindowSize.Y)
                    {
                        gameState = GameState.GameOver;
                    }

                    if (_flag.CheckCollision(_penguin.Bounds))
                    {
                        gameState = GameState.Win;
                    }

                    // Update parallax layers
                    KeyboardState ks = Keyboard.GetState();
                    BgMovement = 0;

                    if (ks.IsKeyDown(Keys.D))
                    {
                        BgMovement = -_bgSpeed;
                    }
                    else if (ks.IsKeyDown(Keys.A))
                    {
                        BgMovement = _bgSpeed;
                    }

                    _bgm.Update(BgMovement);
                }
            }

            public void Draw()
            {
                Color backgroundColor = Color.CornflowerBlue; // Default background color

                if (gameState == GameState.Playing)
                {
                    _bgm.Draw();
                    _map.Draw();
                    _penguin.Draw();
                    _flag.Draw();
                }
                else if (gameState == GameState.GameOver)
                {
                    Globals.SpriteBatch.DrawString(_font, "Game Over!", new Vector2(Globals.WindowSize.X / 3, 350), Color.White);
                    Globals.SpriteBatch.DrawString(_font2, "Press R to restart or Q to Exist", new Vector2((Globals.WindowSize.X / 3) + 10, 500), Color.White);

                    backgroundColor = Color.DarkRed;
                    if (!_gameEnded)
                    {
                        _losingSound.Play();
                        _gameEnded = true;
                    }
                }
                else if (gameState == GameState.Win)
                {
                    Globals.SpriteBatch.DrawString(_font, "You Win! ", new Vector2((Globals.WindowSize.X / 3) + 20, 350), Color.White);
                    Globals.SpriteBatch.DrawString(_font2, "Press R to restart or Q to exist", new Vector2((Globals.WindowSize.X / 3), 500), Color.White);
                    backgroundColor = Color.DarkGreen;
                    if (!_gameEnded)
                    {
                        _winningSound.Play();
                        _gameEnded = true;
                    }
                }
                Globals.SpriteBatch.GraphicsDevice.Clear(backgroundColor);
            }
        }
    }

