using System;

namespace EventsAndDelegates
{
    public class Stock
    {
        private readonly string _symbol;

        public string Symbol
        {
            get { return _symbol; }
        }

        private decimal _currentPrice;

        public decimal CurrentPrice
        {
            get { return _currentPrice; }
        }

        //public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public Stock(string symbol, decimal initialPrice, IPublisher publisher)
        {
            _symbol = symbol;
            _currentPrice = initialPrice;
            publisher.PriceChanged += OnPriceChanged;
        }

        protected virtual void OnPriceChanged(Object source, PriceChangedEventArgs pcArgs)
        {
            if (pcArgs != null && pcArgs._symbol == _symbol)
            {
                _currentPrice = pcArgs._newPrice;
            }
        }
    }
}
