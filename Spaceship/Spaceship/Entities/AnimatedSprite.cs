using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platforms
{
    class AnimatedSprite
    {
        private Texture2D[] _textures;
        private readonly Vector2 _position;

        private int _positionX
        {
            get
            {
                return (int)_position.X;
            }
        }
        private int _positionY
        {
            get
            {
                return (int)_position.Y;
            }
        }
        private int _currentFrame;
        private int _updateCount;
        private readonly int _framesPerSecond;
        private SpriteBatch _spriteBatch;

        public AnimatedSprite(Texture2D[] textures, Vector2 location, int framesPerSecond = 0, SpriteBatch spriteBatch = null)
        {
            _textures = textures;
            _position = location;
            _framesPerSecond = framesPerSecond == 0 ? 30 / _textures.Count() : framesPerSecond;
            _spriteBatch = spriteBatch;
        }

        public AnimatedSprite(Texture2D[] textures, Vector2 location, int speed)
            : this(textures, location, speed, null)
        {

        }

        public AnimatedSprite(Texture2D[] textures, Vector2 location, SpriteBatch spriteBatch)
            : this(textures, location, 0, spriteBatch)
        {

        }

        public void Update()
        {
            _updateCount++;

            if (_updateCount == _framesPerSecond)
                _updateCount = 0;

            if (_updateCount == 0)
                _currentFrame++;

            if (_currentFrame == _textures.Length)
                _currentFrame = 0;
        }
        public void Draw()
        {
            if (_spriteBatch != null)
                Draw(_spriteBatch);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_textures[_currentFrame], new Rectangle(_positionX, _positionY, 128, 128), Color.White);
            spriteBatch.End();
        }
    }
}
