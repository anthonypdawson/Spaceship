using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spaceship.Entities
{
    public class Projectile : Entity
    {

        public Projectile(Texture2D texture, Vector2 velocity, Vector2 location, float mass = 50f, int height = 0, int width = 0)
            : base(texture, velocity, location, mass, height, width)
        {

        }

        public void Update()
        {
            Location += Velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, Color.White);
        }
    }
}
