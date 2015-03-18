using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Classic_Race
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // To handle the different pages we have the page var
        private string _page = "main";

        private MouseState _mouseState;

        float _carPosX;
        SpriteBatch _spriteBatch;
        private ButtonState buttState;
        int _speed = 2;
        List<Vector2> _traffic = new List<Vector2>();
        bool _collison = false;
        bool _click = false;

        readonly PressHelpers _pressHelpers = new PressHelpers();
        readonly GameLogic _gameLogic = new GameLogic();
        
        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            //TargetElapsedTime = TimeSpan.FromTicks(111111);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            // Use the full screen resolution to make use of enhanced car details.
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = _gameLogic.GetScreenHeight();
            graphics.PreferredBackBufferWidth = _gameLogic.GetScreenWidth();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        Texture2D _car;
        Texture2D _sideLine;

        Vector2 _carPosition = new Vector2(105, 300);

        SpriteFont _font;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _car = Content.Load<Texture2D>("pixel_car");
            _sideLine = Content.Load<Texture2D>("side_line");
            _font = Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _mouseState = Mouse.GetState();
            _pressHelpers.InputHandler(_mouseState);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

        }
    }
}
