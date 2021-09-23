using NUnit.Framework;
using System;
using System.Text;
using CustomString;
using System.Linq;

namespace Types
{
    [TestFixture]
    public class BasicStringOperations
    {
        [Test]
        public void CompareStringsTest()
        {
            string s1 = "hello";
            string s2 = "hello";
            Assert.That(s1, Is.EqualTo(s2));
        }

        [Test]
        public void StringReverseManuallyTest()
        {
            var test = "room";
            var result = Methods.reverse(test);
            Assert.That(result, Is.EqualTo("moor"));

            test = "9876543210";
            Assert.That(Methods.reverse(test), Is.EqualTo(test.Reverse()));
        }

        [Test]
        public void TrimEndTest()
        {
            var expected = "test";
            string s1 = string.Format("{0}{1}", expected, Environment.NewLine);
            var actual = s1.TrimEnd(Environment.NewLine.ToCharArray());
            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}
