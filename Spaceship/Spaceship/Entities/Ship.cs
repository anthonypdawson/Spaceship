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
        public Ship(Texture2D texture, SpriteBatch spriteBatch, Vector2 velocity, int height = 0, int width = 0)
            : base(texture, spriteBatch, velocity, height, width)
        {
        }

        public Ship(Texture2D texture, SpriteBatch spriteBatch)
            : base(texture, spriteBatch, new Vector2(0, 0))
        {

        }

        public void Update()
        {
            base.Update();
        }

        public void Draw(SpriteBatch spriteBatch = null)
        {
            base.Draw();
        }
    }
}
