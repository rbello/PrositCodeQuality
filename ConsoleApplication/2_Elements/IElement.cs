using ConsoleApplication.Annotations;
using ConsoleApplication.Moves;
using ConsoleApplication.Univers;

namespace ConsoleApplication.Elements
{

    [DesignPattern("Bridge")]
    [DesignPattern("Observer")]
    [DesignPattern("Strategy")]
    public interface IElement : IMovable
    {
        string Name { get; }

        string Symbol { get; }

        double Mass { get; }

        IUnivers Univers { get; set; }

    }
}
