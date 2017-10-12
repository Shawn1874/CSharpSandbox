using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndDelegates
{
    public interface IPublisher
    {
        event EventHandler<PriceChangedEventArgs> PriceChanged;
    }

    public class PriceChangedPublisher : IPublisher
    {
        public event EventHandler<PriceChangedEventArgs> PriceChanged;
        //public delegate void PriceChangedHandler(PriceChangedPublisher source, PriceChangedEventArgs args);

        public void ChangePrice(string symbol, decimal newPrice, decimal lastPrice)
        {
            var args = new PriceChangedEventArgs(symbol, newPrice, lastPrice);
            PriceChanged?.Invoke(this, args);
        }
    }
}
