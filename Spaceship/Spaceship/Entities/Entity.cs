using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceship.Entities
{
    public class Entity
    {

        #region Lots of properties

        protected Texture2D Texture;
        
        public Vector2 Origin
        {
            get
            {
                return new Vector2(Texture.Width / 2, Texture.Height / 2);
                //return new Vector2(_width / 2f, _height / 2f);
            }
        }

        public State State;
        public Vector2 Velocity;
        public double Rotation = 0;

        public float Mass;
        public float MaxSpeed = 25;
        public Vector2 Power;

        public Vector2 Force
        {
            get { return Power;  /* Direction * Power; */ }
        }

        public Vector2 Acceleration
        {
            get { return Force / Mass; }
        }

        public Vector2 Direction;

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



        protected int _height, _width;

        public Vector2 Location { get; set; }


        #endregion

        public Entity(Texture2D texture, Vector2 velocity, float mass = 50f, int height = 0, int width = 0)
        {
            Texture = texture;

            _height = height == 0 ? Texture.Height : height;
            _width = width == 0 ? Texture.Width : width;
            MaxSpeed = 25;
            Power = new Vector2(0f);
            Mass = mass;
            Direction = new Vector2(0);
        }

        public Entity(Texture2D texture, SpriteBatch spriteBatch)
            : this(texture, new Vector2(0, 0))
        {

        }

        public void Update()
        {
            UpdateLocation();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var newSpriteBatch = spriteBatch == null;
            spriteBatch = spriteBatch ?? GameState.SpriteBatch;

            if (newSpriteBatch)
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            
            DoDraw(spriteBatch);
           
            if (newSpriteBatch)
                spriteBatch.End();
        }

        
        private void DoDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Location.X, (int)Location.Y, _width, _height),null, Color.White, (float)(Rotation  + (Math.PI*2.0f)), Origin, SpriteEffects.None, 0);
        }

        

        public void AddLeft(float analog = 1)
        {
            if (Acceleration.X > (0 - MaxSpeed))
            {
                Power.X += (0 - analog);
            }
            UpdateVelocity();
        }

        public void AddRight(float analog = 1)
        {
            if (Acceleration.X < MaxSpeed)
            {
                Power.X += analog;
            }
            UpdateVelocity();
        }

        public void AddUp(float analog = 1)
        {
            if (Acceleration.Y > (0 - MaxSpeed))
            {
                Power.Y += (0 - analog);
            }
            UpdateVelocity();
            
        }

        public void AddDown(float analog = 1)
        {
            if (Acceleration.Y < MaxSpeed)
            {
                Power.Y += analog;
            }
            UpdateVelocity();
        }

        private Tuple<float, SpriteEffects> GetAngle()
        {
            return new Tuple<float, SpriteEffects>(1, SpriteEffects.None);
        }

        private void UpdateVelocity()
        {
            //_velocity += Acceleration*new Vector2((float) GameState.GameTime.ElapsedGameTime.TotalMilliseconds);
            
            var calcVel = Acceleration * new Vector2((float)GameState.GameTime.ElapsedGameTime.TotalMilliseconds);

                Velocity.X = calcVel.X;

                Velocity.Y = calcVel.Y;

        }

        private void UpdateLocation()
        {
            Location += Velocity * new Vector2((float)GameState.GameTime.ElapsedGameTime.TotalSeconds);
        }


    }

    public enum State
    {
        Idle,
        Moving
    }
}
