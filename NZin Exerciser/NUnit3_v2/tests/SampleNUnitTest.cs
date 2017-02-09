using NUnit.Framework;


namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1() 
        {
            Assert.Pass();
        }

        [Test]
        public void Test2() 
        {
            Assert.Fail();
        }
    }
}