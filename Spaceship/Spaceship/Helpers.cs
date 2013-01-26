using System;

namespace Spaceship
{
    public static class HelperExtensions
    {
        public static Boolean Equals(this float x, float y)
        {
            return Math.Abs(x - y) > float.Epsilon;            
        }
    }
}
