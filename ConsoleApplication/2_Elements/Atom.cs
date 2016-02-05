
namespace ConsoleApplication.Elements
{
    public class Atom : AElement
    {

        public Atom(string name, string symbol, double mass) : base(name, symbol, mass)
        {
            Symbol = symbol;
        }

    }
}
