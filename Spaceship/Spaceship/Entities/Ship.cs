using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceship.Entities
{
    class Ship : Entity
    {

        

        private int Height
        {
            get { return _height; }
        }
        private int Width
        {
            get { return _width; }
        }


        public Ship(Texture2D texture, Vector2 velocity, float mass = 10, int height = 0, int width = 0)
            : base(texture, velocity, mass, height, width)
        {

        }

        public void Update(Vector2 mousePosition)
        {
            Vector2 direction;
            direction.X = mousePosition.X - (Location.X + Width / 2f);
            direction.Y = mousePosition.Y - (Location.Y + Height / 2f);
            
            Rotation = (float)Math.Atan2(direction.Y, direction.X);
            Update();
        }

        public new void Update()
        {
            var newLocation = CheckLocation(Location);
            if (this.Location == newLocation)
                base.Update();
            else
                this.Location = newLocation;


        }

        public new void Draw(SpriteBatch spriteBatch = null)
        {
            base.Draw(GameState.SpriteBatch);
        }

        private Vector2 CheckLocation(Vector2 location)
        {
            if (GameState.OutOfBounds(this))
            {
                var doubleLocation = new Vector2(location.X, location.Y);
                if (Left > GameState.Width)
                {
                    doubleLocation.X = 0 - Width;
                }
                if (Right < 0)
                {
                    doubleLocation.X = GameState.Width;
                }
                if (Top > GameState.Height)
                {
                    doubleLocation.Y = 0 - Height;
                }
                if (Bottom < 0)
                {
                    doubleLocation.Y = GameState.Height;
                }

                return doubleLocation;

            }
            return location;
        }

        private float CheckVelocity(float value)
        {
            var maxSpeed = MaxSpeed;

            if (value < 0)
            {
                maxSpeed *= -1;
                if (value < maxSpeed)
                    return maxSpeed;
                return value;
            }

            if (value > maxSpeed)
                return maxSpeed;
            return value;
        }
    }
}
