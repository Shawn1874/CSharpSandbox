using NUnit.Framework;
using System;

namespace Types
{
    struct Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    };

    struct Transaction
    {
        public decimal cost;
        public int id;

        public Transaction(decimal cost, int id)
        {
            this.cost = cost;
            this.id = id;
        }
    };

    [TestFixture]
    class StructsTests
    {
        // struct instances are zero initialized
        [Test]
        public void InitializationTest()
        {
            // Test custom constructor
            Point coordinates = new Point(5, 10);
            Assert.That(coordinates.x, Is.EqualTo(5));
            Assert.That(coordinates.y, Is.EqualTo(10));

            // Test default constructor
            Point coordinates2 = new Point();
            Assert.That(coordinates2.x, Is.EqualTo(0));
            Assert.That(coordinates2.y, Is.EqualTo(0));

            // test assignment
            coordinates2 = coordinates;
            Assert.That(coordinates2.x, Is.EqualTo(5));
            Assert.That(coordinates2.y, Is.EqualTo(10));


            // Test custom constructor
            var transaction = new Transaction(25.75m, 1);
            Assert.That(transaction.id, Is.EqualTo(1));
            Assert.That(transaction.cost, Is.EqualTo(25.75m));

            // Test default constructor
            var transaction2 = new Transaction();
            Assert.That(transaction2.id, Is.EqualTo(0));
            Assert.That(transaction2.cost, Is.EqualTo(0m));

            // test assignment
            transaction2 = transaction;
            Assert.That(transaction2.id, Is.EqualTo(1));
            Assert.That(transaction2.cost, Is.EqualTo(25.75m));
        }
    }
}
