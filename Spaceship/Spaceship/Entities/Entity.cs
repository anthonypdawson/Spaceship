using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceship.Entities
{
    public class Entity
    {
        protected SpriteBatch SpriteBatch;
        protected Texture2D Texture;

        protected State _state;
        private Vector2 _location;
        protected Vector2 _velocity;
        protected float _maxSpeed;
        protected float _momentum;
        protected int _height, _width;

        public Vector2 Location
        {
            get { return _location; }
            set { _location = value; }
        }

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


        public Entity(Texture2D texture, SpriteBatch spriteBatch, Vector2 velocity, float momentum = 10, int height = 0, int width = 0)
        {
            Texture = texture;
            SetVelocity(velocity);
            SpriteBatch = spriteBatch;

            _height = height == 0 ? Texture.Height : height;
            _width = width == 0 ? Texture.Width : width;
            _maxSpeed = 100;
            _momentum = momentum;
        }

        public Entity(Texture2D texture, SpriteBatch spriteBatch)
            : this(texture, spriteBatch, new Vector2(0, 0))
        {

        }

        public void Update()
        {
            if(Velocity != new Vector2(0, 0))
                _state = State.Moving;
            else
                _state = State.Idle;

            UpdateLocation(Velocity);
        }

        public void Draw(SpriteBatch spriteBatch = null)
        {
            spriteBatch = spriteBatch ?? SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(Texture, new Rectangle((int)Location.X, (int)Location.Y, _width, _height), Color.White);

            spriteBatch.End();
        }

        public Vector2 Velocity
        {
            get
            {
                return _velocity * (float)Global.GameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void AddLeft()
        {
            _velocityX -= _momentum;
        }

        public void AddRight()
        {
            _velocityX += _momentum;
        }

        public void AddUp()
        {
            _velocityY -= _momentum;
        }

        public void AddDown()
        {
            _velocityY += _momentum;
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
