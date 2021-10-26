using ExchangeRate.Domain.Model;
using System.Collections.Generic;

namespace ExchangeRate.Domain.Service
{
    public class ExchangeRateService
    {
        public ExchangeRateEntity ComputeMaximum(List<ExchangeRateEntity> exchangeRates)
        {
            ExchangeRateEntity maxExchangeRate = exchangeRates[0];
            foreach(var exchangeRate in exchangeRates)
            {
                if (exchangeRate.CompareTo(maxExchangeRate) > 0)
                {
                    maxExchangeRate = exchangeRate;
                }
            }
            return maxExchangeRate;
        }
    }
}
