using ConsoleApplication.Annotations;
using ConsoleApplication.Elements;

namespace ConsoleApplication.Univers
{

    public delegate void ElementAddedEventHandler(ElementAddedEvent evt);

    public delegate void ElementRemovedEventHandler(ElementRemovedEvent evt);

    [DesignPattern("Observer")]
    public abstract class AbstractElementEvent
    {
        public AbstractElementEvent(IUnivers univers, IElement element)
        {
            Univers = univers;
            Element = element;
        }

        public IUnivers Univers { get; private set; }

        public IElement Element { get; private set; }
    }

    [DesignPattern("Observer")]
    public class ElementAddedEvent : AbstractElementEvent
    {
        public ElementAddedEvent(IUnivers univers, IElement element) : base(univers, element) { }
    }

    [DesignPattern("Observer")]
    public class ElementRemovedEvent : AbstractElementEvent
    {
        public ElementRemovedEvent(IUnivers univers, IElement element) : base(univers, element) { }
    }

}
