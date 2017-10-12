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

        public Stock(string symbol, decimal initialPrice)
        {
            _symbol = symbol;
            _currentPrice = initialPrice;
        }

        public void Subscribe(PriceChangedPublisher publisher)
        {
            publisher.PriceChanged += OnPriceChanged;
        }

        protected virtual void OnPriceChanged(PriceChangedPublisher source, PriceChangedEventArgs args)
        {
            if (args != null && args._symbol == _symbol)
            {
                _currentPrice = args._newPrice;
                Console.WriteLine(String.Format("Price changed from {0} to {1}", args._lastPrice, args._newPrice));
            }
        }
    }
}
