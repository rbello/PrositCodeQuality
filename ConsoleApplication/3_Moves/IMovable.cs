using ConsoleApplication.Annotations;
using System.Windows;

namespace ConsoleApplication.Moves
{
    public interface IMovable
    {

        Point Location { get; set; }

        Vector Displacement { get; set; }

        [DesignPattern("Observer")]
        event ElementMovedEventHandler OnMoved;

        IMoveStrategy MoveStrategy { get; set; }

        void UpdateLocation();

    }
}
