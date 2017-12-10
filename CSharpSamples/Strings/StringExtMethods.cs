using NUnit.Framework;
using System.Linq;

namespace Types
{
    [TestFixture]
    class StringExtMethodsTests
    {
        /// <summary>
        /// Assert that the Any extension method also works for strings.
        /// </summary>
        [Test]
        public void StringAnyTest()
        {
            var emptyString = "";
            Assert.That(emptyString.Any() == false);
        }

        [Test]
        public void StringReverseTest()
        {
            var test = "12345";
            var reversed = test.Reverse();
            Assert.That(reversed, Is.EqualTo("54321"));
        }

    }
}
