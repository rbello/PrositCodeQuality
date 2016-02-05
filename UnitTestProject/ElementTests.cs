using ConsoleApplication.Elements;
using ConsoleApplication.Moves;
using ConsoleApplication.Univers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows;

namespace UnitTestProject
{
    [TestClass]
    public class ElementTests
    {
        private IElement _a1;
        private IElement _a2;
        private IElement _a3;

        [TestInitialize]
        public void Test_Elements_Init()
        {
            _a1 = ChemicalElementFactory.GetInstance().Create("Helium");
            _a2 = ChemicalElementFactory.GetInstance().Create("Hydrogen");
            _a3 = ChemicalElementFactory.GetInstance().Create("Lithium");
        }

        [TestMethod]
        public void Test_Elements_Creation()
        {
            Assert.AreEqual("He", _a1.Symbol);
            Assert.AreEqual(1.00794, _a2.Mass);
        }

        [TestMethod]
        public void Test_Elements_Moves()
        {
            Assert.AreEqual(_a1.Location.X, 0);
            Assert.AreEqual(_a1.Location.Y, 0);

            // Déplacement absolue
            _a1.Location = new Point(20, 30);
            Assert.AreEqual(_a1.Location.X, 20);
            Assert.AreEqual(_a1.Location.Y, 30);

            // Déplacement avec vecteur
            _a1.Location += new Vector(5, -4);
            Assert.AreEqual(_a1.Location.X, 25);
            Assert.AreEqual(_a1.Location.Y, 26);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Unknown chemical element: Delirium")]
        public void Test_Elements_Factory_Exceptions()
        {
            Assert.AreEqual(null, ChemicalElementFactory.GetInstance().Create("Delirium"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "At least 2 atomes in a molecule")]
        public void Test_Molecule_ConstructorException1()
        {
            new Molecule(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "At least 2 atomes in a molecule")]
        public void Test_Molecule_ConstructorException2()
        {
            var list = new List<Atom>();
            list.Add(ChemicalElementFactory.GetInstance().Create("Helium"));
            new Molecule(list.ToArray());
        }

        [TestMethod]
        public void Test_Molecule_Overloads()
        {
            var list = new List<Atom>();
            list.Add(ChemicalElementFactory.GetInstance().Create("Hydrogen"));
            list.Add(ChemicalElementFactory.GetInstance().Create("Hydrogen"));
            list.Add(ChemicalElementFactory.GetInstance().Create("Lithium"));
            var mol = new Molecule(list.ToArray());
            Assert.AreEqual(2 * 1.00794 + 6.941, mol.Mass);
            Assert.AreEqual("H2Li", mol.Symbol);
            Assert.AreEqual("H2Li", mol.Name);
        }

        [TestMethod]
        public void Test_Elements_Displacement()
        {
            // Initialisation
            IUnivers u = new Univers2D(50, 50);
            IElement e = ChemicalElementFactory.GetInstance().Create("Carbone");
            Assert.AreEqual(e.Location.X, 0);
            Assert.AreEqual(e.Location.Y, 0);
            Assert.AreEqual(e.Displacement.X, 0);
            Assert.AreEqual(e.Displacement.Y, 0);

            // On déplace l'élément en passant par sa méthode
            e.MoveStrategy = new BoundedMovesStrategy(u);
            e.Displacement = new Vector(2, 5);
            e.UpdateLocation();
            Assert.AreEqual(e.Location.X, 2);
            Assert.AreEqual(e.Location.Y, 5);
            Assert.AreEqual(e.Displacement.X, 2);
            Assert.AreEqual(e.Displacement.Y, 5);

            // On déplace l'élément en passant par sa stratégie
            e.MoveStrategy.ExecuteMove(e);
            Assert.AreEqual(e.Location.X, 4);
            Assert.AreEqual(e.Location.Y, 10);
            Assert.AreEqual(e.Displacement.X, 2);
            Assert.AreEqual(e.Displacement.Y, 5);

        }
    }
}
