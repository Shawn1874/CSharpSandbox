using NUnit.Framework;
using System;
using System.Text;

namespace Strings
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
        public void TrimEndTest()
        {
            var expected = "test";
            string s1 = string.Format("{0}{1}", expected, Environment.NewLine);
            var actual = s1.TrimEnd(Environment.NewLine.ToCharArray());
            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}
