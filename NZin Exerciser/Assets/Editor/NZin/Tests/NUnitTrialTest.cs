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

		[Test]
		public void Failing() {
			NUnit.Framework.Assert.Fail();
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

		[Test]
		public void SimpleFail()
		{
			Decorator<Base> test;
			NUnit.Framework.Assert.Fail();
		}
    }

}