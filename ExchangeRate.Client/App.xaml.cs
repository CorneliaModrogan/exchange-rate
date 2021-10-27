using ApiLayerProvider;
using ExchangeRate.App;
using ExchangeRate.Domain.Model;
using ExchangeRate.Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using OpenExchangeRateProvider;
using System;
using System.Configuration;
using System.Windows;

namespace ExchangeRate.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IExchangeRateApplicationService _exchangeRateApplicationService;
        private readonly string _displayedText = "The Best exchange rate value is: {0}";
        private readonly string _textForNoValue = "There were issues in retrieving data";

        protected override async void OnStartup(StartupEventArgs e)
        {
            string printMode = ConfigurationManager.AppSettings["PrintMode"];

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _exchangeRateApplicationService = serviceProvider.GetService<IExchangeRateApplicationService>();
            var bestExchange = await _exchangeRateApplicationService.GetBestExchangeRate();

            DisplayMessage(printMode, bestExchange);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IExchangeRateRepository, ApiLayerService>()
                .AddPolicyHandler(HttpClientPolicyHandlers.GetRetryPolicy());
            services.AddHttpClient<IExchangeRateRepository, OpenExchangeRateService>()
                .AddPolicyHandler(HttpClientPolicyHandlers.GetRetryPolicy());
            services.AddScoped<IExchangeRateService, ExchangeRateService>();
            services.AddScoped<IExchangeRateApplicationService, ExchangeRateApplicationService>();
        }

        private void OpenMessageBox(string messageBoxText)
        {
            string caption = string.Empty;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }

        private void OpenConsole(string text)
        {
            ConsoleAllocator.ShowConsoleWindow();
            Console.WriteLine(text);
        }

        private void DisplayMessage(string printMode, ExchangeRateModel bestExchange)
        {
            string text = bestExchange == null ? _textForNoValue : string.Format(_displayedText, bestExchange.Value);
            switch (printMode)
            {
                case "Console":
                    OpenConsole(text);
                    break;
                case "MessageBox":
                    OpenMessageBox(text);
                    break;
            }
        }

    }
}
