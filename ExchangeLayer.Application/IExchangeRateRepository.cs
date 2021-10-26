using ExchangeRate.Domain.Model;
using System.Threading.Tasks;

namespace ExchangeRate.App
{
    public interface IExchangeRateRepository
    {
        public Task<ExchangeRateModel> GetAsync();
    }
}
