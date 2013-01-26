using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceship.Entities
{
    public class Entity
    {

        #region Lots of properties

        protected Texture2D Texture;

        public State State;
        protected Vector2 _velocity;

        public float Drag;
        public float MaxSpeed;
        public float Acceleration;

        public float Top
        {
            get
            {
                return Location.Y;
            }
        }
        public float Bottom
        {
            get
            {
                return Location.Y + _height;
            }
        }
        public float Left
        {
            get
            {
                return Location.X;
            }
        }
        public float Right
        {
            get
            {
                return Location.X + _width;
            }
        }

        public float Momentum
        {
            get
            {
                return new[]
                {
                    _velocity.X,
                    _velocity.Y
                }.Average() * Mass;
            }
        }
        public float Mass;
        protected int _height, _width;

        public Vector2 Location { get; set; }

        float _velocityX
        {
            get
            {
                return _velocity.X;
            }
            set { _velocity.X = value; }
        }

        float _velocityY
        {
            get
            {
                return _velocity.Y;
            }
            set
            {
                _velocity.Y = value;
            }
        }

        #endregion

        public Entity(Texture2D texture, Vector2 velocity, float mass = 10, int height = 0, int width = 0)
        {
            Texture = texture;
            SetVelocity(velocity);

            _height = height == 0 ? Texture.Height : height;
            _width = width == 0 ? Texture.Width : width;
            MaxSpeed = 100;
            Mass = mass;
            Acceleration = 4f;
            Drag = 3f;
        }

        public Entity(Texture2D texture, SpriteBatch spriteBatch)
            : this(texture, new Vector2(0, 0))
        {

        }

        public void Update()
        {
            UpdateLocation(Velocity);
        }

        public void Draw(SpriteBatch spriteBatch = null)
        {
            var newSpriteBatch = spriteBatch == null;
            spriteBatch = spriteBatch ?? GameState.SpriteBatch;

            if (newSpriteBatch)
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(Texture, new Rectangle((int)Location.X, (int)Location.Y, _width, _height), Color.White);

            if (newSpriteBatch)
                spriteBatch.End();
        }

        public Vector2 Velocity
        {
            get
            {
                return _velocity * (float)GameState.GameTime.ElapsedGameTime.TotalSeconds;
            }
            set { _velocity = value; }
        }

        public void AddLeft(float analog = 1)
        {
            _velocityX -= (_velocityX > 0 ? (Acceleration * Drag) : Acceleration) * analog;
        }

        public void AddRight(float analog = 1)
        {
            _velocityX += (_velocityX < 0 ? (Acceleration * Drag) : Acceleration) * analog;
        }

        public void AddUp(float analog = 1)
        {
            _velocityY -= (_velocityY > 0 ? (Acceleration * Drag) : Acceleration) * analog;
        }

        public void AddDown(float analog = 1)
        {
            _velocityY += (_velocityY < 0 ? (Acceleration * Drag) : Acceleration) * analog;
        }

        private Tuple<float, SpriteEffects> GetAngle()
        {
            return new Tuple<float, SpriteEffects>(1, SpriteEffects.None);
        }

        private void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        private void UpdateLocation(Vector2 velocity)
        {
            Location += velocity;
        }


    }

    public enum State
    {
        Idle,
        Moving
    }
}
