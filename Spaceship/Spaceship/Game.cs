using System.Collections.Generic;
using System.Linq;
using ContentHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceship.Entities;

namespace Spaceship
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TextureLoader _loader;
        Ship ship;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = GameState.SpriteBatch = new SpriteBatch(GraphicsDevice);
            _loader = new TextureLoader(graphics.GraphicsDevice, false, "Content");
            ship = new Ship( _loader.FromFile("spaceship.png"), new Vector2(20, 20), 5, 64, 64);
            GameState.Entities = new List<Entity>();
            GameState.GraphicsDevice = graphics.GraphicsDevice;
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
            GameState.Initialize();
            GameState.SetClock(gameTime);
            
            
            var keys = Keyboard.GetState().GetPressedKeys();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keys.Contains(Keys.Escape))
                this.Exit();


            if (keys.Contains(Keys.Left))
            {
                ship.AddLeft();
            }
            if (keys.Contains(Keys.Right))
            {
                ship.AddRight();
            }
            if (keys.Contains(Keys.Up))
            {
                ship.AddUp();
            }
            if (keys.Contains(Keys.Down))
            {
                ship.AddDown();
            }

            // TODO: Add your update logic here
            ship.Update();
            GameState.Update();            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameState.SetClock(gameTime);
            ship.Draw();
            GameState.Draw();
            base.Draw(gameTime);
        }
    }

    public static class GameState
    {
        public static GraphicsDevice GraphicsDevice;
        public static GameTime GameTime;

        public static SpriteBatch SpriteBatch { get; set; }

        public static List<Entity> Entities;

        public static int Height
        {
            get { return GraphicsDevice.Viewport.Y; }
        }

        public static int Width
        {
            get { return GraphicsDevice.Viewport.X; }
        }


        public static void SetClock(GameTime gameTime)
        {
            GameTime = gameTime;
        }

        public static void Draw()
        {
            Entities.ForEach(e => e.Draw());
        }

        public static void Update()
        {
            Entities.ForEach(e => e.Update());
        }
        public static void Initialize()
        {
            Entities.Clear();
        }
    }
}
