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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _loader = new TextureLoader(graphics.GraphicsDevice, false, "Content");
            ship = new Ship(_loader.FromFile("spaceship.png"), spriteBatch, new Vector2(20, 20), 5, 64, 64);
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
            Global.SetClock(gameTime);

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
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Global.SetClock(gameTime);

            // TODO: Add your drawing code here
            ship.Draw();
            base.Draw(gameTime);
        }
    }

    public static class Global
    {
        public static GameTime GameTime;

        public static int Height
        {
            get { return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; }
        }

        public static int Width
        {
            get { return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; }
        }

        public static void SetClock(GameTime gameTime)
        {
            GameTime = gameTime;
        }
    }
}
