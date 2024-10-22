using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Monogame_Topic_5___Making_a_Class
{
    public class Game1 : Game
    {
        //Christian Moyes
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Ghost ghost1;

        MouseState mouseState;
        KeyboardState keyboardState;

        Random genrator = new Random();

        Texture2D titleTexture;
        Texture2D backgroundTexture;
        Texture2D endTexture;
        Texture2D marioTexture;

        Rectangle windowRect;

        List<Texture2D> ghostTextures;

        enum Screen
        {
            Title,
            House,
            End
        }

        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Title;

            genrator = new Random();
            ghostTextures = new List<Texture2D>();

            windowRect = new Rectangle(0, 0, 800, 600);

            base.Initialize();

            ghost1 = new Ghost(ghostTextures, new Rectangle(150, 250, 40, 40));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            marioTexture = Content.Load<Texture2D>("Images/mario");
            titleTexture = Content.Load<Texture2D>("Images/haunted-title");
            backgroundTexture = Content.Load<Texture2D>("Images/haunted-background");
            endTexture = Content.Load<Texture2D>("Images/haunted-end-screen");

            ghostTextures.Add(Content.Load<Texture2D>("Images/boo-stopped"));

            for (int i = 1; i <= 8; i++)
            {
                ghostTextures.Add(Content.Load<Texture2D>($"Images/boo-move-{i}"));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.House;
                }
            }
            else if (screen == Screen.House)
            {
                ghost1.Update(gameTime, mouseState);

                if (ghost1.Contains(mouseState.Position))
                {
                    screen = Screen.End;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleTexture, windowRect, Color.White);
            }
            else if (screen == Screen.House)
            {
                _spriteBatch.Draw(backgroundTexture, windowRect, Color.White);
                ghost1.Draw(_spriteBatch);
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(endTexture, windowRect, Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
