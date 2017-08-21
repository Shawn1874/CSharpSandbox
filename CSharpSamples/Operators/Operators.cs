using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operators
{
    [TestFixture]
    public class OperatorsTests
    {
        [Test]
        public void RelationalOperatorsTests()
        {
            // <, <=
            Assert.That(5 < 7);
            Assert.That("apple", Is.LessThanOrEqualTo("apple"));
            Assert.That("apple", Is.LessThanOrEqualTo("bananas"));

            // >, >=
            Assert.That(1, Is.GreaterThan(0));
            Assert.That(5 * 5, Is.GreaterThanOrEqualTo(25));

            // =, !=
            int a = 5;
            int b = 5;
            int c = 7;
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a, Is.Not.EqualTo(c));
        }

        [Test]
        public void LogicalOperatorsTests()
        {
            int x = 0xFF00;
            int y = 0xFF;
            Assert.That(x & y, Is.EqualTo(0));
            Assert.That(x | y, Is.EqualTo(0xFFFF));

            int w = 0xAAAA;
            int z = 0x5555;
            Assert.That(w ^ z, Is.EqualTo(0xFFFF));
            Assert.That(z ^ w, Is.EqualTo(0xFFFF));
            Assert.That(x ^ y, Is.EqualTo(0xFFFF));
            Assert.That(w ^ y, Is.EqualTo(0xAA55));

            bool a = true;
            bool b = false;
            Assert.That(a && b == false);
            Assert.That(a || b == true);

            string s = null;
            string t = "fox";
            Assert.That(s ?? t, Is.EqualTo("fox"));

            s = "the";
            Assert.That(s ?? t, Is.EqualTo("the"));

            ushort r = 0xFFFA;
            ushort p = (ushort) ~r;
            Assert.That(p, Is.EqualTo((ushort)0x5));
        }

        [Test]
        public void ConditionalOperatorTest()
        {
            string name = "Shawn";
            Assert.That(name.Contains("S") ? "yes" : "no", Is.EqualTo("yes"));
        }

        [Test]
        public void IncrementDecrementTests()
        {
            int a = 5;
            Assert.That(a++ == 5);
            Assert.That(++a == 7);

            Assert.That((a -= 2) == 5);
            Assert.That((a += 5) == 10);
        }
    }
}
