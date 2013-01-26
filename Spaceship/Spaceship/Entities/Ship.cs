using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceship.Entities
{
    class Ship : Entity
    {        

        private Entity _double;

        private int Height
        {
            get { return _height; }
        }
        private int Width
        {
            get { return _width; }
        }

        float _velocityX
        {
            get
            {
                return _velocity.X;
            }
            set
            {
                if(value.Equals(_velocity.X))
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
                if(value.Equals(_velocity.Y))
                    return;

                _velocity.Y = CheckVelocity(value);
            }
        }
        public Ship(Texture2D texture, Vector2 velocity, float mass = 10, int height = 0, int width = 0)
            : base(texture, velocity, mass, height, width)
        {
            
        }

        public new void Update()
        {
            base.Update();

            if ((_double = CheckLocation(Location)) != null)
            {
               _double.Update();
            }
                
        }

        public new void Draw(SpriteBatch spriteBatch=null)
        {
            base.Draw(GameState.SpriteBatch);
            if (_double != null)
            {
                _double.Draw();
            }
        }
        public new void AddLeft()
        {
            base.AddLeft();
        }

        public new void AddRight()
        {
            base.AddRight();
        }

        public new void AddUp()
        {
            base.AddUp();
        }

        public new void AddDown()
        {
            base.AddDown();
        }

        private Entity CheckLocation(Vector2 location)
        {
            if (((location.X + Width) > GameState.Width && location.X < GameState.Width) ||
                (location.X < 0 && (location.X + Width > 0)) ||
                (((location.Y + Height) > GameState.Height && (location.X < GameState.Height)) ||
                 location.Y < 0 && (location.Y + Height) > 0))
            {
                var doubleLocation = new Vector2(location.X, location.Y);                
                if ((location.X + Width) - GameState.Width > 0)
                {
                    doubleLocation.X = location.X - GameState.Width;
                        
                }
                if (location.X < 0 && (location.X + Width > 0))
                {
                    doubleLocation.X = location.X + GameState.Width;
                }
                if ((location.Y + Height) - GameState.Height > 0)
                {
                    doubleLocation.Y = location.Y - GameState.Height;
                }
                if(location.Y < 0 && (location.Y + Height) > 0)
                {
                    doubleLocation.Y = location.Y + GameState.Height;
                }

                return new Entity(Texture, new Vector2(0, 0), 0f, Height, Width) { Location = doubleLocation};
                
            }
            return null;            
        }

        private float CheckVelocity(float value)
        {
            var maxSpeed = MaxSpeed;

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
