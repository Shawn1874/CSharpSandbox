using System.Collections.ObjectModel;
using NUnit.Framework;
using System.Collections.Generic;

namespace Generics
{
    [TestFixture]
    class List
    {
        [Test]
        public static void sortListTest()
        {
            var numbers = new List<int>() { 5, 3, 1, 10, 2 };
            numbers.Add(7);
            Assert.That(numbers.Count, Is.EqualTo(6));
            numbers.Sort();
            Assert.That(numbers[0], Is.EqualTo(1));

            // sort decending with ICompare
            numbers.Sort((a, b) => { return b - a; });
            Assert.That(numbers[0], Is.EqualTo(10));

            // sort ascending with ICompare
            // notice how the lambda can be simplified
            numbers.Sort((a, b) => a - b);
            Assert.That(numbers[0], Is.EqualTo(1));
        }

        [Test]
        public void addRangeTest()
        {
            var numbers = new List<int>();
            Assert.That(numbers.Count, Is.EqualTo(0));
            numbers.AddRange(new int[] { 1, 3, 5, 7});
            Assert.That(numbers.Count, Is.EqualTo(4));
        }

        [Test]
        public void binarySearchTest()
        {
            var numbers = new List<int> { 2, 3, 7, 12, 15, 21, 33 };

            Assert.That(numbers.Count, Is.EqualTo(7));
            int result = numbers.BinarySearch(3);
            Assert.That(result, Is.EqualTo(1));

            result = numbers.BinarySearch(1);
            Assert.That(result, Is.EqualTo(-1));

            result = numbers.BinarySearch(35);
            Assert.That(result, Is.EqualTo(-8));
        }

        [Test]
        public void linearSearchTest()
        {
            var numbers = new List<int>() { 5, 3, 1, 10, 2 };
            int result = numbers.IndexOf(1);
            Assert.That(result, Is.EqualTo(2));

            numbers.Remove(1);
            Assert.That(numbers.Count, Is.EqualTo(4));

            result = numbers.IndexOf(1);
            Assert.That(result, Is.EqualTo(-1));

        }

        [Test]
        public void filterTest()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 8, 10, 21, 23, 25, 26 };
            Assert.That(numbers.Count, Is.EqualTo(10));

            // use a simple lambda to identify even numbers, and build a new list
            var evenNumbers = numbers.FindAll(n => n % 2 == 0);
            Assert.That(evenNumbers.Count, Is.EqualTo(5));

        }
    }
}
