using ExchangeRate.App;
using ExchangeRate.Domain.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiLayerProvider
{
    public class ApiLayerService : IExchangeRateRepository
    {
        private readonly HttpClient _httpClient;

        private readonly string _currencyPair = "USDEUR";

        public ApiLayerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string baseUri = "http://www.apilayer.net/api/live?access_key=ca2efa55a751808737f72d415f1ca387";

        public async Task<ExchangeRateModel> GetAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(baseUri);
                var resultObject =  JObject.Parse(response);
                var resultRate = resultObject["quotes"]["USDEUR"].ToString();
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
