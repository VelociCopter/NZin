using NUnit.Framework;
using NZin;

namespace MyDnxProject.Test
{
    [TestFixture]
    public class PassExample {
        
        [Test]
        public void Passing() {
            NUnit.Framework.Assert.Pass();
        }
        
    }

    [TestFixture]
    public class CalculatorTests
    {
        [TestCase(1, 1, 2)]
        [TestCase(-1, -1, -2)]
        [TestCase(100, 5, 105)]
        public void CanAddNumbers(int x, int y, int expected)
        {
            NUnit.Framework.Assert.That(Calculator.Add(x, y), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class NZinTests {
    
        class Base : Decoratable {
        }

        [Test]
        public void SimpleTest() {
            Decorator<Base> test;
            Assert.Pass();
        }
    }

}