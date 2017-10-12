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
            var publisher = new PriceChangedPublisher();

            var stock1 = new Stock("BIO", 25.5m, publisher);
            Assert.That(stock1.Symbol == "BIO");
            Assert.That(stock1.CurrentPrice == 25.5m);

            var stock2 = new Stock("AMZ", 14m, publisher);
            Assert.That(stock2.Symbol == "AMZ");
            Assert.That(stock2.CurrentPrice == 14m);

            publisher.ChangePrice("BIO", 23m, stock1.CurrentPrice);
            
            publisher.ChangePrice("BIO", 23m, stock1.CurrentPrice);
            Assert.That(stock1.CurrentPrice == 23m);
            Assert.That(stock2.CurrentPrice == 14m);

            publisher.ChangePrice("AMZ", 19m, stock2.CurrentPrice);
            Assert.That(stock1.CurrentPrice == 23m);
            Assert.That(stock2.CurrentPrice == 19m);
        }
    }
}
