using ExchangeRate.Domain.Model;
using ExchangeRate.Domain.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRate.App
{
    public class ExchangeRateApplicationService : IExchangeRateApplicationService
    {
        private ExchangeRateService _exchangeRateService;
        private IEnumerable<IExchangeRateRepository> _exchangeRatesProviders;

        public ExchangeRateApplicationService(IEnumerable<IExchangeRateRepository> exchangeRatesProviders)
        {
            _exchangeRateService = new ExchangeRateService();
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
