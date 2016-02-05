using ConsoleApplication.Annotations;
using ConsoleApplication.Elements;
using ConsoleApplication.Moves;
using System.Collections.Generic;
using System.Windows;

namespace ConsoleApplication.Univers
{

    [DesignPattern("Bridge")]
    [DesignPattern("Observer")]
    public interface IUnivers
    {

        int Width { get; }

        int Height { get; }

        int ElementsCount { get; }

        [DesignPattern("Iterator")]
        IEnumerable<IElement> Elements { get; }

        void Add(IElement element);

        void Remove(IElement element);

        [SucreSyntaxique]
        IElement this[Point location] { get; set; }

        bool IsInside(Point value);

        [DesignPattern("Observer")]
        event ElementAddedEventHandler OnElementAdded;

        [DesignPattern("Observer")]
        event ElementRemovedEventHandler OnElementRemoved;

        [DesignPattern("Observer")]
        event ElementsCollisionEventHandler OnCollision;

        void Update();

        void ThrowCollisionEvent(ElementsCollisionEvent evt);
    }
}
