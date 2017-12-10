using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    [TestFixture]
    public class Dictionary
    {
        [Test]
        public void AddTest()
        {
            var container = new Dictionary<int, string>();

            container.Add(1, "one");
            Assert.That(container.ContainsKey(1));
            Assert.That(container.ContainsValue("one"));
            Assert.That(container[1], Is.EqualTo("one"));

            container[2] = "two";
            Assert.That(container.ContainsKey(2));
            Assert.That(container.ContainsValue("two"));
            Assert.That(container[2], Is.EqualTo("two"));
        }

        [Test]
        public void ClearTest()
        {
            var container = new Dictionary<int, string>(5);

            container.Add(1, "one");
            Assert.That(container.Count, Is.EqualTo(1));
            container.Clear();
            Assert.That(container.Count, Is.EqualTo(0));
        }

        [Test]
        public void TryGetValueTest()
        {
            var container = new Dictionary<int, string>();
            string output = null;
            var result = container.TryGetValue(1, out output);
            Assert.That(result, Is.False);
            Assert.That(output, Is.Null);
        }

        [Test]
        public void RemoveTest()
        {
            var container = new Dictionary<int, string>();

            container.Add(1, "one");
            container[2] = "two";
            Assert.That(container.Count, Is.EqualTo(2));

            container.Remove(1);
            Assert.That(container.Count, Is.EqualTo(1));
            Assert.That(container.ContainsKey(1), Is.False);
            Assert.That(container.ContainsValue("one"), Is.False);
        }
    }
}
