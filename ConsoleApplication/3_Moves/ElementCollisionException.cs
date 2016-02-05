using System;

namespace ConsoleApplication.Moves
{
    public class ElementCollisionException : Exception
    {
        public ElementCollisionException(string message) : base(message)
        {
        }
    }
}
