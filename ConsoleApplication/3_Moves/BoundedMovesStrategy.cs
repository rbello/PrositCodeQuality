using ConsoleApplication.Univers;
using System.Windows;

namespace ConsoleApplication.Moves
{
    public class BoundedMovesStrategy : IMoveStrategy
    {
        public IUnivers Univers { get; private set; }

        public event ElementsCollisionEventHandler OnCollision;

        public BoundedMovesStrategy(IUnivers univers)
        {
            Univers = univers;
        }

        public void ExecuteMove(IMovable element)
        {
            Point dest = element.Location + element.Displacement;
            if (dest.X >= Univers.Width)
            {
                element.Displacement = new Vector(element.Displacement.X * -1, element.Displacement.Y);
                dest.X = (Univers.Width - 1) - (dest.X - Univers.Width);
            }
            else if (dest.X < 0)
            {
                element.Displacement = new Vector(element.Displacement.X * -1, element.Displacement.Y);
                dest.X *= -1;
            }
            if (dest.Y >= Univers.Height)
            {
                element.Displacement = new Vector(element.Displacement.X, element.Displacement.Y * -1);
                dest.Y = (Univers.Height - 1) - (dest.Y - Univers.Height);
            }
            else if (dest.Y < 0)
            {
                element.Displacement = new Vector(element.Displacement.X, element.Displacement.Y * -1);
                dest.Y *= -1;
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
