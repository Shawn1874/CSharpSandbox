
using System.Collections.ObjectModel;
using NUnit.Framework;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Generics
{
    [TestFixture]
    class ObservableCollection
    {
        static NotifyCollectionChangedAction lastAction;

        static void testCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            lastAction = e.Action;
        }

        [Test]
        public void DefaultConstructorTest()
        {
            var collection = new ObservableCollection<int>();
            Assert.That(collection.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Demonstrate how the CollectionChanged event works
        /// </summary>
        [Test]
        public void CollectionChangedTest()
        {
            var collection = new ObservableCollection<int>();
            collection.CollectionChanged += testCollectionChanged;

            collection.Add(1);
            Assert.That(collection.Count, Is.EqualTo(1));
            Assert.That(lastAction, Is.EqualTo(NotifyCollectionChangedAction.Add));

            collection[0] = 2;
            Assert.That(collection.Count, Is.EqualTo(1));
            Assert.That(lastAction, Is.EqualTo(NotifyCollectionChangedAction.Replace));

            collection.Add(4);
            Assert.That(collection.Count, Is.EqualTo(2));
            Assert.That(lastAction, Is.EqualTo(NotifyCollectionChangedAction.Add));

            collection.Remove(4);
            Assert.That(collection.Count, Is.EqualTo(1));
            Assert.That(lastAction, Is.EqualTo(NotifyCollectionChangedAction.Remove));
        }

    }
}
