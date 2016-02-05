using ConsoleApplication.Annotations;
using System;
using System.Windows;
using ConsoleApplication.Univers;
using ConsoleApplication.Moves;
using System.Collections.Generic;

namespace ConsoleApplication.Elements
{
    [DesignPattern("AbstractFactory")]
    [DesignPattern("Singleton")]
    public class ChemicalElementFactory : IElementFactory
    {

        private static ChemicalElementFactory INSTANCE = null;

        [DesignPattern("Singleton")]
        private ChemicalElementFactory()
        {
        }

        public static IElementFactory GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new ChemicalElementFactory();
            }
            return INSTANCE;
        }

        public Molecule Create(Atom[] atoms)
        {
            return new Molecule(atoms);
        }

        public Molecule Create(List<Atom> atoms)
        {
            return new Molecule(atoms.ToArray());
        }

        public Atom Create(string type)
        {
            switch (type.ToUpper())
            {
                case "HYDROGEN": return new Atom("Hydrogen", "H", 1.00794);
                case "HELIUM": return new Atom("Helium", "He", 4.002602);
                case "LITHIUM": return new Atom("Lithium", "Li", 6.941);
                case "CARBONE": return new Atom("Carbone", "C", 12.01074);
                case "OXYGEN": return new Atom("Oxygen", "O", 15.9994);
            }
            throw new ArgumentException(String.Format("Unknown chemical element: {0}", type));
        }

        public Atom Create(string type, int positionX, int positionY)
        {
            Atom e = Create(type);
            if (e != null)
            {
                e.Location = new Point(positionX, positionY);
            }
            return e;
        }

        public Atom Create(string type, int positionX, int positionY, IUnivers univers)
        {
            Atom e = Create(type, positionX, positionY);
            if (e != null)
            {
                e.Univers = univers;
                e.MoveStrategy = new BoundedMovesStrategy(univers);
            }
            return e;
        }
    }
}
