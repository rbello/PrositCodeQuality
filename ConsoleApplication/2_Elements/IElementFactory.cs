using ConsoleApplication.Annotations;
using ConsoleApplication.Univers;
using System.Collections.Generic;

namespace ConsoleApplication.Elements
{
    [DesignPattern("AbstractFactory")]
    public interface IElementFactory
    {

        Atom Create(string type);

        Atom Create(string type, int positionX, int positionY);

        Atom Create(string type, int positionX, int positionY, IUnivers univers);

        Molecule Create(List<Atom> atoms);

        Molecule Create(Atom[] atoms);
    }
}
