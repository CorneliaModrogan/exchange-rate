using ExchangeRate.App;
using ExchangeRate.Domain.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenExchangeRateProvider
{
    public class OpenExchangeRateService : IExchangeRateRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _currencyPair = "USDEUR";

        public OpenExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string baseUri = "https://openexchangerates.org/api/latest.json?app_id=1de86dfd996b4c9da20c0b3fa6eefaa4&base=USD";

        public async Task<ExchangeRateModel> GetAsync()
        {
            try
            {
                string result = await _httpClient.GetStringAsync(baseUri);
                var resultObject = JObject.Parse(result);
                string resultRate = resultObject["rates"]["EUR"].ToString();

                double rate;
                if (!double.TryParse(resultRate, out rate))
                {
                    rate = double.MinValue;
                }
                return new ExchangeRateModel()
                    {
                        ID = new Guid(),
                        Value = rate,
                        CurrencyPair = _currencyPair
                };
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Trace.TraceError(e.Message);
            }
            return null;
        }
    }
}
