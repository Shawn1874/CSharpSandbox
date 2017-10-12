using NUnit.Framework;
using System;
using EventsAndDelegates;

namespace Delegates
{
    [TestFixture]
    public class EventTests
    {
        [Test]
        public void PriceChangedEventTest()
        {
            var stock1 = new Stock("BIO", 25.5m);
            Assert.That(stock1.Symbol == "BIO");
            Assert.That(stock1.CurrentPrice == 25.5m);

            var stock2 = new Stock("AMZ", 14m);
            Assert.That(stock2.Symbol == "AMZ");
            Assert.That(stock2.CurrentPrice == 14m);

            var publisher = new PriceChangedPublisher();

            publisher.ChangePrice("BIO", 23m, stock1.CurrentPrice);

            // There are no event subscribers yet so the prices in the stocks haven't changed yet.
            Assert.That(stock1.CurrentPrice == 25.5m);
            Assert.That(stock2.CurrentPrice == 14m);

            stock1.Subscribe(publisher);
            stock2.Subscribe(publisher);
            publisher.ChangePrice("BIO", 23m, stock1.CurrentPrice);
            Assert.That(stock1.CurrentPrice == 23m);
            Assert.That(stock2.CurrentPrice == 14m);

            publisher.ChangePrice("AMZ", 19m, stock2.CurrentPrice);
            Assert.That(stock1.CurrentPrice == 23m);
            Assert.That(stock2.CurrentPrice == 19m);
        }
    }
}
