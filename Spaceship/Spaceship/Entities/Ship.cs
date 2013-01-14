using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceship.Entities
{
    class Ship : Entity
    {
        public Ship(Texture2D texture, SpriteBatch spriteBatch, Vector2 velocity, float momentum = 10, int height = 0, int width = 0)
            : base(texture, spriteBatch, velocity, momentum, height, width)
        {
        }

        public Ship(Texture2D texture, SpriteBatch spriteBatch)
            : base(texture, spriteBatch, new Vector2(0, 0))
        {

        }

        private Entity Double;

        private int Height
        {
            get { return Texture.Height; }
        }
        private int Width
        {
            get { return Texture.Width; }
        }

        float _velocityX
        {
            get
            {
                return _velocity.X;
            }
            set
            {
                if(value == _velocity.X)
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
                if(value == _velocity.Y)
                    return;

                _velocity.Y = CheckVelocity(value);
            }
        }

        public new void Draw(SpriteBatch spriteBatch=null)
        {
            base.Draw(spriteBatch);
            if (Double != null)
            {
                Double.Draw(spriteBatch);
            }
        }
        public new void AddLeft()
        {
            _velocityX -= _momentum;
        }

        public new void AddRight()
        {
            _velocityX += _momentum;
        }

        public new void AddUp()
        {
            _velocityY -= _momentum;
        }

        public new void AddDown()
        {
            _velocityY += _momentum;
        }

        private void CheckLocation(Vector2 location)
        {
            if (((location.X + Width) > Global.Width && location.X < Global.Width) ||
                (location.X < 0 && (location.X + Width > 0)) ||
                (((location.Y + Height) > Global.Height && (location.X < Global.Height)) ||
                 location.Y < 0 && (location.Y + Height) > 0))
            {
                var double_location = new Vector2(location.X, location.Y);                
                if (((location.X + Width) > Global.Width && location.X < Global.Width))
                {
                    double_location.X = 0 - (location.X + Width - Width);
                        
                }
                if (location.X < 0 && (location.X + Width > 0))
                {
                    double_location.X = Global.Width - (location.X + Width);
                }
                if ((location.Y + Height) > Global.Height && (location.X < Global.Height))
                {
                    double_location.Y = 0 - (location.Y + Height - Global.Height);
                }
                if(location.Y < 0 && (location.Y + Height) > 0)
                {
                    double_location.Y = Global.Height - (Height + location.Y);
                }

                Double = new Entity(Texture, SpriteBatch, double_location, 0, Height, Width);
                return;
            }
            Double = null;            
        }

        private float CheckVelocity(float value)
        {
            var maxSpeed = _maxSpeed;

            if(value < 0)
            {
                maxSpeed *= -1;
                if(value < maxSpeed)
                    return maxSpeed;
                return value;
            }

            if(value > maxSpeed)
                return maxSpeed;
            return value;
        }
    }
}
