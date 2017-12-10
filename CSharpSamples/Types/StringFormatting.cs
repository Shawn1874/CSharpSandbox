using NUnit.Framework;
using System;

namespace Types
{
    [TestFixture]
    class StringFormattingTests
    {
        // struct instances are zero initialized
        [Test]
        public void StringInterpolationTest()
        {
            var p = new Person();
            p.FirstName = "Shawn";
            p.LastName = "Fox";

            var fullName = $"{p.FirstName} {p.LastName}";
            Assert.That(fullName, Is.EqualTo("Shawn Fox"));
        }
    }
}
