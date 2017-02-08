using NUnit.Framework;
using NZin;

namespace NUnitTesting.Test
{
    [TestFixture]
    public class PassExample {
        
        [Test]
        public void Passing() {
            NUnit.Framework.Assert.Pass();
        }
        
    }



    [TestFixture]
    public class NZinTests {
    
        class Base : Decoratable {
        }

        [Test]
        public void SimpleTest() {
            Decorator<Base> test;
            NUnit.Framework.Assert.Pass();
        }
    }

}