using System;

namespace ExchangeRate.Domain.Model
{
    public class ExchangeRateModel : IComparable
    {
        public Guid ID { get; set; }

        public double Value { get; set; }

        public string CurrencyPair { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            return obj is ExchangeRateModel otherExchangeRate
                ? this.Value.CompareTo(otherExchangeRate.Value)
                : throw new ArgumentException("Object is not a ExchangeRate");
        }
    }
}
