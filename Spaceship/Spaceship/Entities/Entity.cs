using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceship.Entities
{
    class Entity
    {
        SpriteBatch _spriteBatch;
        Texture2D _texture;

        State _state;
        Vector2 _location, _velocity;
        float _maxSpeed, _momentum;
        int _height, _width;

        float _velocityX
        {
            get
            {
                return _velocity.X;
            }
            set
            {
                if (value == _velocity.X)
                    return;

                _velocity.X = CheckVelocity(value);
            }
        }
        float _velocityY
        {
            get
            {
                return _velocity.Y;
            }
            set
            {
                if (value == _velocity.Y)
                    return;

                _velocity.Y = CheckVelocity(value);
            }
        }


        protected Entity(Texture2D texture, SpriteBatch spriteBatch, Vector2 velocity, float momentum = 10, int height = 0, int width = 0)
        {
            _texture = texture;
            SetVelocity(velocity);
            _spriteBatch = spriteBatch;

            _height = height == 0 ? _texture.Height : height;
            _width = width == 0 ? _texture.Width : width;
            _maxSpeed = 100;
            _momentum = momentum;
        }

        protected Entity(Texture2D texture, SpriteBatch spriteBatch)
            : this(texture, spriteBatch, new Vector2(0, 0))
        {

        }

        public void Update()
        {
            if (Velocity != new Vector2(0, 0))
                _state = State.Moving;
            else
                _state = State.Idle;

            UpdateLocation(Velocity);
        }

        public void Draw(SpriteBatch spriteBatch = null)
        {
            spriteBatch = spriteBatch ?? _spriteBatch;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(_texture, new Rectangle((int)_location.X, (int)_location.Y, _width, _height), Color.White);

            spriteBatch.End();
        }

        public Vector2 Velocity
        {
            get
            {
                return _velocity * (float)Clock.GameTime.ElapsedGameTime.TotalSeconds;
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
            _location += velocity;
        }

        private float CheckVelocity(float value)
        {
            var maxSpeed = _maxSpeed;

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

    public enum State
    {
        Idle,
        Moving
    }
}
