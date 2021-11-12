using NUnit.Framework;
using System;

namespace Delegates
{
    [TestFixture]
    class PerformCalculation
    {
        public delegate int calculate(int x, int y);

        [Test]
        public void testAddition()
        {
            calculate add = (x, y) => x + y;
            Assert.That(add(5, 5), Is.EqualTo(10));
        }

        [Test]
        public void testModulus()
        {
            calculate add = (x, y) => x % y;
            Assert.That(add(5, 2), Is.EqualTo(1));
        }

        [Test]
        public void tesMulticast()
        {
            // Silly example but shows that delegates can easily be multi-cast.  However they don't pass inputs from one to the next.
            // In this case the last one in the list provides the return value.
            calculate operations = (x, y) => x % y;
            operations += (x, y) => x + y;
            int result = operations(5, 3);
            Assert.That(result, Is.EqualTo(8));

        }
    }
}
