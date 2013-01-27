using System;
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
        SpriteFont _font;
        private Dictionary<String, String> log;
        private int updateCount = 0;
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            ship = new Ship(_loader.FromFile("spaceship.png"), new Vector2(20, 20), 5, 64, 64)
                {
                    Location = new Vector2(50)
                };
            GameState.Entities = new List<Entity>();
            GameState.GraphicsDevice = graphics.GraphicsDevice;
            _font = Content.Load<SpriteFont>("Text");
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

            GameState.SetClock(gameTime);


            var keys = Keyboard.GetState().GetPressedKeys();
            var buttons = GamePad.GetState(0).ThumbSticks;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keys.Contains(Keys.Escape))
                this.Exit();


            if (buttons.Left.X < 0 || buttons.Left.X > 0)
            {
                ship.AddRight(buttons.Left.X);
            }
            if (keys.Contains(Keys.Left))
            {
                ship.AddLeft(1);
            }

            if (keys.Contains(Keys.Right))
            {
                ship.AddRight(1);
            }

            if (buttons.Left.Y > 0 || buttons.Left.Y < 0) { ship.AddUp(buttons.Left.Y); }
            if (keys.Contains(Keys.Up))
            {
                ship.AddUp(1);
            }
            if (keys.Contains(Keys.Down))
            {
                ship.AddDown(1);
            }


            var mouseState = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            if (!buttons.Right.Y.Equals(0) || !buttons.Right.X.Equals(0))
            {
                mouseState.X += buttons.Right.X;
                mouseState.Y += buttons.Right.Y;
            }

            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightShoulder))
            {
                if (ship.projectiles.Count() == 0)
                    ship.Shoot(_loader.FromFile("laser.png"), mouseState);
            }


            // TODO: Add your update logic here
            ship.UpdateGamepad(buttons.Right);
            GameState.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            updateCount++;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameState.SetClock(gameTime);
            spriteBatch.Begin();
            if (updateCount < 2 || updateCount % 10 == 0)
            {
                log = new Dictionary<String, String>()
                    {
                        {"Left", Math.Floor(ship.Left).ToString()},
                        {"Right", Math.Floor(ship.Right).ToString()},
                        {"Top", Math.Floor(ship.Top).ToString()},
                        {"Bottom", Math.Floor(ship.Bottom).ToString()},
                        {"Direction", ship.Direction.ToString()},
                        {"Power", ship.Power.ToString()},
                        {"Force", ship.Force.ToString()},
                        {"Mass", ship.Mass.ToString()},
                        {"Acceleration", ship.Acceleration.ToString()},
                        {"Velocity", ship.Velocity.ToString()},
                        {"Time", GameState.GameTime.ElapsedGameTime.TotalMilliseconds.ToString()},
                        {"Origin", ship.Origin.ToString()},
                        {"Rotation", ship.Rotation.ToString()},
                        {"Projected Rotation", ship.projectedRotation.ToString()}
                    };

            }
            spriteBatch.DrawString(_font, String.Join("\n", log.Select(k => String.Format("{0} = {1}, ", k.Key, k.Value))),
                                       new Vector2(100, 100), Color.Black);


            ship.Draw(spriteBatch);
            spriteBatch.End();
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
            get { return GraphicsDevice.Viewport.Height; }
        }

        public static int Width
        {
            get { return GraphicsDevice.Viewport.Width; }
        }

        public static Boolean OutOfBounds(Entity entity)
        {
            return entity.Right < 0 || entity.Bottom < 0 || entity.Left > Width || entity.Top > Height;
        }

        public static void SetClock(GameTime gameTime)
        {
            GameTime = gameTime;
        }

        public static void Draw()
        {
            SpriteBatch.Begin();
            Entities.ForEach(e => e.Draw(SpriteBatch));
            SpriteBatch.End();
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
