using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Looping
{
    [TestFixture]
    class While
    {
        [Test]
        public void WhileTest()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var evenNumbers = new List<int>();
            int numValues = numbers.Length;
            int current = 0;

            while (current < numValues)
            {
                if (numbers[current] % 2 == 0)
                {
                    evenNumbers.Add(numbers[current]);
                }
                ++current;
            }

            Assert.That(evenNumbers, Has.No.Member(1));
            Assert.That(evenNumbers, Has.No.Member(3));
            Assert.That(evenNumbers, Has.No.Member(5));
            Assert.That(evenNumbers, Has.No.Member(7));
            Assert.That(evenNumbers, Has.Member(2));
            Assert.That(evenNumbers, Contains.Item(4));
            Assert.That(evenNumbers, Does.Contain(6));
            Assert.That(evenNumbers, Does.Contain(8));
            Assert.That(evenNumbers.Count == 4);
        }
    }
}
