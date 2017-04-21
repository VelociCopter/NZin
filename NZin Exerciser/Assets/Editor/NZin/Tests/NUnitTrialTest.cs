using NUnit.Framework;
using NZin;


// zzz td: Ensure proper test coverage for most of NZin!
// zzz td: Kill this class
namespace NUnitTesting.Test
{
    [TestFixture]
    public class PassExample {
        
        [Test]
        public void Passing() {
            NUnit.Framework.Assert.Pass();
        }

		// [Test]
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
            NUnit.Framework.Assert.Pass();
        }



    }

}