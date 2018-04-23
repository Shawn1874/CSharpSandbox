using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace LINQ
{
    [TestFixture]
    public class BasicQueryExpressions
    {
        [Test]
        public void DoubleTheFunTransformTest()
        {
            int[] fun = { 1, 2, 3, 4, 5 };

            var doubled = (from value in fun
                           select value * 2).ToList();   // execute query and store the results in a list

            Assert.That(doubled.Sum, Is.EqualTo(30));
        }

        [Test]
        public void GetEvenNumbersTest()
        {
            var fun = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var doubled = (from value in fun
                           where value % 2 == 0
                           select value);

            var cached = doubled.ToList();

            // Add to fun and confirm that the cached results don't include the 12
            fun.Add(11);
            fun.Add(12);

            Assert.That(cached.Sum, Is.EqualTo(30));
            Assert.That(doubled.Sum(), Is.EqualTo(42));
        }
    }
}
