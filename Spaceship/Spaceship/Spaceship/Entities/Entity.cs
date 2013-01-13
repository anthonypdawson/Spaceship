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

        Vector2 _location;

        int _velocity_x;
        int _velocity_y;

        int _height;
        int _width;

        protected Entity(Texture2D texture, SpriteBatch spriteBatch, Vector2 velocity, int height = 0, int width = 0)
        {
            _texture = texture;
            SetVelocity(velocity);
            _spriteBatch = spriteBatch;

            _height = height == 0 ? _texture.Height : height;
            _width = width == 0 ? _texture.Width : width;
        }

        protected Entity(Texture2D texture, SpriteBatch spriteBatch)
            : this(texture, spriteBatch, new Vector2(0, 0))
        {

        }

        public void Update()
        {
            _location.X += (_velocity_x / 10);
            _location.Y += (_velocity_y / 10);
        }

        public void Draw(SpriteBatch spriteBatch = null)
        {
            spriteBatch = spriteBatch ?? _spriteBatch;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(_texture, new Rectangle((int)_location.X, (int)_location.Y, _width, _height), Color.White);

            spriteBatch.End();
        }

        public void AddLeft()
        {
            _velocity_x--;
        }

        public void AddRight()
        {
            _velocity_x++;
        }

        public void AddUp()
        {
            _velocity_y--;
        }

        public void AddDown()
        {
            _velocity_y++;
        }

        private Tuple<float, SpriteEffects> GetAngle()
        {
            if (_velocity_y > 10)
            {
                return new Tuple<float, SpriteEffects>(-1, SpriteEffects.FlipVertically);
            }
            return new Tuple<float, SpriteEffects>(1, SpriteEffects.None);
        }
        private void SetVelocity(Vector2 velocity)
        {
            _velocity_x = (int)velocity.X;
            _velocity_y = (int)velocity.Y;
        }
    }
}
