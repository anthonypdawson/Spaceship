using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Spaceship.Entities
{
    class Ship : Entity
    {

        public List<Projectile> projectiles = new List<Projectile>();

        private int Height
        {
            get { return _height; }
        }
        private int Width
        {
            get { return _width; }
        }

        public float projectedRotation = 0f;

        public Ship(Texture2D texture, Vector2 velocity, float mass = 10, int height = 0, int width = 0)
            : base(texture, velocity, mass, height, width)
        {

        }


        public void UpdateGamepad(Vector2 position)
        {
            if (!position.X.Equals(0) || !position.Y.Equals(0))
            {
                Vector2 direction;

                direction.X = GameState.Width * position.X;
                direction.Y = GameState.Height * position.Y;

                projectedRotation = (float)Math.Atan2(direction.Y * -1, direction.X);
            }

            if (Math.Floor(Rotation) != Math.Floor(projectedRotation))
            {
                Rotation += projectedRotation > Rotation ? 0.1 : -0.1;
            }
            Update();
        }
        public void Update(Vector2 mousePosition)
        {
            Vector2 direction;
            direction.X = mousePosition.X - (Location.X + Width / 2f);
            direction.Y = mousePosition.Y - (Location.Y + Height / 2f);

            Rotation = (float)Math.Atan2(direction.Y, direction.X);
            Update();
        }

        public void Shoot(Texture2D texture, Vector2 direction)
        {
            var b = new Projectile(texture, Direction, new Vector2(Right + 2, Bottom / 2), 1f, 32, 8);
            b.Direction = Direction;
            b.Power = new Vector2(100);
            projectiles.Add(b);
        }

        public new void Update()
        {
            var newLocation = CheckLocation(Location);
            if (this.Location == newLocation)
                base.Update();
            else
                this.Location = newLocation;

            var pRemove = projectiles.Where(p => GameState.OutOfBounds(p));

            projectiles = projectiles.Where(p => !GameState.OutOfBounds(p)).ToList();

            foreach (var p in pRemove)
            {
                //
            }
            foreach (var p in projectiles)
            {
                p.Update();
            }
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
