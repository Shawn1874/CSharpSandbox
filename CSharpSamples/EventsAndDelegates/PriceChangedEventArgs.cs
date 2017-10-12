using System;

namespace EventsAndDelegates
{
    public class PriceChangedEventArgs : EventArgs
    {
        public readonly string _symbol;
        public readonly decimal _lastPrice;
        public readonly decimal _newPrice;

        public PriceChangedEventArgs(string symbol, decimal newPrice, decimal lastPrice)
        {
            _symbol = symbol;
            _lastPrice = lastPrice;
            _newPrice = newPrice;
        }
    }
}
