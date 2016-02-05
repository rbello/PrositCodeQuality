
using System;
using ConsoleApplication.Univers;
using System.Windows;
using ConsoleApplication.Annotations;
using ConsoleApplication.Moves;

namespace ConsoleApplication.Moves
{
    public class UnBoundedMovesStrategy : IMoveStrategy
    {
        public IUnivers Univers { get; private set; }

        public event ElementsCollisionEventHandler OnCollision;

        public UnBoundedMovesStrategy(IUnivers univers)
        {
            Univers = univers;
        }

        public void ExecuteMove(IMovable element)
        {
            Point dest = element.Location + element.Displacement;
            if (dest.X >= Univers.Width)
            {
                dest.X = 0;
            }
            else if (dest.X < 0)
            {
                dest.X = Univers.Width - 1;
            }
            if (dest.Y >= Univers.Height)
            {
                dest.Y = 0;
            }
            else if (dest.Y < 0)
            {
                dest.Y = Univers.Height - 1;
            }
            try {
                element.Location = dest;
            }
            catch (ElementCollisionException)
            {
                var evt = new ElementsCollisionEvent(element, Univers[dest], dest);
                Univers.ThrowCollisionEvent(evt);
                if (OnCollision != null)
                {
                    OnCollision(evt);
                }
            }
        }
    }
}
