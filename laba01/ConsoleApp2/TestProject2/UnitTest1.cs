using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Rectangle rectangle = new Rectangle(5, 10);
            Assert.AreEqual(30, rectangle.Perimeter);
        }

        [TestMethod]
        public void TestArea()
        {
            Rectangle rectangle = new Rectangle(5, 10);
            Assert.AreEqual(50, rectangle.Area);
        }

        [TestMethod]

        public void TestArea2()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(3, 0);
            Point p3 = new Point(3, 4);
            Point p4 = new Point(0, 4);

            Figure rectangle = new Figure(p1, p2, p3, p4);

            Assert.AreEqual(14, rectangle.PerimeterCalculator());
        }

        [TestMethod]

        public void TestArea4() {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(3, 0);
            Point p3 = new Point(3, 4);
            Point p4 = new Point(0, 4);

            Figure rectangle = new Figure(p1, p2, p3, p4);
            Assert.AreEqual(4, rectangle.LengthSide(p2, p3));
        }
    }
}