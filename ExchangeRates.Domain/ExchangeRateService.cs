using ExchangeRate.Domain.Model;
using System.Collections.Generic;

namespace ExchangeRate.Domain.Service
{
    public class ExchangeRateService
    {
        public ExchangeRateModel ComputeMaximum(List<ExchangeRateModel> exchangeRates)
        {
            if (exchangeRates.Count > 0)
            {
                ExchangeRateModel maxExchangeRate = exchangeRates[0];
                for (var i = 1; i < exchangeRates.Count; i++)
                {
                    var exchangeRate = exchangeRates[i];
                    if (exchangeRate.CompareTo(maxExchangeRate) > 0)
                    {
                        maxExchangeRate = exchangeRate;
                    }
                }
                return maxExchangeRate;
            }
            return null;
        }
    }
}
