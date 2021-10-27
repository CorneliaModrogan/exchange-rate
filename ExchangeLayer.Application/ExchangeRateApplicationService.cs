using ExchangeRate.Domain.Service;
using ExchangeRate.Domain.Model;
using ExchangeRate.Domain.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRate.App
{
    public class ExchangeRateApplicationService : IExchangeRateApplicationService
    {
        private IExchangeRateService _exchangeRateService;
        private IEnumerable<IExchangeRateRepository> _exchangeRatesProviders;

        public ExchangeRateApplicationService(IExchangeRateService exchangeRateService, IEnumerable<IExchangeRateRepository> exchangeRatesProviders)
        {
            _exchangeRateService = exchangeRateService;
            _exchangeRatesProviders = exchangeRatesProviders;
        }

        public async Task<ExchangeRateModel> GetBestExchangeRate()
        {
            List<ExchangeRateModel> exchangeRates = new List<ExchangeRateModel>();
            foreach (var exchangeRatesProvider in _exchangeRatesProviders)
            {
                var exchangeRate = await exchangeRatesProvider.GetAsync();
                if (exchangeRate != null)
                {
                    exchangeRates.Add(exchangeRate);
                }
            }
            return _exchangeRateService.ComputeMaximum(exchangeRates);

        }
    }
}
