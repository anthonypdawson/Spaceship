using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spaceship.IO
{
    public static class PlayerInput
    {
        public static readonly Keys[] MovementKeys = new Keys[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down };

        public static Boolean IsMovement(Keys key)
        {
            return MovementKeys.Contains(key);
        }

        public static Boolean HasMovement(Keys[] keys)
        {
            return keys.Any(k => IsMovement(k));
        }
    }
}
