using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spaceship.IO
{
    public static class PlayerInput
    {
        public static readonly Keys[] MovementKeys =
            new Keys[] 
            { 
                Keys.Left, 
                Keys.Right, 
                Keys.Up, 
                Keys.Down 
            };

        public static readonly Buttons[] MovementButtons =
            new Buttons[] 
        { 
            Buttons.LeftThumbstickLeft, 
            Buttons.LeftThumbstickRight, 
            Buttons.LeftThumbstickUp, 
            Buttons.LeftThumbstickDown 
        };

        public static Boolean HasMovement(Keys[] keys = null, Buttons[] buttons = null)
        {
            bool isMovement = false;
            if (keys != null)
                isMovement = HasMovement(keys);
            if (buttons != null && !isMovement)
                isMovement = HasMovement(buttons);

            return isMovement;


        }

        public static Boolean IsMovement(Keys key)
        {
            return MovementKeys.Contains(key);
        }

        public static Boolean IsMovement(Buttons button)
        {
            return MovementButtons.Contains(button);
        }

        public static Boolean HasMovement(Keys[] keys)
        {
            return keys.Any(k => IsMovement(k));
        }

        public static Boolean HasMovement(Buttons[] buttons)
        {
            return buttons.Any(b => IsMovement(b));
        }

    }
}
