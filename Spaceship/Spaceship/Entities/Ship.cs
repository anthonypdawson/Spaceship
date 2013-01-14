using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
