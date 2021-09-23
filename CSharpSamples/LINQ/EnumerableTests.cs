using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LINQ
{
    [TestFixture]
    class EnumerableTests
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
