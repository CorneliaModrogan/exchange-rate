using ExchangeRate.Domain.Model;
using System.Threading.Tasks;

namespace ExchangeRate.App
{
    public interface IExchangeRateApplicationService
    {
        public Task<ExchangeRateModel> GetBestExchangeRate();
    }
}
