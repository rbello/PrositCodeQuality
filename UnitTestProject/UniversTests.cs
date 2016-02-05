using ConsoleApplication.Elements;
using ConsoleApplication.Moves;
using ConsoleApplication.Univers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;

namespace UnitTestProject
{
    [TestClass]
    public class UniversTests
    {

        private Univers2D _univers;
        private IElementFactory _factory;

        [TestInitialize]
        public void Test_Univers_Init()
        {
            _univers = new Univers2D(800, 500);
            _factory = ChemicalElementFactory.GetInstance();
        }

        [TestMethod]
        public void Test_Univers_Creation()
        {
            Assert.AreEqual(800, _univers.Width);
            Assert.AreEqual(500, _univers.Height);
            Assert.AreEqual(0, _univers.ElementsCount);
        }

        [TestMethod]
        public void Test_Univers_Dimension()
        {
            Assert.AreEqual(true,  _univers.IsInside(new Point(0 ,0)));
            Assert.AreEqual(true,  _univers.IsInside(new Point(799, 499)));
            Assert.AreEqual(false, _univers.IsInside(new Point(800, 500)));
            Assert.AreEqual(false, _univers.IsInside(new Point(-1, 0)));
            Assert.AreEqual(false, _univers.IsInside(new Point(0, -60)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Element allready exists in this univers")]
        public void Test_Univers_Populate_Exception_SameInstance()
        {
            var a1 = _factory.Create("Helium");
            _univers.Add(a1);
            _univers.Add(a1);
        }

        [TestMethod]
        [ExpectedException(typeof(ElementCollisionException), "Location [0;0] is allready occuped")]
        public void Test_Univers_Populate_Exception_SameLocation()
        {
            _univers.Add(_factory.Create("Helium"));
            _univers.Add(_factory.Create("Helium"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Location [-50;600] is out of univers' range")]
        public void Test_Univers_Populate_Exception_OutOfUnivers()
        {
            _univers.Add(_factory.Create("Helium", -50, 600));
        }

        [TestMethod]
        [ExpectedException(typeof(ElementCollisionException), "Location [50;80] is allready occuped")]
        public void Test_Univers_Populate_Exception_InvalidMove()
        {
            var a1 = _factory.Create("Helium");
            _univers += a1;
            a1.Location += new Vector(50, 80);
            Assert.AreEqual(50, a1.Location.X);
            Assert.AreEqual(80, a1.Location.Y);
            var a2 = _factory.Create("Hydrogen");
            _univers += a2;
            a2.Location += new Vector(50, 80);
        }

        [TestMethod]
        public void Test_Univers_Populate_ElementsCount()
        {
            _univers += _factory.Create("Helium", 300, 26);
            _univers += _factory.Create("Helium", 588, 105);
            Assert.AreEqual(2, _univers.ElementsCount);
        }
    }
}
