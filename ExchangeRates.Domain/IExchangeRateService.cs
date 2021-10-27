using ExchangeRate.Domain.Model;
using System.Collections.Generic;

namespace ExchangeRate.Domain.Service
{
    public interface IExchangeRateService
    {
        public ExchangeRateModel ComputeMaximum(List<ExchangeRateModel> exchangeRates);
    }
}
