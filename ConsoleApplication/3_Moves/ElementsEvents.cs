using ConsoleApplication.Annotations;
using System.Windows;

namespace ConsoleApplication.Moves
{

    public delegate void ElementMovedEventHandler(ElementMovedEvent evt);

    public delegate void ElementsCollisionEventHandler(ElementsCollisionEvent evt);

    [DesignPattern("Observer")]
    public class ElementMovedEvent
    {
        public ElementMovedEvent(IMovable element, Point locationOld, Point locationNew)
        {
            Element = element;
            OldLocation = locationOld;
            NewLocation = locationNew;
        }

        public IMovable Element { get; private set; }

        public Point NewLocation { get; private set; }

        public Point OldLocation { get; private set; }
    }

    [DesignPattern("Observer")]
    public class ElementsCollisionEvent
    {
        public ElementsCollisionEvent(IMovable from, IMovable to, Point at)
        {
            FromElement = from;
            ToElement = to;
            AtLocation = at;
        }

        public IMovable FromElement { get; private set; }

        public IMovable ToElement { get; private set; }

        public Point AtLocation { get; private set; }

    }
}
